using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using LDPP_API.Common.Helper;
using System.IO;
using static LDPP_API.Common.Helper.Models;
using Newtonsoft.Json;
using NUnit.Framework.Internal.Execution;
using LDPPInterfaceTesting;
using System.Reflection;
using Microsoft.FSharp.Core;
using System.Threading;
using WindowsFormsApp1;
using LDPP_API.Common.DCLDPP;
using System.Security.RightsManagement;
using Newtonsoft.Json.Linq;
using NLog.Fluent;
using System.Diagnostics;
using ConsoleDebug;

namespace LDPP_API
{
    public class DeviceInfo
    {
        public string BT;
        public string BLE;
        public string Name;
        public string deviceIP;
        //public static long _startLocationDiscover;
    }

    

    [Binding]
    public sealed class ConnectFeatureStepDefinitions
    {
        public string Response;
        static string SSID;
        static string PWD;
        static string  BLE;
        static string  BT;
        //public static long _startLocationDiscover;
        private static Dictionary<string, DeviceInfo> devInfoMap = new Dictionary<string, DeviceInfo>();
        LenovoLDPPStep lenovoldppstep = new LenovoLDPPStep();
        DeviceInfo devInfo = new DeviceInfo();
        public static string DeviceName = null;
        
        //
        //read task .ini file
        public void ReadTaskFile()
        {
            //taskid
            string taskId = CommonHelper.ReadIniValue("Task", "task_id", FileConstants.TaskConfigFile);
            DeviceName = CommonHelper.ReadIniValue("Task", "device_name", FileConstants.TaskConfigFile);
        }
       

        public ConnectFeatureStepDefinitions()
        {
        }

        [Given(@"Add Waiting Time (.*)")]
        public void GivenAddWaitingTime(int sleep_time)
        {
            Thread.Sleep(sleep_time);
        }



        [Given(@"Open Ldpp App Window (.*)")]
        public void GivenOpenLdppAppWindow(string exe_path)
        {
            lenovoldppstep.InstallUnitySampleAppWindows(exe_path);
            Thread.Sleep(3000);
        }



        [When(@"Click Sign In Button")]
        public void WhenClickSignInButton()
        {
            lenovoldppstep.SiginInUnitySampleAppWindows();
            
        }


        [When(@"Click Sign out Button")]
        public void WhenClickSignOutButton()
        {
            lenovoldppstep.SiginOutUnitySampleAppWindows();
        }


        [When(@"Choose (.*) from The Following Windows Options And Click Test Button")]
        public void WhenChooseDiscoverFromTheFollowingWindowsOptionsAndClickTestButton(string window_name)
        {
            
            lenovoldppstep.TestUnitySampleAppWindows();
            Thread.Sleep(3000);
        }
        [When(@"Choose from The Following Windows Options And Click Test Button")]
        public void WhenChooseFromTheFollowingWindowsOptionsAndClickTestButton()
        {
            lenovoldppstep.TestUnitySampleAppWindows();
            Thread.Sleep(3000);
        }

        [When(@"Click Start Button on The (.*) Window")]
        public void WhenClickStartButtonOnTheWindow(string window_name)
        {
            string response;
            if (window_name == "connect")
            {
                response = lenovoldppstep.ConnectStartUnitySampleAppWindows();
            }
            else
            {
                response = lenovoldppstep.DiscoverStartUnitySampleAppWindows();
            }
            Response = response;
        }

        [When(@"Click Stop Button on The (.*) Window")]
        public void WhenClickStopButtonOnTheWindow(string window_name)
        {
            string response;
            if (window_name == "connect")
            {
                response = lenovoldppstep.ConnectStopUnitySampleAppWindows();
            }
            else
            {
                response = lenovoldppstep.DiscoverStopUnitySampleAppWindows();
            }
            Response = response;
        }


        [When(@"Click Stop Button on The window_name")]
        public void WhenClickStopButtonOnTheWindow_Name(Table table)
        {
            string response;
            if (table.Rows[0][0] == "connect")
            {
                response = lenovoldppstep.ConnectStopUnitySampleAppWindows();
            }
            else
            {
                response = lenovoldppstep.DiscoverStopUnitySampleAppWindows();
            }
            Response = response;
        }


