using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LDPPInterfaceTesting
{
    class SystemControl
    {
        #region  设定窗口大小

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)] private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)] public static extern IntPtr GetForegroundWindow();  //当前窗口
        [DllImport("user32.dll", EntryPoint = "FindWindow")] public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetForegroundWindow")] public static extern bool SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern bool SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);

        /// <summary>
        /// 设置窗口大小显示
        /// </summary>
        /// <param name="WindowName"></param>
        /// <param name="flag">0-1:窗口置顶 2：最小化  3： 最大化  任意正常</param>
        /// <returns></returns>
        public void SetWindowSize(string WindowClassName, string WindowTitle, int flag = 1)
        {
            try
            {
                IntPtr ParenthWnd;
                foreach (IntPtr intptr in GetAllWindowsIntPtr())
                {
                    StringBuilder sb = new StringBuilder(256);
                    GetWindowText(intptr, sb, sb.Capacity);
                    string wname = sb.ToString();
                    GetClassName(intptr, sb, sb.Capacity);
                    string wcname = sb.ToString();
                    if (wname == WindowTitle & wcname == WindowClassName)
                    {
                        ParenthWnd = intptr;
                        if (flag == 2)  //最小化
                        {
                            ShowWindow(ParenthWnd, 2);
                        }
                        else if (flag == 3)  //最大化
                        {
                            ShowWindow(ParenthWnd, 3);
                        }
                        else if (flag == 0)
                        {
                            SwitchToThisWindow(ParenthWnd, true);
                        }
                        else if (flag == 1)
                        {
                            SetWindowPos(ParenthWnd, -1, 0, 0, 0, 0, 1);
                        }
                        else  //正常
                        {
                            ShowWindow(ParenthWnd, 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetWindowSize" + ex.ToString());
            }
        }

        /// <summary>
        /// 设置窗口大小显示
        /// </summary>
        /// <param name="WindowName"></param>
        /// <param name="flag">0-1:窗口置顶 2：最小化  3： 最大化  任意正常</param>
        /// <returns></returns>
        public void SetWindowSize(IntPtr ParenthWnd, int flag = 1)
        {
            try
            {
                switch(flag)
                {
                    case 0:
                        SwitchToThisWindow(ParenthWnd, true);
                        break;
                    case 1:
                        SetWindowPos(ParenthWnd, -1, 0, 0, 0, 0, 1);
                        break;
                    case 2: //最小化
                        ShowWindow(ParenthWnd, 2);
                        break;
                    case 3: //最大化
                        ShowWindow(ParenthWnd, 3);
                        break;
                    default:
                        ShowWindow(ParenthWnd, 1);
                        break;
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetWindowSize" + ex.ToString());
            }
        }

        /// <summary>
        /// 将窗口显示在最前端
        /// </summary>
        /// <returns></returns>
        public void ShowThisWindowByProcessName(string pName)
        {
            try
            {
                
                Process[] temp = Process.GetProcessesByName(pName);//在所有已启动的进程中查找需要的进程；     
                if (temp.Length > 0)//如果查找到     
                {
                    IntPtr handle = temp[0].MainWindowHandle;
                    SwitchToThisWindow(handle, true);    // 激活，显示在最前  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ShowThisWindowByProcessName() " + ex.ToString());
            }

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
                Console.WriteLine("ShowThisWindowByProcessName() " + ex.ToString());
            }
            return IntPtr.Zero;
        }

        #endregion

        #region 关闭窗口

        public const int WM_CLOSE = 0x10;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public void CloseWindow(string lpClassName, string lpWindowName)
        {
            IntPtr fw = FindWindow(lpClassName,lpWindowName);
            SendMessage(fw,WM_CLOSE,0,0);
        }

        #endregion

        #region 根据窗口句柄获取窗口大小

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="flag">0:宽度 1:高度 2:最左坐标 3:最上坐标 4:最右坐标 5:最下坐标 6:中间最上坐标 7:最中间坐标</param>
        /// <returns></returns>
        public int GetWindowsInfo(IntPtr window,int flag = 0)
        {
            RECT rect = new RECT();
            GetWindowRect(window, ref rect);
            int def = 0;
            switch(flag)
            {
                case 0:
                    return rect.Right - rect.Left;
                case 1:
                    return rect.Bottom - rect.Top;
                case 2:
                    return rect.Left;
                case 3:
                    return rect.Top;
                case 4:
                    return rect.Right;
                case 5:
                    return rect.Bottom;
                case 6:
                    return rect.Left + (rect.Right - rect.Left) / 2;
                case 7:
                    return rect.Left + (rect.Bottom - rect.Top) / 2;
            }
            return def;
           
        }

        #endregion

        #region  获取所有窗口句柄
        private enum GetWindowCmd : uint
        {
            /// <summary>
            /// 返回的句柄标识了在Z序最高端的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在Z序最高端的最高端窗口；
            /// 如果指定窗口是顶层窗口，则该句柄标识了在z序最高端的顶层窗口：
            /// 如果指定窗口是子窗口，则句柄标识了在Z序最高端的同属窗口。
            /// </summary>
            GW_HWNDFIRST = 0,
            /// <summary>
            /// 返回的句柄标识了在z序最低端的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该柄标识了在z序最低端的最高端窗口：
            /// 如果指定窗口是顶层窗口，则该句柄标识了在z序最低端的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在Z序最低端的同属窗口。
            /// </summary>
            GW_HWNDLAST = 1,
            /// <summary>
            /// 返回的句柄标识了在Z序中指定窗口下的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在指定窗口下的最高端窗口：
            /// 如果指定窗口是顶层窗口，则该句柄标识了在指定窗口下的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在指定窗口下的同属窗口。
            /// </summary>
            GW_HWNDNEXT = 2,
            /// <summary>
            /// 返回的句柄标识了在Z序中指定窗口上的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在指定窗口上的最高端窗口；
            /// 如果指定窗口是顶层窗口，则该句柄标识了在指定窗口上的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在指定窗口上的同属窗口。
            /// </summary>
            GW_HWNDPREV = 3,
            /// <summary>
            /// 返回的句柄标识了指定窗口的所有者窗口（如果存在）。
            /// GW_OWNER与GW_CHILD不是相对的参数，没有父窗口的含义，如果想得到父窗口请使用GetParent()。
            /// 例如：例如有时对话框的控件的GW_OWNER，是不存在的。
            /// </summary>
            GW_OWNER = 4,
            /// <summary>
            /// 如果指定窗口是父窗口，则获得的是在Tab序顶端的子窗口的句柄，否则为NULL。
            /// 函数仅检查指定父窗口的子窗口，不检查继承窗口。
            /// </summary>
            GW_CHILD = 5,
            /// <summary>
            /// （WindowsNT 5.0）返回的句柄标识了属于指定窗口的处于使能状态弹出式窗口（检索使用第一个由GW_HWNDNEXT 查找到的满足前述条件的窗口）；
            /// 如果无使能窗口，则获得的句柄与指定窗口相同。
            /// </summary>
            GW_ENABLEDPOPUP = 6
        }

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

        ///https://blog.csdn.net/pangwenquan5/article/details/40317773
        /// 定义设置活动窗口API（SetActiveWindow），设置前台窗口API（SetForegroundWindow）
        [DllImport("user32.dll")] static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(
                                                                    IntPtr hWnd,//窗口句柄 
                                                                    StringBuilder lpString,//标题 
                                                                    int nMaxCount //最大值 
                                                                    );
        [DllImport("user32.dll")]
        public static extern int GetClassName(
                                                            IntPtr hWnd,//句柄 
                                                            StringBuilder lpString, //类名 
                                                            int nMaxCount //最大值 
                                                            );

        /// <summary>
        /// 获取所有窗口句柄
        /// </summary>
        /// <returns>返回窗口句柄列表</returns>
        public List<IntPtr> GetAllWindowsIntPtr()
        {
            List<IntPtr> IntPtrList = new List<IntPtr>();
            try
            {
                IntPtr desktopPtr = GetDesktopWindow();

                IntPtr winPtr = GetWindow(desktopPtr, GetWindowCmd.GW_CHILD);//2、获得一个子窗口（这通常是一个顶层窗口，当前活动的窗口）

                while (winPtr != IntPtr.Zero)//3、循环取得桌面下的所有子窗口
                {
                    winPtr = GetWindow(winPtr, GetWindowCmd.GW_HWNDNEXT);//4、继续获取下一个子窗口
                    IntPtrList.Add(winPtr);
                }
                return IntPtrList;
            }
            catch
            {
                return IntPtrList;
            }
        }

        public bool WindowIntPtrIsExistByName(string titleName)
        {
            try
            {
                foreach (IntPtr intptr in GetAllWindowsIntPtr())
                {
                    StringBuilder sb = new StringBuilder(256);
                    GetWindowText(intptr, sb, sb.Capacity);
                    if (sb.ToString() == titleName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool WindowIntPtrIsExistByClassName(string ClassName)
        {
            try
            {
                foreach (IntPtr intptr in GetAllWindowsIntPtr())
                {
                    StringBuilder sb = new StringBuilder(256);
                    GetClassName(intptr, sb, sb.Capacity);
                    if (sb.ToString() == ClassName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion


        /// <summary>
        /// 通过窗口名字获取窗口的ID
        /// </summary>
        /// <param name="WindowName"></param>
        /// <param name="flag">0-1:窗口置顶 2：最小化  3： 最大化  任意正常</param>
        /// <returns></returns>
        public IntPtr GetWindowhwd(string WindowTitle, int flag = 1)
        {
            try
            {
                // IntPtr ParenthWnd;
                foreach (IntPtr intptr in GetAllWindowsIntPtr())
                {
                    StringBuilder sb = new StringBuilder(256);
                    GetWindowText(intptr, sb, sb.Capacity);
                    string wname = sb.ToString();
                    if (wname == WindowTitle)
                    {
                        return intptr;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetWindowSize" + ex.ToString());
            }

            return IntPtr.Zero;
        }

    }
}
