using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.DCLDPP
{
    public class FileConstants
    {

        public static string _sWatchPath = @"C:\Users\YogaS950\AppData\Local\Lenovo\Unity\LDPPData\log\Discover_trace.log";
        //public void TaskConfigFile()
        //{ 

        public static long _startLocationDiscover;
        //}

        public static string key = "9C2DCD8A";
        public static string iv = "8CF8C5D4";
        public static string ConfigDir = @"C:\LenovoSmartEngineClient\Config";
        public static string BsaeConfigFile = Path.Combine(ConfigDir, "base_conf.ini");
        public static string TaskConfigFile = Path.Combine(ConfigDir, "task_conf.ini");


        public static string TestRootPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string TestReportGenerateFile = Path.Combine(TestRootPath, "TestScript/GenerateTestReport.bat");
        public static string TestLogPath = Path.Combine(TestRootPath, "Logs");
        public static string TestScreenShotPath = Path.Combine(TestRootPath, "ScreenShots");

        public static string TestReportBastPath = "allure/dc-for-ldpp1.0-api";
        public static string TestReportDataPath = "data/dc-for-ldpp1.0-api";

        public static string TaskClientStartFile = @"C:\LenovoSmartEngineClient\TestScripts\agent_start.bat";
        public static string TaskClientStopFile = @"C:\LenovoSmartEngineClient\TestScripts\agent_stop.bat";

        public static string TestScriptStopFile = @"C:\LenovoSmartEngineClient\TestScripts\dc_api_stop.bat";

        public static string TestAllureReportPath = Path.Combine(TestRootPath, "allure-results");

        public static string TestHistoryPath = Path.Combine(TestRootPath, "History");

    }
}
