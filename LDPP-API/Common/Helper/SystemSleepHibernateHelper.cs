using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
//using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ConsoleDebug
{ 
    class SystemSleepHibernateHelper
    {
        #region  S3 S4

        public static bool Sleep(int sec = 60)
        {
            DateTime wakeUpTime = DateTime.Now, sleepTime = DateTime.Now;
            PowerManagement.Sleep(DateTime.Now.AddSeconds(sec), (pm, args) =>
            {
                wakeUpTime = DateTime.Now;
            });
            Thread.Sleep(15000);
            if ((wakeUpTime - sleepTime).TotalSeconds < 2)
            {
                return false;
            }
            Thread.Sleep(TimeSpan.FromSeconds(2));
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return true;
        }

        public static bool Hibernate(int sec = 60)
        {
            DateTime wakeUpTime = DateTime.Now, sleepTime = DateTime.Now;
            PowerManagement.Hibernate(DateTime.Now.AddSeconds(sec), (pm, args) =>
            {
                wakeUpTime = DateTime.Now;
            });
            Thread.Sleep(15000);
            if ((wakeUpTime - sleepTime).TotalSeconds < 2)
            {
                return false;
            }
            Thread.Sleep(TimeSpan.FromSeconds(2));
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return true;
        }

        public class PowerManagement
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessageW(int hWnd, uint Msg, int wParam, int lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessageA(int hWnd, uint Msg, int wParam, int lParam);

            private const int WM_SYSCOMMAND = 0x0112;
            private const int SC_MONITORPOWER = 0xF170;
            private const int MonitorTurnOn = -1;
            private const int MonitorShutoff = 2;
            private const int HWND_BROADCAST = 0xffff;

            [DllImport("user32.dll")]
            public static extern void mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 dwData, UIntPtr dwExtraInfo);
            private const int MOUSEEVENTF_MOVE = 0x0001;


            public static void Sleep(DateTime _dtWakeUpTime, EventHandler _lambReturnWakenTime)
            {
                EnterS3S4(true, _dtWakeUpTime, _lambReturnWakenTime);
                MonitorOn();
            }
            public static void Hibernate(DateTime _dtWakeUpTime, EventHandler _lambReturnWakenTime)
            {
                EnterS3S4(false, _dtWakeUpTime, _lambReturnWakenTime);
                MonitorOn();
            }
            public static void EnterS3S4(bool _Sleep, DateTime _dtWakeUpTime, EventHandler _lambReturnWakenTime)
            {
                TimeManager wup = new TimeManager();
                wup.eventHandler += _lambReturnWakenTime;
                wup.SetWakeUpTime(_dtWakeUpTime);
                if (_Sleep)
                {
                    Application.SetSuspendState(PowerState.Suspend, true, false);
                }
                else
                {
                    Application.SetSuspendState(PowerState.Hibernate, true, false);
                }
            }

            //https://social.msdn.microsoft.com/Forums/en-US/4b366a2a-2930-43f6-82a8-2dbfb76fd760/window-10-c-code-to-wake-and-turn-on-monitor
            //stackoverflow.com/questions/12572441/sendmessage-sc-monitorpower-wont-turn-monitor-on-when-running-windows-8
            internal static void MonitorOn()
            {
                //SendMessage((IntPtr)0xFFFF, 0x0112, (IntPtr)0xF170, (IntPtr)(-1));//虽然这条与下面3行有相同效果，但Win10上点不亮，只好强行混合试用。要是还是不行，就考虑用最后参数-1，即进入低电量模式，看行不行
                SendMessage((IntPtr)HWND_BROADCAST, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)MonitorTurnOn);
                mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);//发现Win10上单独这3条无法点亮从hibernation返回的显示器，google各处找不到更好的结果。
                Thread.Sleep(40);
                mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                SendMessageA(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorTurnOn);
                mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);//发现Win10上单独这3条无法点亮从hibernation返回的显示器，google各处找不到更好的结果。
                Thread.Sleep(40);
                mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                SendMessageW(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorTurnOn);
                mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);//发现Win10上单独这3条无法点亮从hibernation返回的显示器，google各处找不到更好的结果。
                Thread.Sleep(40);
                mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
            }

            internal static void MonitorOff()
            {
                //SendMessage((IntPtr)0xFFFF, 0x0112, (IntPtr)0xF170, (IntPtr)(-1));//虽然这条与下面3行有相同效果，但Win10上点不亮，只好强行混合试用。要是还是不行，就考虑用最后参数-1，即进入低电量模式，看行不行
                SendMessage((IntPtr)HWND_BROADCAST, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)MonitorShutoff);
               // mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);//发现Win10上单独这3条无法点亮从hibernation返回的显示器，google各处找不到更好的结果。
               // Thread.Sleep(40);
               // mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
            }

        }

        class TimeManager
        {
            [DllImport("kernel32.dll")]
            public static extern SafeWaitHandle CreateWaitableTimer(IntPtr lpTimerAttributes,
                                                                      bool bManualReset,
                                                                    string lpTimerName);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWaitableTimer(SafeWaitHandle hTimer,
                                                        [In] ref long pDueTime,
                                                                  int lPeriod,
                                                               IntPtr pfnCompletionRoutine,
                                                               IntPtr lpArgToCompletionRoutine,
                                                                 bool fResume);

            public event EventHandler eventHandler;

            private BackgroundWorker bgWorker = new BackgroundWorker();

            public TimeManager()
            {
                //Here , Nothing happens.
                bgWorker.DoWork += new DoWorkEventHandler(WaitingTimeUp);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Callback_WhenWakeUp);
            }

            public void SetWakeUpTime(DateTime time)
            {
                //start counting time 
                bgWorker.RunWorkerAsync(time.ToFileTime());
            }

            void Callback_WhenWakeUp(object sender, RunWorkerCompletedEventArgs e)
            {
                if (eventHandler != null)
                {
                    eventHandler(this, new EventArgs());
                }
            }

            private void WaitingTimeUp(object sender, DoWorkEventArgs e)
            {
                long waketime = (long)e.Argument;

                using (SafeWaitHandle handle = CreateWaitableTimer(IntPtr.Zero, true, this.GetType().Assembly.GetName().Name.ToString() + "Timer"))
                {
                    if (SetWaitableTimer(handle, ref waketime, 0, IntPtr.Zero, IntPtr.Zero, true))
                    {
                        using (EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset))
                        {
                            wh.SafeWaitHandle = handle;
                            wh.WaitOne();
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
            }

        }

        #endregion

        //#region 设置用户密码
        //public static bool ResetUserPassword(string userName, string pwd)
        //{
        //    string _Path = "WinNT://" + Environment.MachineName;
        //    DirectoryEntry machine = new DirectoryEntry(_Path);
        //    DirectoryEntry user = machine.Children.Find(userName, "User");
        //    if (user != null)
        //    {
        //        user.Invoke("SetPassword", pwd);
        //        user.CommitChanges();
        //        user.Close();
        //        return true;
        //    }
        //    Console.WriteLine("ResetUserPassword fail,{0},{1}", userName, pwd);
        //    return false;
        //}

        //#endregion


    }
}
