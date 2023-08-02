using LDPPInterfaceTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.Xml.Linq;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using LDPP_API.Common.DCLDPP;
using LDPP_API;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Management;
//using OpenQA.Selenium.Appium.Windows;

namespace WindowsFormsApp1
{
    public class LenovoLDPPStep
    {
        AutomationElementActions m_AutomationElementActions = new AutomationElementActions();
        AutomationElementCheck m_AutomationElementCheck = new AutomationElementCheck();
        SystemControl m_SystemControl = new SystemControl();

        private static Dictionary<string, DeviceInfo> devInfoMap = new Dictionary<string, DeviceInfo>();
        public static string BLE;
        public static string BT;
        public static string Name;
        private static long _startLocation = 0;        // 开始监听文件的起始位置
        private static string _sWatchPath = "";
        public static long _startLocationDiscover = 0;
        DeviceInfo devInfo = new DeviceInfo();
        public static long _startLocationConnect = 0;
        public static string _sDeviceIP = string.Empty;

        //read task .ini file
        public string ReadTaskFile()
        {
            //taskid
            string taskId = CommonHelper.ReadIniValue("Task", "task_id", FileConstants.TaskConfigFile);
            Name = CommonHelper.ReadIniValue("Task", "device_name", FileConstants.TaskConfigFile);
            
            return Name;
        }

        /// <summary>
        /// get log path  获取log文件路径
        /// </summary>
        /// <returns></returns>
        public string GetDiscoverLogFileDir()
        {
            string logDir = "";
            try
            {
                logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lenovo", "Unity");    //2. user permission (%localappdata%)
            }
            catch (Exception)
            {

                logDir = @"C:/temp/";
            }

            string[] strDir = new string[2] { "LDPPData", "log" };

            for (int i = 0; i < strDir.Length; i++)
            {
                logDir = Path.Combine(logDir, strDir[i]);
            }
            return logDir;
        }

        /// <summary>
        /// 获取文件的长度，从最后开始读取文件内容
        /// </summary>
        /// <returns></returns>
        public long initFileInfo()
        {
            long _startLocation = 0;
            _sWatchPath = GetDiscoverLogFileDir();

            // 获取文件的长度，从最后开始读取文件内容
            FileInfo logFileInfo = new FileInfo(_sWatchPath + "\\Discover_trace.log");

            if (!logFileInfo.Exists)
            {
                _startLocation = 0;
                logFileInfo.Create(); // 创建文件
            }
            else
            {
                _startLocation = logFileInfo.Length;
            }

            return _startLocation;
        }
        //get device BT BLE
        public void getdeviceinfo(long location)
        {
            //读取文件
            ReadTaskFile();
            _sWatchPath = GetDiscoverLogFileDir();
            ReadFile("FindBLE", location);
        }

