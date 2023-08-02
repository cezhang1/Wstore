using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LDPP_API
{
    internal class Start
    {
        const int WM_CLOSE = 0x10;
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetClassName")]
        public static extern void GetClassName(IntPtr hwndParent, System.Text.StringBuilder CName, int len);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwndParent);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow")]
        public extern static bool DestroyWindow(IntPtr HW);

        public string ExePath = "C:\\API\\unityLDPP\\Windows\\agent\\UnitySampleAppWindows\\bin\\x64\\Debug\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe";
        public string ConfigPath = "C:\\LenovoSmartEngineClient\\Config\\task_conf.ini";

        public string INIRead(string section, string key, string path)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=配置节点名称，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();

        }

        public bool StartExe(string ExePath)
        {
            Process.Start(ExePath);
            IntPtr mW = FindWindow(null, "UnitySampleApp");
            Thread.Sleep(2000);
            if (mW != IntPtr.Zero)
            {
                Console.WriteLine("Success Start");
                return true;
            }
            else 
            {
                Console.WriteLine("Fail to Start");
                return false;
            }
        }

        public void InitExe(FormControl FC)
        {
            IntPtr mW = FindWindow(null, "UnitySampleApp");
            if (mW != IntPtr.Zero)
            {

                //FormControl FC = new FormControl();
                FC.Init();
                FC.SetDeviceName(INIRead("Task", "device_name", ConfigPath));

                IntPtr hwndParent3 = FindWindow(null, "UnitySampleApp");
                SetForegroundWindow(hwndParent3);
                //HecPlatform.API.App.Types.PluginListEntry
                FC.FindAllBox("UnitySampleApp");
                (double, double) RectCorner1 = FC.GetRectCorner("Browse to add");
                FC.CilckMouseOnPoint(RectCorner1);
                Thread.Sleep(2000);

                FC.FindAllBox("UnitySampleApp");

                if (FC.BoxIndex.ContainsKey("ldpp-plugin-0.0.1-dev.cab") == false) 
                {
                    Console.WriteLine("could not find cab file");
                    return;
                }
                (double, double) RectCorner2 = FC.GetRectCorner("ldpp-plugin-0.0.1-dev.cab");
                FC.CilckMouseOnPoint(RectCorner2);
                Thread.Sleep(2000);
                //Process[] p = Process.GetProcessesByName("C:\\Windows\\system32\\cmd.exe");
                ////关闭窗口
                //foreach (Process proc in p)
                //{
                //    if (!proc.CloseMainWindow())
                //    {
                //        proc.Kill();
                //    }
                //}


                FC.FindAllBox("UnitySampleApp");
                (double, double) RectCorner3 = FC.GetRectCorner("打开(O)");
                FC.CilckMouseOnPoint(RectCorner3);
                Thread.Sleep(2000);

                IntPtr hwndParent4 = FindWindow(null, "UnitySampleApp");
                SetForegroundWindow(hwndParent4);
                FC.FindAllBox("UnitySampleApp");
                (double, double) RectCorner4 = FC.GetRectCorner("Test");
                FC.CilckMouseOnPoint(RectCorner4);
                Thread.Sleep(2000);



                IntPtr hwndParent5 = FindWindow(null, "Plugin name: LDPP");
                SetForegroundWindow(hwndParent5);
                FC.FindAllBox("Plugin name: LDPP");
                Thread.Sleep(2000);
                //开一个新线程，随时更新返回的结果字符串数组
                Task ConfigureIndex =  new Task(() =>
                {
                    string SplitString = "========================================================";
                    while (true)
                    {
                        if (FC.BoxIndex.ContainsKey("Log") == false || FC.BoxIndex.Count == 0)
                        {
                            continue;
                        }
                        var res = (TextPattern)FC.elementCollection[FC.BoxIndex["Log"] + 1].GetCurrentPattern(TextPattern.Pattern);
                        FC.StringArray = Regex.Split(res.DocumentRange.GetText(2147483647).ToString(), SplitString, RegexOptions.IgnoreCase);
                        Console.WriteLine(FC.StringArray.Length);
                        Thread.Sleep(500);
                    }
                });
                ConfigureIndex.Start();
                Thread.Sleep(2000);
                //await ConfigureIndex;
            }
            else 
            {
                Console.WriteLine("not find exe");
            }
        }

        public bool IsOpenLdppPlugin()
        {
            IntPtr hwndParent = FindWindow(null, "C:\\Windows\\system32\\cmd.exeP");
            if (hwndParent != null) 
            {
                Console.WriteLine("success to OpenLdppPlugin");
                return true;
            }
            else 
            {
                Console.WriteLine("fail to OpenLdppPlugin");
                return false; 
            }
        }

        public bool TestStart(FormControl FC) 
        {
            IntPtr hwndParent2 = FindWindow(null, "Plugin name: LDPP");
            SetForegroundWindow(hwndParent2);
            FC.FindAllBox("Plugin name: LDPP");

            (double, double) RectCorner5 = FC.GetRectCorner("Start");
            lock (FC.StringArray)
            {
                FC.IndexFlag = FC.StringArray.Length - 1;
            }
            FC.CilckMouseOnPoint(RectCorner5);


            //已经启动
            if (FC.StringArray[1].Contains("DeviceList") == false && FC.StringArray[1].Contains("Plugin already running LDPP"))
            {
                Console.WriteLine("Success to start!");
            }
            Thread.Sleep(2000);

            //重启
            if (FC.BoxIndex.ContainsKey("Stop"))
            {
                (double, double) RectCorner9 = FC.GetRectCorner("Stop");
                lock (FC.StringArray)
                {
                    //每次做涉及到返回结果的操作，先打一个时间戳
                    FC.IndexFlag = FC.StringArray.Length;
                }
                FC.CilckMouseOnPoint(RectCorner9);
                //FC.GetResponseText(FC.IndexFlag);
            }
            Thread.Sleep(2000);

            IntPtr hwndParenta = FindWindow(null, "Plugin name: LDPP");
            SetForegroundWindow(hwndParenta);

            (double, double) RectCornera = FC.GetRectCorner("Start");
            lock (FC.StringArray)
            {
                FC.IndexFlag = FC.StringArray.Length - 1;
            }
            FC.CilckMouseOnPoint(RectCornera);
            //已经启动
            //if (FC.StringArray[1].Contains("DeviceList") == false && FC.StringArray[1].Contains("Plugin already running LDPP"))
            //{
            //    Thread.Sleep(5000);
            //    Console.WriteLine("Success to start!");
            //    return true;
            //}
            //else 
            //{
            //    Thread.Sleep(5000);
            //    return FC.GetResponseText(FC.IndexFlag);
            //}
            Thread.Sleep(5000);
            return FC.GetResponseText(FC.IndexFlag);
        }

        public bool IsDiscoverDevice(FormControl FC) 
        {
            //循环等待设备发现
            while (true)
            {

                {
                    FC.FindAllBox("Plugin name: LDPP");
                    if (FC.BoxIndex.ContainsKey(FC.DeviceName))
                    {
                        break;
                    }
                }

                //Thread.Sleep(1000);
            }
            (double, double) RectCorner6 = FC.GetRectCorner(FC.DeviceName);
            FC.CilckMouseOnPoint(RectCorner6);
            Thread.Sleep(2000);
            return true;
        }

        public bool TestConnect(FormControl FC) 
        {
            FC.FindAllBox("Plugin name: LDPP");
            //设置文本框
            FC.SetTextValue("Connect");

            //FC.FindAllBox("Plugin name: LDPP");
            (double, double) RectCorner7 = FC.GetRectCorner("Send");
            lock (FC.StringArray)
            {
                FC.IndexFlag = FC.StringArray.Length - 1;
            }
            FC.CilckMouseOnPoint(RectCorner7);
            
            return FC.GetResponseText(FC.IndexFlag);
        }

        public bool TestDisConnect(FormControl FC) 
        {
            FC.FindAllBox("Plugin name: LDPP");
            //设置文本框
            FC.SetTextValue("Disconnect");

            //FC.FindAllBox("Plugin name: LDPP");
            (double, double) RectCorner7 = FC.GetRectCorner("Send");
            lock (FC.StringArray)
            {
                FC.IndexFlag = FC.StringArray.Length - 1;
            }
            FC.CilckMouseOnPoint(RectCorner7);
            Thread.Sleep(1000);
            return FC.GetResponseText(FC.IndexFlag);
        }

        public bool TestStop(FormControl FC)
        {
            FC.FindAllBox("Plugin name: LDPP");
            if (FC.BoxIndex.ContainsKey("Stop"))
            {
                (double, double) RectCorner9 = FC.GetRectCorner("Stop");
                lock (FC.StringArray)
                {
                    //每次做涉及到返回结果的操作，先打一个时间戳
                    FC.IndexFlag = FC.StringArray.Length;
                }
                FC.CilckMouseOnPoint(RectCorner9);
                //FC.GetResponseText(FC.IndexFlag);
            }
            else 
            {
                Console.WriteLine("stop failed");
                return false;
            }
            Thread.Sleep(2000);
            return FC.GetResponseText(FC.IndexFlag);
        }

        public bool CloseCmdWindow() 
        {
            IntPtr hwndParent = FindWindow(null, "C:\\Windows\\system32\\cmd.exe");
            if (hwndParent != IntPtr.Zero)
            {
                SendMessage(hwndParent, WM_CLOSE, 0, 0);
                Console.WriteLine("success close cmdwindow");
                return true;
            }
            else 
            {
                Console.WriteLine("fail to close cmdwindow");
                return false;
            }
        }

        public bool CloseUnityWindow() 
        {
            IntPtr hwndParent = FindWindow(null, "UnitySampleApp");
            if (hwndParent != IntPtr.Zero)
            {
                SendMessage(hwndParent, WM_CLOSE, 0, 0);
                Console.WriteLine("success close UnityWindow");
                return true;
            }
            else
            {
                Console.WriteLine("fail to close UnityWindow");
                return false;
            }
        }

        public bool ClosePluginWindow()
        {
            IntPtr hwndParent = FindWindow(null, "Plugin name: LDPP");
            if (hwndParent != IntPtr.Zero)
            {
                SendMessage(hwndParent, WM_CLOSE, 0, 0);
                Console.WriteLine("success close PluginWindow");
                return true;
            }
            else
            {
                Console.WriteLine("fail to close PluginWindow");
                return false;
            }
        }

        public static void Main()
        {
            Start start = new Start();
            FormControl FC = new FormControl();
            start.StartExe(start.ExePath);
            start.InitExe(FC);
            start.IsOpenLdppPlugin();
            start.TestStart(FC);
            start.IsDiscoverDevice(FC);
            start.TestConnect(FC);
            start.CloseCmdWindow();
            start.ClosePluginWindow();
            start.CloseUnityWindow();
        }
    }
}
