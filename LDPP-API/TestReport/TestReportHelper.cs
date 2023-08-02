using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using System.Threading;
using LDPP_API.Common.DCLDPP;
using LDPP_API;
using System.IO;

namespace LDPPInterfaceTesting.TestReport
{
    class TestReportHelper
    {
        private static readonly Allure.Commons.AllureLifecycle allure = Allure.Commons.AllureLifecycle.Instance;

        #region allure report upload

        public void GenerateTestReport()
        {
            RunCmd("taskkill /f /im cmd.exe");
            Thread.Sleep(3000);
            for (int i = 0; i < 3; i++)
            {
                Process.Start(FileConstants.TestReportGenerateFile);
                Thread.Sleep(3000);
                Process[] ps1 = Process.GetProcessesByName("cmd");
                if (ps1.Length > 0)
                {
                    break;
                }
            }
            do
            {
                Thread.Sleep(3000);
                Process[] ps1 = Process.GetProcessesByName("cmd");
                if (ps1.Length == 0)
                {
                    break;
                }

            } while (true);
        }

        public void RunCmd(string cmdStr)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/S /C " + cmdStr)
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas"
            };

            Process.Start(processInfo);
        }

        public void TestReportUpload()
        {
            string testReportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"allure-results");
            string allureHtmlFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "allure-results\\html-report"); 
            string zip_file_name = Path.Combine(Path.GetDirectoryName(testReportFolder), Path.GetFileName(Hooks.testReportDataFile));
            if(Directory.Exists(FileConstants.TestLogPath))
            {
                Directory.Move(FileConstants.TestLogPath, Path.Combine(testReportFolder, "Logs"));
            }
            if (Directory.Exists(FileConstants.TestScreenShotPath))
            {
                Directory.Move(FileConstants.TestScreenShotPath, Path.Combine(testReportFolder, "ScreenShots"));
            }
            FtpHelper ftp = new FtpHelper();
            DirectoryInfo allureDir = new DirectoryInfo(allureHtmlFolder);
            FileInfo[] files = allureDir.GetFiles("*", SearchOption.AllDirectories);

            if (!File.Exists(zip_file_name))
            {
                ZipFile.CreateFromDirectory(testReportFolder, zip_file_name);
            }

            ftp.Upload(zip_file_name, Path.GetDirectoryName(Hooks.testReportDataFile));

            foreach (var f in files)
            {
                string info = f.FullName.Replace(allureHtmlFolder, "").Trim();
                string path = Hooks.testReportPath+ info;
                ftp.Upload(f.FullName, Path.GetDirectoryName(path));
            }
            
            //DirectoryInfo[] directories = screen.GetDirectories("*", SearchOption.AllDirectories);
            //FileInfo[] sfiles = report.GetFiles("*", SearchOption.AllDirectories);
            //foreach (var d in directories)
            //{
            //    ftp.FtpMakeDir(d.FullName.Replace(testScreenShots, ""));
            //}
            //foreach (var f in files)
            //{
            //    ftp.Upload(f.FullName, "SmartEngineTestReport/" + machine + "/" + day + "/allure-results");
            //}
            //foreach (var f in sfiles)
            //{
            //    ftp.Upload(f.FullName, "SmartEngineTestReport/" + machine + "/" + day + "/ScreenShots");
            //}

        }


        #endregion

        # region 标记测试用例状态

        /// <summary>
        /// 标记测试用例状态
        /// </summary>
        /// <param name="status">1:警告信息，测试用例继续执行，用于提示  3：跳过测试用例执行 </param>
        /// <param name="message">提示信息</param>
        public void TestStatus(int status, string message="")
        {
            switch (status)
            {
                case 1:
                    Hooks.reportDetail.status = 2;
                    TestReportAddLogInfo(message);
                    break;
                case 3:
                    Hooks.reportDetail.status = 3;
                    Assert.Ignore(message);
                    break;
            }
        }

        #endregion


        #region allure 添加日志/截图

        public void TestReportAddLogInfo(string info,string title= "测试日志")
        {
            allure.AddAttachment(title, "text/plain", WriteLog(info, ""));
        }

        public void TestReportAddScreenShot(string imagePath, string imageName)
        {
            allure.AddAttachment(imagePath, imageName);
        }

        public void TestErrorScreenShot()
        {
            string screenShotfilepath = Path.Combine(FileConstants.TestScreenShotPath, "TestError//" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".jpeg");
            CaptureScreen.PrintScreen(screenShotfilepath);
            allure.AddAttachment(screenShotfilepath, "TestError");
        }

        private string WriteLog(string LogText, string file = null)
        {
            string LogDir = FileConstants.TestLogPath;
            file = DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + "_log.txt";
            string fileName = Path.Combine(LogDir, file);
            try
            {
                if (!Directory.Exists(LogDir))
                {
                    Directory.CreateDirectory(LogDir);
                }
                if (!File.Exists(fileName))
                {
                    StreamWriter swFile;
                    swFile = File.CreateText(fileName);
                    swFile.Close();
                }
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + LogText);
                sw.Close();
            }
            catch (Exception ex)
            {
                WriteLog("WriteLogs() Exception:" + LogText, fileName);
                WriteLog("WriteLogs() Exception:" + ex.ToString(), fileName);
            }
            return fileName;
        }

        #endregion

    }
}
