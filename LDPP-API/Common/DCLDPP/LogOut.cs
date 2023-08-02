using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.DCLDPP
{
    public class LogOut
    {
        public static void WriteInfoLog(string message)
        {
            try
            {
                string path = @"C:\tl";
                if (!Directory.Exists(path)) // 判断路径是否存在
                {
                    Directory.CreateDirectory(path);// 创建目录
                }

                string logFileName = path + "\\APICommon.log";
                if (!File.Exists(logFileName)) // 判断文件是否存在
                {
                    FileStream fc = File.Create(logFileName); // 创建文件
                    fc.Close();
                }

                StreamWriter writer = File.AppendText(logFileName);// 文件中添加文件流
                writer.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + " ：" + message);

                writer.Flush(); // 为了防止数据丢失，应该在关闭读写流之前先flush()
                writer.Close();
                writer.Dispose();


            }
            catch (Exception ex)
            {
                string path = @"C:\tl";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string logFileName = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "APICatch.log";
                if (!File.Exists(logFileName))
                {
                    FileStream fc = File.Create(logFileName); // 创建文件
                    fc.Close();
                }

                StreamWriter writer = File.AppendText(logFileName);
                writer.WriteLine(DateTime.Now.ToString("日志记录错误HH:mm:ss") + ex.Message + " " + message);

                writer.Flush();
                writer.Close();
                writer.Dispose();//加上这个代码

            }

        }

    }
}
