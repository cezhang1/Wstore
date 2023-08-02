using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using System.Diagnostics;
using System.Windows.Automation;
using TechTalk.SpecFlow.Tracing;
using System.Threading;
using SpecFlow.Internal.Json;
using System.Text.RegularExpressions;

namespace LE_STORE
{
    class LeStore
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public extern static IntPtr SetForegroundWindow(IntPtr HW);

        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        public List<string> Top20Soft=new List<string>();

        public string INIRead(string section, string key, string path)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=配置节点名称，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();

        }

        public bool LaunchLeStore(FormControl FC) 
        {
            System.Diagnostics.Process.Start("ms-windows-store:");
            FC.WaitToLoad("搜索");
            return true;
        }

        public bool SetClass( FormControl FC) 
        {
            IntPtr hwndParent = FindWindow(null, "Microsoft Store");
            SetForegroundWindow(hwndParent);
            Thread.Sleep(1000);
            string ClassName = INIRead("Task", "ClassNameText", "C:\\LenovoSmartEngineClient\\Config\\TestTool.ini");
            FC.SetTextValue(ClassName);
            FC.FindAllBoxByAll("Microsoft Store");
            FC.WaitToLoad("筛选器");
            (double, double) RectCorner = FC.GetRectCorner("筛选器");
            FC.CilckMouseOnPoint(RectCorner);
            FC.WaitToLoad("已选择 价格类型 筛选器 所有类型");
            FC.FindAllBoxByAll("Microsoft Store");
            (double, double) RectCorner3 = FC.GetRectCorner("已选择 价格类型 筛选器 所有类型");
            FC.CilckMouseOnPoint(RectCorner3);
            FC.WaitToLoad("免费下载");
            FC.FindAllBoxByAll("Microsoft Store");
            (double, double) RectCorner4 = FC.GetRectCorner("免费下载");
            FC.CilckMouseOnPoint(RectCorner4);
            FC.FindAllBoxByAll("Microsoft Store");
            (double, double) RectCorner5 = FC.GetRectCorner("Microsoft Store   Insider");
            MouseHandler.SetCursorPos((int)RectCorner5.Item1 + 100, (int)RectCorner5.Item2 + 100);
            Thread.Sleep(1000);
            return true;
        }

        public bool GetSoftForIdx(FormControl FC) 
        {
            string[] TextNameV;
            while (Top20Soft.Count < 20) 
            {
                FC.FindAllBoxByLocalizedControlType("Microsoft Store", "卡片");
                foreach (var Name in FC.BoxIndex)
                {
                    TextNameV= Name.Key.Split('.');
                    if (!Top20Soft.Contains(TextNameV[0]) && Top20Soft.Count < 20)
                    {
                        Top20Soft.Add(TextNameV[0]);
                        Console.WriteLine(TextNameV[0]);
                    }
                }
                MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_WHEEL, 0, 0, MouseHandler.delta, 0);
                Thread.Sleep(1000);
            }
            
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_WHEEL, 0, 0, 2000, 0);
            return true;
        }

        public bool InstallSoftForIdx(int SoftIdx,FormControl FC)
        {
            SetClass(FC);
            Thread.Sleep(1000);
            while (true) 
            {
                FC.FindAllBoxByAll("Microsoft Store");
                if (FC.BoxIndex.ContainsKey(Top20Soft[SoftIdx]))
                {
                    break;
                }
                else 
                {
                    MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_WHEEL, 0, 0, MouseHandler.delta, 0);
                }
                Thread.Sleep(1000);
            }
            (double, double) RectCorner = FC.GetRectCorner(Top20Soft[SoftIdx]);
            FC.CilckMouseOnPoint(RectCorner);
            FC.WaitToLoad("后退");
            Thread.Sleep(2000);
            FC.FindAllBoxByAll("Microsoft Store");
            if (FC.BoxIndex.ContainsKey("打开") || FC.BoxIndex.ContainsKey("已安装"))
            {
                Console.WriteLine("该软件已安装");
                (double, double) RectCorner2 = FC.GetRectCorner("后退");
                FC.CilckMouseOnPoint(RectCorner2);
                FC.WaitToLoad("搜索");
                return false;
            }
            else 
            {
                if (FC.BoxIndex.ContainsKey("获取")) 
                {
                    (double, double) RectCorner3 = FC.GetRectCorner("获取");
                    FC.CilckMouseOnPoint(RectCorner3);
                }
                if (FC.BoxIndex.ContainsKey("安装"))
                {
                    (double, double) RectCorner3 = FC.GetRectCorner("安装");
                    FC.CilckMouseOnPoint(RectCorner3);
                }
                while (true) 
                {
                    Thread.Sleep(20000);
                    FC.FindAllBoxByAll("Microsoft Store");
                    if (FC.BoxIndex.ContainsKey("已安装") || FC.BoxIndex.ContainsKey("打开")) 
                    {
                        break;
                    }
                }
                Console.WriteLine("下载完成");
                return true;
            }
        }

        public bool LaunchlSoftForIdx(int SoftIdx,FormControl FC) 
        {
            SetClass(FC);
            while (true)
            {
                FC.FindAllBoxByAll("Microsoft Store");
                if (FC.BoxIndex.ContainsKey(Top20Soft[SoftIdx]))
                {
                    break;
                }
                else
                {
                    MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_WHEEL, 0, 0, MouseHandler.delta, 0);
                }
                Thread.Sleep(1000);
            }
            (double, double) RectCorner = FC.GetRectCorner(Top20Soft[SoftIdx]);
            FC.CilckMouseOnPoint(RectCorner);
            FC.WaitToLoad("后退");
            Thread.Sleep(2000);
            FC.FindAllBoxByAll("Microsoft Store");
            if (FC.BoxIndex.ContainsKey("获取"))
            {
                Console.WriteLine("该软件还没有下载");
                return false;
            }
            else if (FC.BoxIndex.ContainsKey("打开"))
            {
                (double, double) RectCorner2 = FC.GetRectCorner("打开");
                FC.DoubleCilckMouseOnPoint(RectCorner2);
                Thread.Sleep(2000);
                return true;
            }
            else 
            {
                Console.WriteLine("未知错误");
                return false;
            }
        }

        public bool CloseSoftByIdx(int SoftIdx)
        {
            string[] StringVec = Top20Soft[SoftIdx].Split('.');
            IntPtr hwndParent = FindWindow(null, StringVec[0]);
            if (hwndParent==IntPtr.Zero)
            {
                return false;
            }
            uint pid;
            GetWindowThreadProcessId(hwndParent, out pid);
            Process process = Process.GetProcessById((int)pid);

            process.Kill();
            return true;
        }

        public bool UninstallSoftForName(string SoftName) 
        {
            return true;
        }
    }
}
