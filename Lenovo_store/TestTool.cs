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
using LE_STORE;
using System.IO;

namespace Lenovo_store
{
    class TestTool
    {
        public string ConfigPath= "C:\\LenovoSmartEngineClient\\Config\\TestTool.ini";
        public string ClassName;

        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

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

        public string INIRead(string section, string key, string path)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=配置节点名称，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();

        }

        public void LaunchTestTool() 
        {
            Process.Start(INIRead("Task", "ToolPath", ConfigPath)); 
            Thread.Sleep(1000);
        }

        public void SetToolClass() 
        {
            StreamReader sr = new StreamReader(ConfigPath, Encoding.UTF8);
            string line;
            line = sr.ReadLine();
            line = sr.ReadLine();
            string[] LTWO = line.Split('=');
            ClassName = LTWO[1];
            Thread.Sleep(1000);
        }

        public bool WaitToTest(FormControl FC)
        {
            IntPtr hwndParent = FindWindow(null, "SESceneTools");
            SetForegroundWindow(hwndParent);
            Thread.Sleep(1000);
            bool ExitFlag = false;
            int Num = 0;
            while (true)
            {
                ++Num;
                if (ExitFlag == true) 
                {
                    break;
                }
                if (Num > 50) 
                {
                    return false;
                }
                FC.FindAllBoxByAll("SESceneTools");
                foreach(var Name in FC.BoxIndex)
                {
                    if (Name.Key.Contains("场景：")) 
                    {
                        string[] Sv = Name.Key.Split('：');
                        if (Sv[1] == ClassName)
                        {
                            (double, double) RectCorner = FC.GetRectCorner("正确");
                            FC.CilckMouseOnPoint(RectCorner);
                            Thread.Sleep(1000);
                        }
                        else 
                        {
                            (double, double) RectCorner = FC.GetRectCorner("错误");
                            FC.CilckMouseOnPoint(RectCorner);
                            Thread.Sleep(1000);
                            FC.FindAllBoxByAll("SESceneTools");
                            (double, double) RectCorner2 = FC.GetRectCorner(ClassName);
                            FC.CilckMouseOnPoint(RectCorner2);
                            Thread.Sleep(1000);
                        }
                        ExitFlag = true;
                        break;
                    }
                }
                Thread.Sleep(1000);
            }
            return true;
        }
    }
}
