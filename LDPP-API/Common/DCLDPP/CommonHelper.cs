using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.DCLDPP
{
    public class CommonHelper
    {
        #region ini 文件处理        
        /// <summary>读取INI文件</summary>        
        /// /// <param name="section">section</param>       
        /// /// <param name="key">key</param>        
        /// /// <param name="filepath">ini文件路径</param>        
        /// /// <param name="size">默认2048</param>        
        /// /// <returns></returns>       
        public static string ReadIniValue(string section, string key, string file)
        {
            const int size = 1024 * 2;
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
            {
                return "false";
            }


            StringBuilder sb = new StringBuilder(size);
            int bytesReturned = GetPrivateProfileString(section, key, "", sb, size, file);

            if (bytesReturned != 0)
            {
                return sb.ToString();
            }

            return "false";
        }

        /// <summary>修改INI文件值</summary>       
        /// /// <param name="section">section</param>        
        /// /// <param name="key">key</param>        
        /// /// <param name="value">新值</param>        
        /// /// <param name="file">ini文件路径</param>        
        public static void WriteIniValue(string section, string key, string value, string file)
        {
            WritePrivateProfileString(section, key, value, file);
        }
        /// <summary>获取Ini文件Section </summary>        
        /// /// <param name="file">ini文件</param>       
        /// /// <returns></returns>        
        public List<string> ReadIniSections(string file)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, file);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }
        /// <summary>获取Ini文件Section下的Key </summary>        
        /// /// <param name="file">ini文件</param>        
        /// /// <param name="section">section</param>        
        /// /// <returns></returns>        
        public List<string> ReadKeys(string file, string section)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(section, null, null, buf, buf.Length, file);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32 ", CharSet = CharSet.Unicode)]
        internal static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, Byte[] retVal, int size, string filePath);
        #endregion


    }
}