        public void InstallUnitySampleAppWindows(string exePath)
        {
            if (File.Exists(exePath))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exePath);
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                //Console.WriteLine("Hello, World!");
            }

        }

        public void SiginInUnitySampleAppWindows()
        {
            //var m_Aut = m_AutomationElementCheck.GetRootElement("UnitySampleApp");//获取窗体
            var SignInButton = m_AutomationElementCheck.GetAutomationElement("UnitySampleApp", "", "Button", "Sign in");
            string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(SignInButton, 4);

            if ("Enable" == TypeDefault)
            {
                //点击鼠标
                m_AutomationElementActions.InvokeByAutomationElement(SignInButton);
            }
            else
            {

            }
            //获取数值


        }
        public void SiginOutUnitySampleAppWindows()
        {
            //var m_Aut = m_AutomationElementCheck.GetRootElement("UnitySampleApp");//获取窗体
            var SignoutButton = m_AutomationElementCheck.GetAutomationElement("UnitySampleApp", "", "Button", "Sign out");
            string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(SignoutButton, 4);

            if ("Enable" == TypeDefault)
            {
                //点击鼠标
                m_AutomationElementActions.InvokeByAutomationElement(SignoutButton);
            }
            else
            {

            }
            //获取数值


        }
        public void TestUnitySampleAppWindows()
        {
            var TestComboBox = m_AutomationElementCheck.GetAutomationElement("UnitySampleApp", "", "ComboBox", "");

            Thread.Sleep(1000);
            //{l:229 t:1170 r:510 b:1213}
            double Y = TestComboBox.Current.BoundingRectangle.Location.Y;
            double X = TestComboBox.Current.BoundingRectangle.Location.X;

            Y = TestComboBox.GetClickablePoint().Y;
            X = TestComboBox.GetClickablePoint().X;

            double Width = TestComboBox.Current.BoundingRectangle.Width;
            double Height = TestComboBox.Current.BoundingRectangle.Height;
            //double x11 = X + Width / 2;
            double x11 = X;
            double y12 = Y;

            double wy = y12 + Height;
            double wb = y12 + 2 * Height;

            int i = 0;
            while (true)
            {
                i++;
                //LogWatcher.WriteInfoLog("i：" + i + "\n");
                bool blew = m_SystemControl.WindowIntPtrIsExistByName("Plugin name: LDPP.BLE.Discover");
                bool wifi = m_SystemControl.WindowIntPtrIsExistByName("Plugin name: LDPP.WiFi.Connect");

                //LogWatcher.WriteInfoLog("blew：" + blew + "\n");
                //LogWatcher.WriteInfoLog("wifi：" + wifi + "\n");

                if (blew == true)
                {
                    const int WM_CLOSE = 0x10;
                    IntPtr ParenthWnd = new IntPtr(0);
                    ParenthWnd = m_SystemControl.GetWindowhwd("Plugin name: LDPP.BLE.Discover", 0);
                    SystemControl.SendMessage(ParenthWnd, WM_CLOSE, 0, 0);
                    blew = false;
                }
                if (wifi == true)
                {
                    const int WM_CLOSE = 0x10;
                    IntPtr ParenthWnd = new IntPtr(0);
                    ParenthWnd = m_SystemControl.GetWindowhwd("Plugin name: LDPP.WiFi.Connect", 0);
                    SystemControl.SendMessage(ParenthWnd, WM_CLOSE, 0, 0);
                    wifi = false;
                }

                m_AutomationElementCheck.ComboBoxExpandElement(TestComboBox);

                Thread.Sleep(1000);
                AutomationElementCheck.SetCursorPos((int)x11, (int)wy);
                AutomationElementCheck.DoMouseClick((int)x11, (int)wy);
                AutomationElementCheck.DoMouseClick((int)x11, (int)wy);
                Thread.Sleep(1000);
                //按钮状态
                var TestButton = m_AutomationElementCheck.GetAutomationElement("UnitySampleApp", "", "Button", "Test");
                Thread.Sleep(2000);
                TestButton.SetFocus();
                Thread.Sleep(1000);
                m_AutomationElementActions.InvokeByAutomationElement(TestButton);
                Thread.Sleep(3000);
                m_AutomationElementCheck.ComboBoxExpandElement(TestComboBox);
                Thread.Sleep(1000);
                AutomationElementCheck.SetCursorPos((int)x11, (int)wb);
                AutomationElementCheck.DoMouseClick((int)x11, (int)wb);
                AutomationElementCheck.DoMouseClick((int)x11, (int)wb);
                Thread.Sleep(1000);
                //按钮状态
                TestButton = m_AutomationElementCheck.GetAutomationElement("UnitySampleApp", "", "Button", "Test");
                Thread.Sleep(2000);
                TestButton.SetFocus();
                Thread.Sleep(1000);
                m_AutomationElementActions.InvokeByAutomationElement(TestButton);
                Thread.Sleep(3000);


                blew = m_SystemControl.WindowIntPtrIsExistByName("Plugin name: LDPP.BLE.Discover");
                wifi = m_SystemControl.WindowIntPtrIsExistByName("Plugin name: LDPP.WiFi.Connect");
                if (blew == true && wifi == true)
                {
                    break;
                }


                if (i > 5)
                {
                    break;
                }

            }
        }
        public string ConnectStartUnitySampleAppWindows()
        {
            string textdataStart;
            string textdata;
            var ConnectStartText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.WiFi.Connect", "", "TextBox", "Window");
            Thread.Sleep(1000);
            // 获取文本框数值
            textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStartText[3]);
            

            while (textdataStart == null)
            {
                Thread.Sleep(1000);
                textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStartText[3]);

                if (textdataStart != "")
                {
                    break;
                }
            }
            int startlocation = textdataStart.Length;


            var StartConnectButton = m_AutomationElementCheck.GetAutomationElement("Plugin name: LDPP.WiFi.Connect", "", "Button", "Start");

            string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartConnectButton, 4);
            if ("Enable" == TypeDefault)
            {
                //点击鼠标
                m_AutomationElementActions.InvokeByAutomationElement(StartConnectButton);
            }
            else
            {
                return "Please confirm the button status ";
            }

            TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartConnectButton, 4);

            while ("Disable" != TypeDefault)
            {
                Thread.Sleep(1000);

                TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartConnectButton, 4);

                if ("Disable" == TypeDefault)
                {
                    break;
                }
            }
            Thread.Sleep(1000);
            //获取文本框的data
            ///ConnectStartText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.WiFi.Connect", "", "TextBox", "");
            textdata = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStartText[3]);

            while (textdata == textdataStart)
            {
                Thread.Sleep(1000);
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStartText[3]);

                if (textdata != textdataStart)
                {
                    break;
                }
            }
            int stoplocation = textdata.Length;

            textdata = textdata.Substring(startlocation, (stoplocation - startlocation));


            return ResponseMessageProcess(textdata);
        }
        public string ConnectStopUnitySampleAppWindows()
        {
            string textdata;
            string textdataStart;
            var ConnectStopText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.WiFi.Connect", "", "TextBox", "");
            Thread.Sleep(1000);
            // 获取文本框数值
            textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStopText[3]);
            Thread.Sleep(1000);
            while (textdataStart == null)
            {
                Thread.Sleep(1000);
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStopText[3]);

                if (textdataStart != "")
                {
                    break;
                }
            }

            int startlocation = textdataStart.Length;

            var StopConnectButton = m_AutomationElementCheck.GetAutomationElement("Plugin name: LDPP.WiFi.Connect", "", "Button", "Stop");
            Thread.Sleep(1000);
            string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StopConnectButton, 4);
            if ("Enable" == TypeDefault)
            {
                //点击鼠标
                m_AutomationElementActions.InvokeByAutomationElement(StopConnectButton);
            }
            else
            {
                return "Please confirm the button status ";
            }
            Thread.Sleep(1000);
            TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StopConnectButton, 4);
           
            while ("Disable" != TypeDefault)
            {
                Thread.Sleep(1000);

                TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StopConnectButton, 4);

                if ("Disable" == TypeDefault)
                {
                    break;
                }
            }
            //获取文本框的data
            //ConnectStopText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.WiFi.Connect", "", "TextBox", "");

            textdata = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStopText[3]);
            while (textdata == textdataStart)
            {
                Thread.Sleep(1000);
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(ConnectStopText[3]);

                if (textdata != textdataStart)
                {
                    break;
                }
            }
            int stoplocation = textdata.Length;

            textdata = textdata.Substring((startlocation), (stoplocation - startlocation));

            return ResponseMessageProcess(textdata);
        }

        public string ConnectSendUnitySampleAppWindows(string textName, string RequestText)
        {
            try
            {
                string textdata = string.Empty;
                string textdataStart;
                var SendConnectText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.WiFi.Connect", "", "TextBox", "");
                Thread.Sleep(1000);

                textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(SendConnectText[3]);
                while (textdataStart == null)
                {
                    Thread.Sleep(1000);
                    textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(SendConnectText[3]);

                    if (textdataStart != "")
                    {
                        break;
                    }
                }

                int startlocation = textdataStart.Length;

                //文本框输入内容
                m_AutomationElementActions.SetTextByAutomationElement(SendConnectText[1], textName);
                m_AutomationElementActions.SetTextByAutomationElement(SendConnectText[2], RequestText);
                Thread.Sleep(1000);
                var SendConnectButton = m_AutomationElementCheck.GetAutomationElement("Plugin name: LDPP.WiFi.Connect", "", "Button", "Send");
                Thread.Sleep(1000);

                string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(SendConnectButton, 4);
//                if ("Enable" == TypeDefault)
  //              {
                    //点击鼠标
                    m_AutomationElementActions.InvokeByAutomationElement(SendConnectButton);
 //               }
//                else
//                {
//                    return "Please confirm the button status ";
//                }
                TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(SendConnectButton, 4);
                Thread.Sleep(2000);

                //获取文本框数值
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(SendConnectText[3]);
                Thread.Sleep(1000);
                while (textdata == textdataStart)
                {
                    Thread.Sleep(1000);
                    textdata = m_AutomationElementActions.ReadTextByAutomationElement(SendConnectText[3]);

                    if (textdata != textdataStart)
                    {
                        break;
                    }
                }

                int stoplocation = textdata.Length;

                textdata = textdata.Substring(startlocation, (stoplocation - startlocation));


                return ResponseMessageProcess(textdata);

            }
            catch (Exception ex)
            {           
                Console.WriteLine(ex.ToString());
                return ex.ToString();
            }
            
        }

        public string  DiscoverStartUnitySampleAppWindows()
        {
            string textdata = string.Empty;
            string textdataStart = string.Empty;
            var StartDiscoverText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.BLE.Discover", "", "TextBox", "");
            Thread.Sleep(1000);
            textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(StartDiscoverText[3]);

            while (textdataStart == null)
            {
                Thread.Sleep(1000);
                textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(StartDiscoverText[3]);

                if (textdataStart != "")
                {
                    break;
                }
            }

            int startlocation = textdataStart.Length;

            var StartDiscoverButton = m_AutomationElementCheck.GetAutomationElement("Plugin name: LDPP.BLE.Discover", "", "Button", "Start");
            string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartDiscoverButton, 4);
            //if ("Enable" == TypeDefault)
            //{
            //    //点击鼠标
            //    m_AutomationElementActions.InvokeByAutomationElement(StartDiscoverButton);
            //}
            //else
            //{
            //    return "Please confirm the button status ";
            //}

            //点击鼠标
            m_AutomationElementActions.InvokeByAutomationElement(StartDiscoverButton);
            //TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartDiscoverButton, 4);
            while ("Disable" != TypeDefault)
            {
                Thread.Sleep(1000);

                TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartDiscoverButton, 4);

                if ("Disable" == TypeDefault)
                {
                    break;
                }
            }
            Thread.Sleep(2000);
            //获取文本框的data
            //StartDiscoverText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.BLE.Discover", "", "TextBox", "");
            textdata = m_AutomationElementActions.ReadTextByAutomationElement(StartDiscoverText[3]);
            Thread.Sleep(1000);
            while (textdata == textdataStart)
            {
                Thread.Sleep(1000);
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(StartDiscoverText[3]);

                if (textdata != textdataStart)
                {
                    break;
                }
            }
            int stoplocation = textdata.Length;
            textdata = textdata.Substring((startlocation), (stoplocation - startlocation));

            return ResponseMessageProcess(textdata);
        }
        public string DiscoverStopUnitySampleAppWindows()
        {
            string textdata = string.Empty;
            string textdataStart = string.Empty;

            var StopDiscoverText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.BLE.Discover", "", "TextBox", "");
            Thread.Sleep(1000);
            textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(StopDiscoverText[3]);
            while (textdataStart == null)
            {
                Thread.Sleep(1000);
                textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(StopDiscoverText[3]);

                if (textdataStart != "")
                {
                    break;
                }
            }
            int startlocation = textdataStart.Length;

            var StartDiscoverButton = m_AutomationElementCheck.GetAutomationElement("Plugin name: LDPP.BLE.Discover", "", "Button", "Stop");
            string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartDiscoverButton, 4);

            //if ("Enable" == TypeDefault)
            //{
            //    //点击鼠标
            //    m_AutomationElementActions.InvokeByAutomationElement(StartDiscoverButton);
            //}
            //else
            //{
            //    return "Please confirm the button status ";
            //}
            //点击鼠标
            m_AutomationElementActions.InvokeByAutomationElement(StartDiscoverButton);
            Thread.Sleep(1000);
            TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartDiscoverButton, 4);
            while ("Disable" != TypeDefault)
            {
                Thread.Sleep(1000);

                TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(StartDiscoverButton, 4);

                if ("Disable" == TypeDefault)
                {
                    break;
                }
            }
            Thread.Sleep(2000);
            //获取文本框的data
            //StopDiscoverText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.BLE.Discover", "", "TextBox", "");
            textdata = m_AutomationElementActions.ReadTextByAutomationElement(StopDiscoverText[3]);
            Thread.Sleep(1000);
            while (textdata == textdataStart)
            {
                Thread.Sleep(1000);
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(StopDiscoverText[3]);

                if (textdata != textdataStart)
                {
                    break;
                }
            }
            int stoplocation = textdata.Length;
            textdata = textdata.Substring((startlocation), (stoplocation - startlocation));

            return ResponseMessageProcess(textdata);
        }

        public string DiscoverSendUnitySampleAppWindows(string textName,string RequestText)
        {
            try
            {
                string textdata = string.Empty;
                string textdataStart = string.Empty;
                string ResponseText  = string.Empty;
                var SendDiscoverText = m_AutomationElementCheck.GetAutomationElementCollection("Plugin name: LDPP.BLE.Discover", "", "TextBox", "");
                Thread.Sleep(3000);
                // 获取文本框数值
                textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(SendDiscoverText[3]);
                //Thread.Sleep(1000);
                while (textdataStart == null)
                {
                    Thread.Sleep(1000);
                    textdataStart = m_AutomationElementActions.ReadTextByAutomationElement(SendDiscoverText[3]);

                    if (textdataStart != "")
                    {
                        break;
                    }
                }
                int startlocation = textdataStart.Length;

                //输入文本框内容
                m_AutomationElementActions.SetTextByAutomationElement(SendDiscoverText[1], textName);
                m_AutomationElementActions.SetTextByAutomationElement(SendDiscoverText[2], RequestText);
                Thread.Sleep(1000);

                var SendDiscoverButton = m_AutomationElementCheck.GetAutomationElement("Plugin name: LDPP.BLE.Discover", "", "Button", "Send");
                //string TypeDefault = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(SendDiscoverButton, 4);
                //if ("Enable" == TypeDefault)
               // {
                    //点击鼠标
                    m_AutomationElementActions.InvokeByAutomationElement(SendDiscoverButton);
               // }
                //else
               // {
                //    return "Please confirm the button status ";
               // }
                if (textName == "ReadyForLink")
                {
                    Thread.Sleep(6000);
                }
                Thread.Sleep(3000);

                //获取文本框数值
                textdata = m_AutomationElementActions.ReadTextByAutomationElement(SendDiscoverText[3]);
                Thread.Sleep(1000);
                while (textdata == textdataStart)
                {
                    Thread.Sleep(1000);
                    textdata = m_AutomationElementActions.ReadTextByAutomationElement(SendDiscoverText[3]);

                    if (textdata != textdataStart)
                    {
                        break;
                    }
                }
                int stoplocation = textdata.Length;
                textdata = textdata.Substring((startlocation), (stoplocation - startlocation));

                LogOut.WriteInfoLog("本次指令获取textdata :" + textdata);
   //             textdata = "========================================================\r\n\r\nPluginNotifierEventLDPPHandler : \r\n\r\nPluginNotifierEventLDPP => {\"CorrelationId\":\"\",\"EventId\":\"\",\"PluginId\":\"8EB25AF2-945D-46EE-B727-42E56EEA1B28\",\"Context\":\"{\"Name\":\"DeviceInfoAdd\",\"Param\":\"{\\\"Status\\\": 0, \\\"Name\\\": \\\"123\\\", \\\"BLE\\\": \\\"133543532277128\\\", \\\"BT\\\": \\\"53342733859872\\\"}\"}\"}\r\n\r\n========================================================\r\n\r\nPluginCommandResponseEvent => {\"Code\":\"OK\",\"CorrelationId\":\"05906eac-792f-46df-bed5-fac66d7c5259\",\"EventId\":\"PluginCommandResponseEvent\",\"PluginId\":\"8EB25AF2-945D-46EE-B727-42E56EEA1B28\",\"Context\":\"{\"ResponseCode\" : \"4\", \"Message\" : \"COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR\"}\"}";
                //获取字符串处理-先进行分割
                //按照======分割
                if (textdata.Contains("========================================================"))
                {
                    string[] sArray  =  Regex.Split(textdata, "========================================================", RegexOptions.IgnoreCase);

                    //遍历所有数组，查看是否包含ResponseCode，如果包含则获取相应的字段
                    foreach (var item in sArray)
                    {
                        if (item.Contains("ResponseCode") == true)
                        {
                            string[] sArray1 = Regex.Split(item, ",\"Context\":", RegexOptions.IgnoreCase);
                            string[] sArray2 = Regex.Split(sArray1[1], "\r\n\r\n", RegexOptions.IgnoreCase);

                            ResponseText =  sArray2[0].Substring(0, (sArray2[0].Length - 1));
                        }
                    }
                }
                else 
                {
                    if (textName == "ReadyForLink" && RequestText == "{}")
                    {
                        if (textdata.Contains("\"{\"ResponseCode\" : \"5\", \"Message\" : \"PREPARE_LINK_JSON_NOTIFY_DEVICE_EMPTY\"}\" "))
                        {
                            ResponseText = "\"{\"ResponseCode\" : \"5\", \"Message\" : \"PREPARE_LINK_JSON_NOTIFY_DEVICE_EMPTY\"}\" ";
                        }
                        else if (textdata.Contains("\"{\"ResponseCode\" : \"6\", \"Message\" : \"BLE_SMS_ERROR_UNSTART_DISCOVER\"}\""))
                        {
                            ResponseText = "\"{\"ResponseCode\" : \"6\", \"Message\" : \"BLE_SMS_ERROR_UNSTART_DISCOVER\"}\"";
                        }
                        else
                        {
                            ResponseText = ResponseMessageProcess(textdata);
                        }
                    }
                    else
                    {
                        ResponseText = ResponseMessageProcess(textdata);
                    }
                }             
                
                return ResponseText;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.ToString();
            }
            
        }

        public void CloseConnectWindow()
        {
            IntPtr hWnd = FindWindow(null, "Plugin name: LDPP.WiFi.Connect");
            SendMessage(hWnd, 0x10, 0, 0);
            Thread.Sleep(1000);
        }
        public void CloseDiscoverWindow()
        {
            IntPtr hWnd = FindWindow(null, "Plugin name: LDPP.BLE.Discover");
            SendMessage(hWnd, 0x10, 0, 0);
            Thread.Sleep(1000);
        }
        public void CloseunityWindow()
        {
            IntPtr hWnd = FindWindow(null, "UnitySampleApp");
            SendMessage(hWnd, 0x10, 0, 0);
            Thread.Sleep(1000);
        }
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lp1, string lp2);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        //获取文本款最后一行
        //Response: {"ResponseCode" : "0", "Message" : "COMMON_OK"} 
        //{"ResponseCode" : "0", "Message" : "START_OK"}"}

        public string ResponseMessageProcess(string textdata)
        {
            //textdata = "PluginCommandResponseEvent => {\"Code\":\"OK\",\"CorrelationId\":\"6c2b2eaf-ac0b-4c38-8066-f0256de354f6\",\"EventId\":\"PluginCommandResponseEvent\",\"PluginId\":\"8EB25AF2-945D-46EE-B727-42E56EEA1B28\",\"Context\":\"{\"ResponseCode\" : \"0\", \"Message\" : \"START_OK\"}\"}";
            //textdata = "PluginCommandResponseEvent => {\"Code\":\"OK\",\"CorrelationId\":\"110c1982-5b5e-4edf-8e12-5818e4cf2a00\",\"EventId\":\"PluginCommandResponseEvent\",\"PluginId\":\"AF61377A-04BF-4DF6-8966-29ACD17F53F6\",\"Context\":\"{\"ResponseCode\" : \"0\", \"Message\" : \"COMMON_OK\"}\"}";
            //字符串拆分
            if (textdata != "")
            {
                string[] sArray1 = Regex.Split(textdata, ",\"Context\":", RegexOptions.IgnoreCase);
                string[] sArray = Regex.Split(sArray1[1], "\r\n\r\n", RegexOptions.IgnoreCase);

                return sArray[0].Substring(0, (sArray[0].Length - 1));

            }
            else
            {
                return "fasle";           
            }
            
        }

        //CreateAP 随机生成账号、密码
        public string AutoGenPWD()
        {

            //SSID = generator.RandomString(8) + "_" + generator.RandomString(5, true);
            string PWD = RandomPassword();
            return PWD;
        }
        public string AutoGenSSID()
        {
            string SSID =RandomString(8) + "_" + RandomString(5, true);
            return SSID;
        }

        // Instantiate random number generator.  
        // It is better to keep a single Random instance and keep using Next on the same instance.  
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // Generates a random password:4-LowerCase + 4-Digits + 2-UpperCase  
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();
            passwordBuilder.Append(RandomString(4, true));
            passwordBuilder.Append(RandomNumber(1000, 9999));
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }

        public IntPtr GetWindowHandleByProcessName(string pName)
        {
            try
            {
                Process[] temp = Process.GetProcessesByName(pName);//在所有已启动的进程中查找需要的进程；                    
                if (temp.Length > 0)//如果查找到                    
                {                    
                                     
                    return temp[0].MainWindowHandle;               
                }           
            }            
            catch (Exception ex)            
            {                
            }           
            return IntPtr.Zero;        
        }

       

        public static DeviceInfo GetDeviceInfo()
        {
            if (devInfoMap.ContainsKey(Name))
            {
                return devInfoMap[Name];
            }

            return null;
        }

        //当文件不操作时，也要读取文件20221223
        public void ReadFile(string _judgeFlag,long _startLocationDiscover)
        {
            FileStream fs = new FileStream(_sWatchPath + "\\" + "\\Discover_trace.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            //FileStream fs = new FileStream(_sWatchPath + "\\" + _sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            fs.Seek(_startLocationDiscover, SeekOrigin.Begin);       // 跳转到需要开始读的地方，第一次的时候，是从文件末尾开始读
            StreamReader sr = new StreamReader(fs);

            try
            {
                string tempStr = string.Empty;
                while (!sr.EndOfStream)
                {                   // 如果没有到文件末尾，则一直读
                    tempStr = sr.ReadLine();                // 读取一行文件内容
                    if (tempStr == null)
                    {
                        // null保护
                        continue;
                    }
                    // 将读出来的一行内容长度进行累加，方便第二次读取
                    // +1 是因为每一行都有一个换行符
                    _startLocation += tempStr.Length + 1;
                    //WriteInfoLog(tempStr);

                    if (_judgeFlag == "FindBLE")
                    {
                        // 如果判断标记是1，那么进行判断
                        if (tempStr != null && tempStr.Contains("Find Ble Device :") && tempStr.Contains(Name))
                        {
                            Console.WriteLine("find:::::::" + tempStr);

                            // 如果满足条件，则将监听标志设置为false，停止文件监听，并跳出循环，停止读取文件内容
                            //这行代码处理，获取设备名称
                            string[] line_array = tempStr.Split(new char[2] { '[', ']' });
                            string[] time_array = line_array[1].Split(' ');

                            //DeviceInfo devInfo = new DeviceInfo();
                            devInfo.BLE = line_array[15];
                            devInfo.BT = line_array[17];
                            devInfo.Name = line_array[19];

                           

                            if (devInfo.Name.Equals(Name))
                            {
                                //WriteInfoLog("find Ble Device： " + _sDeviceName);
                                if (devInfoMap.ContainsKey(devInfo.Name) == false)
                                {
                                    devInfoMap.Add(devInfo.Name, devInfo);
                                }
                                else
                                {
                                    devInfoMap[devInfo.Name] = devInfo;
                                }                         
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 出现任何异常的处理，防止程序崩溃
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                // 方法退出前关闭文件流
                sr.Close();
                fs.Close();
                fs.Dispose();
            }           
            return;

        }


        ///////公共方法/////////////////////

        // <summary>获取指定位置 蓝牙/Wifi/移动热点 根元素</summary>
        /// <param name="desc_name">mobilehotspot/wifi/bluetooth</param>
        /// <returns></returns>
        public AutomationElement GetWindowsSettingsElement(string desc_name)
        {
            switch (desc_name)
            {
                case "mobilehotspot":
                    Process.Start("ms-settings:network-mobilehotspot");
                    break;
                case "wifi":
                    Process.Start("ms-settings:network-wifi");
                    break;
                case "bluetooth":
                    Process.Start("ms-settings:bluetooth");
                    break;
            }
            Thread.Sleep(5000);
            return m_AutomationElementCheck.GetRootElement("设置");
        }

        /// <summary>打开/关闭 蓝牙</summary>
        /// <param name="isOn"></param>
        public void SetBluetoothToggle(bool isOn = false)
        {
            string id = "SystemSettings_Device_BluetoothRadioToggle_ToggleSwitch";
            string class_name = "ToggleSwitch";
            AutomationElement element = null;
            element = GetWindowsSettingsElement("bluetooth");
            AutomationElement blue_element = m_AutomationElementActions.FindElementByClasssNameAndAutomationID(element, id, class_name);
            m_AutomationElementActions.SetToggleStateByAutomationElement(blue_element, isOn);
            Thread.Sleep(2000);
            RunCmd("taskkill /f /im SystemSettings.exe");
            Thread.Sleep(4000);
        }

        /// <summary>打开/关闭 Wifi/移动热点</summary>
        /// <param name="isOn"></param>
        public void SetWifiToggle(bool isOn = false)
        {
            string id = "SystemSettings_Connections_Adapter_Wi-Fi_WLAN_RadioToggle";
            string class_name = "ToggleSwitch";
            AutomationElement element = null;
            element = GetWindowsSettingsElement("wifi");
            AutomationElement blue_element = m_AutomationElementActions.FindElementByClasssNameAndAutomationID(element, id, class_name);
            m_AutomationElementActions.SetToggleStateByAutomationElement(blue_element, isOn);
            Thread.Sleep(2000);
            RunCmd("taskkill /f /im SystemSettings.exe");
            Thread.Sleep(4000);
        }

        /// <summary>打开/关闭 移动热点</summary>
        /// <param name="isOn"></param>
        public void SetMobilehotspotToggle(bool isOn = false)
        {
            string id = "SystemSettings_Connections_InternetSharingEnabled_ToggleSwitch";
            string class_name = "ToggleSwitch";
            AutomationElement element = null;
            element = GetWindowsSettingsElement("mobilehotspot");
            AutomationElement blue_element = m_AutomationElementActions.FindElementByClasssNameAndAutomationID(element, id, class_name);
            m_AutomationElementActions.SetToggleStateByAutomationElement(blue_element, isOn);
            RunCmd("taskkill /f /im SystemSettings.exe");
            Thread.Sleep(2000);
        }

        /// <summary>开关 移动热点 </summary>
        /// <param name="isOn"></param>
        public void SethotspotToggle(bool isOn = false)
        {
            string id = "SystemSettings_Connections_InternetSharingEnabled_ToggleSwitch";
            string class_name = "ToggleSwitch";
            AutomationElement element = null;
            element = GetWindowsSettingsElement("mobilehotspot");
            AutomationElement blue_element = m_AutomationElementActions.FindElementByClasssNameAndAutomationID(element, id, class_name);
            m_AutomationElementActions.SetToggleStateByAutomationElement(blue_element, isOn);
            RunCmd("taskkill /f /im SystemSettings.exe");
            Thread.Sleep(2000);
        }

        /// 连接wifi
        public void ConnectWifi(string wifiname)
        {
            RunCmd("Netsh wlan connect " + wifiname);
            Thread.Sleep(5000);
        }
        public void disconnectWifi()
        {

            RunCmd("netsh wlan disconnect " );
            Thread.Sleep(5000);

        }

        #region Runcmd
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

        #endregion

        ////（3）判断是否连接成功///////////////
        public long initFileInfo(string filename)
        {
            long _startLocation = 0;
             _sWatchPath = GetDiscoverLogFileDir();

            // 获取文件的长度，从最后开始读取文件内容
            FileInfo logFileInfo = new FileInfo(_sWatchPath + "\\" + filename);

            if (!logFileInfo.Exists)
            {
                _startLocation = 0;
                logFileInfo.Create(); // 创建文件
            }
            else
            {
                _startLocation = logFileInfo.Length;
            }

            return _startLocation;
        }

        //调用函数1---获取文件位置，在readforlink之前调用次函数(3)(4)共用此方法
        public void GetConnectLogLength()
        {
            string ConnectPath = "Connect_trace.log";

            //获取当前文件位置
            _startLocationConnect = initFileInfo(ConnectPath);

        }

        //(3)判断文件中是否存在insert new device info
        public bool IsConnect()
        {
            _sWatchPath = GetDiscoverLogFileDir();
            FileStream fs = new FileStream(_sWatchPath + "\\Connect_trace.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs.Seek(_startLocationConnect, SeekOrigin.Begin);       
            StreamReader sr = new StreamReader(fs);
            try
            {
                string tempStr = string.Empty;
                while (!sr.EndOfStream)
                {                   
                    tempStr = sr.ReadLine();                
                    if (tempStr == null)
                    {
                        continue;
                    }

                    _startLocationConnect += tempStr.Length + 1;

                    if (tempStr != null && tempStr.Contains("new count = 1"))//insert new device info
                    {
                        //获取设备的IP地址
                        string[] line_array = tempStr.Split(new char[2] { '[', ']' });
                        _sDeviceIP = line_array[13];

                    }
                    if (tempStr.Contains("insert new device info") == true)
                    {
                        return true;
                    }                                    

                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
                
            }
            finally
            {
                sr.Close();
                fs.Close();
                fs.Dispose();
            }
         //   
        }


        ////（4）判断设备是否断开连接，可以在连接成功时获取设备ip,如果设备ping不通，则设备断开连接///////////////
        public void getDeviceIP()
        {
            _sWatchPath = GetDiscoverLogFileDir();
            FileStream fs = new FileStream(_sWatchPath + "\\Connect_trace.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs.Seek(_startLocationConnect, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(fs);
            try
            {
                string tempStr = string.Empty;
                while (!sr.EndOfStream)
                {
                    tempStr = sr.ReadLine();
                    if (tempStr == null)
                    {
                        continue;
                    }

                    _startLocationConnect += tempStr.Length + 1;

                    if (tempStr != null && tempStr.Contains("new count = 1"))//insert new device info
                    {
                        //获取设备的IP地址
                        string[] line_array = tempStr.Split(new char[2] { '[', ']' });
                        _sDeviceIP = line_array[13];                       

                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sr.Close();
                fs.Close();
                fs.Dispose();
            }

        }
        public bool PingTest()
        {
            getDeviceIP();

            Process myProcess = new Process();
            myProcess.StartInfo.FileName = "cmd.exe"; //设定程序名
            myProcess.StartInfo.UseShellExecute = false; //关闭shell的使用           
            //要重定向 IO 流，Process 对象必须将 UseShellExecute 属性设置为 False。            
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            string pingstr;
            myProcess.Start();
            myProcess.StandardInput.WriteLine("ping " + _sDeviceIP);
            myProcess.StandardInput.WriteLine("exit");
            string strRst = myProcess.StandardOutput.ReadToEnd();
            //WriteLog(strRst);            
            if (strRst.Contains("字节") && strRst.Contains("时间") && strRst.Contains("TTL"))
            {
                return true;
            }
            else if (strRst.Contains("bytes") && strRst.Contains("time") && strRst.Contains("TTL"))
            {
                return true;
            }
            else if (strRst.Contains("无法访问目标主机") || strRst.Contains("Destination host unreachable."))
            {
                pingstr = "无法到达主机";
                return false;
            }
            else if (strRst.Contains("请检查该名称") || strRst.Contains("UnKonw host"))
            {
                pingstr = "无法解析主机";
                return false;
            }
            else if (strRst.Contains("请求超时"))
            {
                pingstr = "请求超时";
                return false;
            }
            else
            {
                return false;
            }

            myProcess.Close();

        }


        public string GetSsidNameByTaskMgr()
        {
            string ssid_name = null;
            RunCmd("taskmgr");
            Thread.Sleep(5000);
            AutomationElement taskmgr_window = m_AutomationElementCheck.GetRootElement("任务管理器");

            //string performance_id = "Microsoft.UI.Xaml.Controls.NavigationViewItem";//"MenuItemsHost";//"Microsoft.UI.Xaml.Controls.NavigationViewItem";

            string btn_ap_id = "net_entry_1";
            string btn_ap_class_name = "accessiblebutton";

            string ap_id = "wifi";
            string ap_class_name = "Element";
            //AutomationElement element = null;
            //element = m_AutomationElementActions.FindElementByAutomationID(taskmgr_window,performance_id)[0];
            //AutomationElementCheck.DoMouseClick((int)element.GetClickablePoint().X,(int)element.GetClickablePoint().Y);
            //m_AutomationElementActions.GetPointByAutomationElement(element);
            //Thread.Sleep(2000);

            AutomationElement btn_ap_element = m_AutomationElementActions.FindElementByClasssNameAndAutomationID(taskmgr_window, btn_ap_id, btn_ap_class_name);
            m_AutomationElementActions.InvokeByAutomationElement(btn_ap_element);
            Thread.Sleep(2000);

            AutomationElement ap_element = m_AutomationElementActions.FindElementByClasssNameAndAutomationID(taskmgr_window, ap_id, ap_class_name);
            //ssid_name = m_AutomationElementActions.GetTypeStringByIntFromAutomationElement(ap_element,0);
            Thread.Sleep(2000);
            ssid_name = ap_element.Current.Name;
            Thread.Sleep(5000);
            RunCmd("taskkill /f /im taskmgr.exe");
            Thread.Sleep(4000);
            return ssid_name;
        }

        public bool GetWifiDirectIsExist(string keywords = "Wi-Fi Direct Virtual Adapter #2")
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkAdapter");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_NetworkAdapter instance");
                    //Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Name: {0}", queryObj["Name"]);
                    if (queryObj["Name"].ToString().Contains(keywords) && bool.Parse(queryObj["PhysicalAdapter"].ToString()) == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (ManagementException e)
            {
                //MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
                return false;
            }
        }

    }

   
}
