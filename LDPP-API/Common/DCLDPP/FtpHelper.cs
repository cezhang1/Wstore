
using System;
using System.IO;
using System.Net;

namespace LDPP_API.Common.DCLDPP
{
    class FtpHelper
    {
        private CommonHelper common = new CommonHelper();
        public FtpHelper()
        {

        }

        /// <summary>获取FTP配置信息</summary>
        /// <param name="type">FTP 类型</param>
        /// <returns>FTP配置信息</returns>
        /// <returns></returns>
        public FTP GetFTPInfo()
        {
            FTP ftp = new FTP();
            SecurityHelper sh = new SecurityHelper();
            ftp.FtpName = FTPTYPE.FTP_PA_Automated_Test_Report.ToString();
            ftp.Server = sh.DESDecrypt(CommonHelper.ReadIniValue(ftp.FtpName, "Server", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            ftp.Port = uint.Parse(CommonHelper.ReadIniValue(ftp.FtpName, "Port", FileConstants.BsaeConfigFile));
            ftp.UserName = sh.DESDecrypt(CommonHelper.ReadIniValue(ftp.FtpName, "UserName", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            ftp.Password = sh.DESDecrypt(CommonHelper.ReadIniValue(ftp.FtpName, "Password", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            ftp.RemotePath = FileConstants.TestReportBastPath;
            return ftp; 
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ftpPath">"a/s"</param>
        /// <param name="json"></param>
        public void Upload(string filename, string ftpPath)
        {
            FTP ftp = GetFTPInfo();
            string info = "";
            
            foreach (var item in ftpPath.Replace("\\", "/").Split('/'))
            {
                info += item + "/";
                FtpMakeDir("ftp://" + ftp.Server + ":" + ftp.Port + "/" + info);
            }
            //FtpMakeDir("ftp://" + ftpServer + "/" + ftpPath + "/");
            FileInfo fileInf = new FileInfo(filename);
            string url = "ftp://" + ftp.Server + ":" + ftp.Port + "/" + ftpPath + "/" + fileInf.Name;
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
            // ftp用户名和密码.
            reqFTP.Credentials = new NetworkCredential(ftp.UserName, ftp.Password);
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;
            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;
            // 缓冲大小设置为2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();
            try
            {
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //创建目录  
        public bool FtpMakeDir(string localPath)
        {
            FTP ftp = GetFTPInfo();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(new Uri(localPath));
            req.Credentials = new NetworkCredential(ftp.UserName, ftp.Password);
            req.KeepAlive = false;
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }


    }
}
