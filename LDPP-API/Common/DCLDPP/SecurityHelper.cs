using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.DCLDPP
{
    public class SecurityHelper
    {
        #region DES加密 解密

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public string DESEncrypt(string data, string key, string iv)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public string DESDecrypt(string data, string key, string iv)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        #endregion

        #region RAS加密 解密

        /// <summary> 
        /// RSA加密数据 
        /// </summary> 
        /// <param name="express">要加密数据</param> 
        /// <param name="KeyContainerName">密匙容器的名称</param> 
        /// <returns></returns> 
        public string RSAEncryption(string express, string KeyContainerName = null)
        {

            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "zhiqiang"; //密匙容器的名称，保持加密解密一致才能解密成功
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] plaindata = System.Text.Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }

        /// <summary> 
        /// RSA解密数据 
        /// </summary> 
        /// <param name="express">要解密数据</param> 
        /// <param name="KeyContainerName">密匙容器的名称</param> 
        /// <returns></returns> 
        public string RSADecrypt(string ciphertext, string KeyContainerName = null)
        {
            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "zhiqiang"; //密匙容器的名称，保持加密解密一致才能解密成功
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return System.Text.Encoding.Default.GetString(decryptdata);
            }
        }

        #endregion

        #region Base64加密 解密 
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }

        #endregion

        # region AES 加密 解密
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region SHA加密方式  可用作验签

        //SHA为不可逆加密方式
        public string SHA1Encrypt(string Txt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(Txt);
            var SHA = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var encryptbytes = SHA.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public string SHA256Encrypt(string Txt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(Txt);
            var SHA256 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            var encryptbytes = SHA256.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public string SHA384Encrypt(string Txt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(Txt);
            var SHA384 = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            var encryptbytes = SHA384.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public string SHA512Encrypt(string Txt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(Txt);
            var SHA512 = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            var encryptbytes = SHA512.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        #endregion

        #region MD5加密 可用作验签

        public string ToMD5(string strs)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(strs);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(bytes);//将字符串加密后也转换为字符数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }

        #endregion
    }
}