        [When(@"Input Command And Press Send (.*) (.*) from (.*)")]
        public void WhenInputCommandAndPressSendStatus(string command_one, string command_two, string window_name)
        {
            if (command_two == "randomPwd")
            {
                SSID = lenovoldppstep.AutoGenSSID();
                PWD = lenovoldppstep.AutoGenPWD();
                command_two = "{ \"SSID\":\"" + SSID + "\",\"PWD\":\""+ PWD +"\"}";
                Response = window_name=="connect"
                    ? lenovoldppstep.ConnectSendUnitySampleAppWindows(command_one, command_two)
                    : lenovoldppstep.DiscoverSendUnitySampleAppWindows(command_one, command_two);
                return; 
            }
            if (command_two == "readyforlink")
            {
                //获取log文件中的设备BLE的BT、BLE
                BT = LenovoLDPPStep.GetDeviceInfo().BT;
                BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                LogOut.WriteInfoLog("BT: " + BT);
                LogOut.WriteInfoLog("BLE: " + BLE);
                command_two = "{\"Device\": \"{\\\"Name\\\": \\\"" + DeviceName + "\\\", \\\"BLE\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
            }
            Response = window_name == "connect"
                    ? lenovoldppstep.ConnectSendUnitySampleAppWindows(command_one, command_two)
                    : lenovoldppstep.DiscoverSendUnitySampleAppWindows(command_one, command_two);
            Console.WriteLine("真实的响应结果："+ Response);
            LogOut.WriteInfoLog("实际响应结果：" + Response);
        }

        [Then(@"The response is expected (.*) in (.*) under (.*) after (.*)")]
        public void ThenTheResponseIsExpected(string expectedRes, bool requireApprove, bool requireVerifyPwd, int verifyWholeResponse)
        {
            
            if (verifyWholeResponse == 0)
            {
                string[] resArr = expectedRes.Split(',');
                foreach (string item in resArr) {
                    Console.WriteLine(item);
                    Console.WriteLine(Response.Contains(item));
                    Assert.True(Response.Contains(item), "响应结果与预期不同(不需要验证整个res)");
                } ;
                return;
            }
            if (requireVerifyPwd) {
                bool haveSSID = Response.Contains(SSID);
                bool havePWD = Response.Contains(PWD);
                Assert.True(haveSSID && havePWD, "账号密码有误(不需要验证整个res)");
                return;
            }
            //Hooks.IsRequireApprove = requireApprove;
            Console.WriteLine("实际响应结果：" + Response);
            LogOut.WriteInfoLog("实际响应结果：" + Response);
            Assert.AreEqual(expectedRes, Response, "响应结果与预期不同1");
        }

        [When(@"Discoverui Input Command And Press Send <command_one> <command_two>")]
        public void WhenDiscoveruiInputCommandAndPressSendCommand_OneCommand_Two()
        {
            throw new PendingStepException();
        }

        [When(@"Discoverui Input Command And Press Send Start")]
        public void WhenDiscoveruiInputCommandAndPressSendStart(Table table)
        {

        }

        //tl
        [When(@"connectui Input Command And Press Send")]
        public void WhenConnectuiInputCommandAndPressSend(Table table)
        {
            string command_two, command_one;
            command_one = table.Rows[0][0];
            command_two = table.Rows[0][1];

            if (command_two == "randomPwd")
            {
                SSID = lenovoldppstep.AutoGenSSID();
                PWD = lenovoldppstep.AutoGenPWD();
                command_two = "{ \"SSID\":\"" + SSID + "\",\"PWD\":\"" + PWD + "\"}";
                Response = lenovoldppstep.ConnectSendUnitySampleAppWindows(command_one, command_two);
            }
            else
            {
                Response = lenovoldppstep.ConnectSendUnitySampleAppWindows(command_one, command_two);
            }

            
            Console.WriteLine("真实的响应结果：" + Response);
            LogOut.WriteInfoLog("实际响应结果：" + Response);
            Thread.Sleep(1000);
            //throw new PendingStepException();
        }

