using System;
using System.IO;


namespace LDPP_API.Common.Helper
{
    public class FileDel
    {
        public string GetDiscoverLogFileDir()
        {
            string logDir = "";
            try
            {
                logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lenovo", "Unity");    //2. user permission (%localappdata%)
            }
            catch (Exception)
            {



                logDir = @"C:/temp/";
            }



            string[] strDir = new string[2] { "LDPPData", "key" };



            for (int i = 0; i < strDir.Length; i++)
            {
                logDir = Path.Combine(logDir, strDir[i]);
            }
            return logDir;
        }



        public string deleteOneFile()
        {
            string fileFullPath = GetDiscoverLogFileDir();

            // 1、首先判断文件或者文件路径是否存在
            if (Directory.Exists(fileFullPath))
            {
                // 2、根据路径字符串判断是文件还是文件夹
                FileAttributes attr = File.GetAttributes(fileFullPath);
                // 3、根据具体类型进行删除
                if (attr == FileAttributes.Directory)
                {
                    Directory.Delete(fileFullPath, true); // 3.1、删除文件夹
                }
                else
                {
                    File.Delete(fileFullPath);// 3.2、删除文件
                }
                File.Delete(fileFullPath);
                return "删除成功:" + fileFullPath;
            }
            return "无该文件或文件夹:" + fileFullPath;
        }
    }
}



