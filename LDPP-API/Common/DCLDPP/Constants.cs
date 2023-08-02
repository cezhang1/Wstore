using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.DCLDPP
{
    internal class Constants
    {
        public static string key = "9C2DCD8A";
        public static string iv = "8CF8C5D4";
        public static string ConfigDir = @"C:\LenovoSmartEngineClient\Config";
        public static string BsaeConfigFile = Path.Combine(ConfigDir, "base_conf.ini");
        public static string TaskConfigFile = Path.Combine(ConfigDir, "task_conf.ini");


        public static string TestRootPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string TestReportGenerateFile = Path.Combine(TestRootPath, "TestScript/GenerateTestReport.bat");
        public static string TestLogPath = Path.Combine(TestRootPath, "Logs");
        public static string TestScreenShotPath = Path.Combine(TestRootPath, "ScreenShots");

        public static string TestReportBastPath = "allure/se-for-pcm-api";
    }
}