        [When(@"Discoverui Input Command And Press Send")]
        public void WhenDiscoveruiInputCommandAndPressSend(Table table)
        {
            string command_two, command_one;
            command_one = table.Rows[0][0];
            command_two = table.Rows[0][1];
            //获取设备名字
            devInfo.Name = lenovoldppstep.ReadTaskFile();

            if (command_one == "StartDiscover" && command_two == "{\"Status\":\"0\"}")
            {
                //记录当前文件的位置
                FileConstants._startLocationDiscover = lenovoldppstep.initFileInfo();
                LogOut.WriteInfoLog("_startLocationDiscover：" + FileConstants._startLocationDiscover);
                Thread.Sleep(2000);
            }
            if (command_one == "ReadyForLink")
            {
                //读取log文件获取设备的BLE BT
                lenovoldppstep.getdeviceinfo(FileConstants._startLocationDiscover);
                

                if (command_two == "readyforlink")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                    
                    command_two = "{\"Device\": \"{\\\"Name\\\": \\\"" + devInfo.Name + "\\\", \\\"BLE\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }
                if (command_two == "MissingDeviceError")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                    command_two = "{\"Device1\": \"{\\\"Name\\\": \\\"" + DeviceName + "\\\", \\\"BLE\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }
                if (command_two == "MissingNameError")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;

                    
                    command_two = "{\"Device\": \"{\\\"Name1\\\": \\\"" + DeviceName + "\\\", \\\"BLE\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }
                if (command_two == "InvalidNameError")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                    command_two = "{\"Device\": \"{\\\"Name\\\": \\\"Wrong name\\\", \\\"BLE\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }
                if (command_two == "MissingBLEError")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                    command_two = "{\"Device\": \"{\\\"Name\\\": \\\"" + DeviceName + "\\\", \\\"BLE1\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }
                if (command_two == "InvalidBTError")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                    command_two = "{\"Device\": \"{\\\"Name\\\": \\\"" + DeviceName + "\\\", \\\"BLE\\\": \\\"" + BLE + "\\\", \\\"BT\\\": \\\"12345678\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }
                if (command_two == "InvalidBLError")
                {
                    BT = LenovoLDPPStep.GetDeviceInfo().BT;
                    BLE = LenovoLDPPStep.GetDeviceInfo().BLE;
                    command_two = "{\"Device\": \"{\\\"Name\\\": \\\"" + DeviceName + "\\\", \\\"BLE\\\": \\\"12345678\\\", \\\"BT\\\": \\\"" + BT + "\\\"}\", \"SSID\": \"" + SSID + "\", \"PWD\": \"" + PWD + "\"}";
                }

                LogOut.WriteInfoLog("BT: " + BT);
                LogOut.WriteInfoLog("BLE: " + BLE);
            }

            //SetWifiApInfo接口测试
            if (command_two == "MissingkeySSIDError")
            {
                command_two = "{\"SSID1\":\"" + SSID + "\",\"PWD\":\"" + PWD + "\"}";
            }
            if (command_two == "InvalidvalueofkeyPWDError")
            {
                //{"SSID":"LENOVE_WIFIAP_TEST",PWD:"26853645"}
                command_two = "{\"SSID\":\"" + SSID + "\",\"PWD\":" + PWD + "}";
            }
            if (command_two == "PWDshorterthan8Error")
            {
                //{"SSID":"LENOVE_WIFIAP_TEST","PWD":"268545"}
                command_two = "{\"SSID\":\"" + SSID + "\",\"PWD\":\"" + PWD.Substring(0, 6) + "\"}";
            }
            if (command_two == "PWDlongerthan32Error")
            {
                //{"SSID":"LENOVE_WIFIAP_TEST","PWD":"268545"}
                PWD = "268555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555545";
                command_two = "{\"SSID\":\"" + SSID + "\",\"PWD\":\"" + PWD + "\"}";
            }
            if (command_two == "SetWifiApInfoNormal")
            {
                command_two = "{ \"SSID\":\"" + SSID + "\",\"PWD\":\"" + PWD + "\"}";
            }
            if (command_two == "InvalidkeyPWD")
            {
                command_two = "{ \"SSID\":\"" + SSID + "\",PWD:\"" + PWD + "\"}";
            }

            Response = lenovoldppstep.DiscoverSendUnitySampleAppWindows(command_one, command_two);
            LogOut.WriteInfoLog("Discover Response :" + Response);

            //Hooks.IsRequireApprove = requireApprove;
            Console.WriteLine("实际响应结果：" + Response);
            LogOut.WriteInfoLog("实际响应结果：" + Response);
            Thread.Sleep(3000);
        }

        [Then(@"The response is expected")]
        public void ThenTheResponseIsExpected(Table table)
        {
            string expectedRes = table.Rows[0][0];
            bool requireApprove = bool.Parse(table.Rows[0][1]);
            bool requireVerifyPwd = bool.Parse(table.Rows[0][2]);
            int verifyWholeResponse = int.Parse(table.Rows[0][3]);

            if (verifyWholeResponse == 0)
            {
                string[] resArr = expectedRes.Split(',');
                foreach (string item in resArr)
                {
                    Console.WriteLine(item);
                    Console.WriteLine(Response.Contains(item));

                    Assert.True(Response.Contains(item), "响应结果与预期不同(不需要验证整个res)");
                };
                return;
            }
            if (requireVerifyPwd)
            {
                bool haveSSID = Response.Contains(SSID);
                bool havePWD = Response.Contains(PWD);
                Assert.True(haveSSID && havePWD, "账号密码有误(不需要验证整个res)");
                return;
            }
            //Hooks.IsRequireApprove = requireApprove;
            Console.WriteLine("实际响应结果：" + Response);
            LogOut.WriteInfoLog("实际响应结果：" + Response);
            Assert.AreEqual(expectedRes, Response, "响应结果与预期不同1");
        }

        [When(@"close connect or discover UI")]
        public void WhenCloseUI(Table table)
        {
            switch (table.Rows[0][0])
            {
                case "connect":
                    lenovoldppstep.CloseConnectWindow();
                    break;
                case "discover":
                    lenovoldppstep.CloseDiscoverWindow();
                    break;
                case "unity":
                    lenovoldppstep.CloseunityWindow();
                    break;
                default:
                    break;

            }
            //throw new PendingStepException();
        }

        [When(@"User set wifi bluetooth mobilehotspot network connect status")]
        public void WhenUserSetWifiBluetoothMobilehotspotNetworkConnectStatus(Table table)
        {
            switch (table.Rows[0][0])
            {
                case "mobilehotspot":
                    lenovoldppstep.SetMobilehotspotToggle(bool.Parse(table.Rows[0][1]));
                    break;
                case "wifi":
                    lenovoldppstep.SetWifiToggle(bool.Parse(table.Rows[0][1]));
                    break;
                case "bluetooth":
                    lenovoldppstep.SetBluetoothToggle(bool.Parse(table.Rows[0][1]));
                    break;
                case "network connect":
                    lenovoldppstep.ConnectWifi(table.Rows[0][1]);
                    break;
                case "network disconnect":
                    lenovoldppstep.disconnectWifi();
                    break;
            }
        }

        [When(@"User set system status")]
        public void WhenUserSetSystemStatus(Table table)
        {
            switch (table.Rows[0][0])
            {
                case "S3":
                    SystemSleepHibernateHelper.Sleep();
                    for (int i = 0; i < 2; i++)
                    {
                        try
                        {
                            SystemSleepHibernateHelper.PowerManagement.MonitorOn();
                            Thread.Sleep(TimeSpan.FromSeconds(2));
                        }
                        catch
                        {

                        }
                    }
                    break;
                case "S4":
                    SystemSleepHibernateHelper.Hibernate();
                    for (int i = 0; i < 2; i++)
                    {
                        try
                        {
                            SystemSleepHibernateHelper.PowerManagement.MonitorOn();
                            Thread.Sleep(TimeSpan.FromSeconds(2));
                        }
                        catch
                        {


                        }
                    }
                    break;
               
            }
        }


        [When(@"Delete LDPP Key")]
        public void WhenDeleteLDPPKey()
        {
            FileDel del = new FileDel();
            del.deleteOneFile();
        }

        /// 补充公共方法-20230329
        [When(@"is connect status")]
        public void WhenIsConnectStatus(Table table)
        {
            bool Response = lenovoldppstep.IsConnect();
            Assert.AreEqual(table.Rows[0][0], Response.ToString(), "设备未成功连接（概率）");//自己决定是否加断言
        }

        //补充公共方法ping 20230329
        [When(@"get file length")]
        public void WhenGetFileLength(Table table)
        {
            lenovoldppstep.GetConnectLogLength();
        }

        [When(@"ping phone IP address")]
        public void WhenPingPhoneIPAddress(Table table)
        {
            bool Response = lenovoldppstep.PingTest();
            Assert.AreEqual(table.Rows[0][1], Response.ToString(), "响应结果与预期不同1");
        }

        [Then(@"wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr")]
        public void ThenWifidirect_ExitAndLdpp_Ap_SsidSameAsApSsidInTaskmgr(Table table)
        {
            if (bool.Parse(table.Rows[0][2]) == true)
            {
                string ssid_name = lenovoldppstep.GetSsidNameByTaskMgr();
                if (bool.Parse(table.Rows[0][1]) == true)
                {
                    Assert.AreEqual(table.Rows[0][0], ssid_name, "不一样");
                }
                else
                {
                    Assert.AreNotEqual(table.Rows[0][0], ssid_name, "一样");
                }
            }
            else
            {
                bool wifi_direct_exist = lenovoldppstep.GetWifiDirectIsExist();
                Assert.IsFalse(wifi_direct_exist, "找到了wifidirect");
            }
        }

    }
}
