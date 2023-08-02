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
using System.Xml.Linq;


namespace LE_STORE
{
    class FormControl
    {


        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public extern static IntPtr SetForegroundWindow(IntPtr HW);


        [DllImport("user32.dll", SetLastError = true)]
        public extern static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public const UInt32 BM_CLICK = 0x00F5;  // 按钮点击消息
        public FormControl() 
        {
            
        }
        //存控件对应的automation索引
        public Dictionary<string,int> BoxIndex=new Dictionary<string, int>();
        //所有automation
        public AutomationElementCollection elementCollection;
        
        public MouseHandler MH = new MouseHandler();
        //要连接的设备名
        public string DeviceName;
        //字符串时间戳
        public int IndexFlag=0;
        //存分割后的字符串数组
        public string[] StringArray;
        public void SetDeviceName(string DName) 
        {
            DeviceName= DName;
        }
        //获取当前窗口的所有控件名(有些无法获取)
        public Dictionary<string,int>  FindAllBoxByAll(string WinName)
        {
            Console.WriteLine("****************************************");
            lock (BoxIndex) 
            {
                BoxIndex.Clear();
                IntPtr hwndParent = FindWindow(null, WinName);
                AutomationElement element = AutomationElement.FromHandle(hwndParent);
                //int processIdentifier = (int)element.GetCurrentPropertyValue(AutomationElement.LocalizedControlTypeProperty);

                System.Windows.Automation.Condition conditions = Condition.TrueCondition;


                string[] TextNameV;
                elementCollection = element.FindAll(TreeScope.Subtree, conditions);
                string autoId;
                for (int i = 0; i < elementCollection.Count; i++)
                {
                    try
                    {
                        autoId = elementCollection[i].GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
                    }
                    catch (ElementNotAvailableException) 
                    {
                        break;
                    }
                    if (elementCollection[i].Current.Name != null)
                    {

                        TextNameV = elementCollection[i].Current.Name.Split('.');
                        if (BoxIndex.ContainsKey(TextNameV[0]))
                        {
                            BoxIndex.Add($"null{i}", i);
                            continue;
                        }

                        BoxIndex.Add(TextNameV[0], i);
                        Console.WriteLine(TextNameV[0]);
                    }
                    else
                    {
                        TextNameV = autoId.Split('.');
                        if (BoxIndex.ContainsKey(TextNameV[0]))
                        {
                            BoxIndex.Add($"null{i}", i);
                            continue;
                        }
                        BoxIndex.Add(TextNameV[0], i);
                        Console.WriteLine(TextNameV[0]);
                    }
                }
                return BoxIndex;
            }
        }

        //获取当前窗口的所有控件名(有些无法获取)
        public Dictionary<string, int> FindAllBoxByLocalizedControlType(string WinName, string LocalizedControlType)
        {
            Console.WriteLine("****************************************");
            lock (BoxIndex)
            {
                BoxIndex.Clear();
                IntPtr hwndParent = FindWindow(null, WinName);
                AutomationElement element = AutomationElement.FromHandle(hwndParent);
                //int processIdentifier = (int)element.GetCurrentPropertyValue(AutomationElement.LocalizedControlTypeProperty);

                Condition conditions = new AndCondition(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, LocalizedControlType),
                     Condition.TrueCondition);

                string[] TextNameV;
                elementCollection = element.FindAll(TreeScope.Subtree, conditions);
                string autoId;
                for (int i = 0; i < elementCollection.Count; i++)
                {

                    try
                    {
                        autoId = elementCollection[i].GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
                    }
                    catch (ElementNotAvailableException)
                    {
                        break;
                    }
                    if (elementCollection[i].Current.Name != null)
                    {

                        TextNameV = elementCollection[i].Current.Name.Split('.');
                        if (BoxIndex.ContainsKey(TextNameV[0]))
                        {
                            BoxIndex.Add($"null{i}", i);
                            continue;
                        }

                        BoxIndex.Add(TextNameV[0], i);
                        Console.WriteLine(TextNameV[0]);
                    }
                    else
                    {
                        TextNameV = autoId.Split('.');
                        if (BoxIndex.ContainsKey(TextNameV[0]))
                        {
                            BoxIndex.Add($"null{i}", i);
                            continue;
                        }
                        BoxIndex.Add(TextNameV[0], i);
                        Console.WriteLine(TextNameV[0]);
                    }
                }
                return BoxIndex;
            }
        }
            //获取当前控件的坐标
            public (double, double) GetRectCorner(string Func) 
        {
            if (!BoxIndex.ContainsKey(Func)) 
            {
                Thread.Sleep(2000);
                FindAllBoxByAll("Microsoft Store");
            }
            //foreach (var x in BoxIndex)
            //{
            //    Console.WriteLine(x.Key);
            //}
            (double, double) RectCorner;
            System.Windows.Rect boundingRect = (System.Windows.Rect)elementCollection[BoxIndex[Func]].GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
            RectCorner.Item1 = boundingRect.Left;
            RectCorner.Item2 = boundingRect.Top;
            return RectCorner;
        }
        //单击
        public void CilckMouseOnPoint((double, double) RectCorner) 
        {
            System.Windows.Forms.Cursor.Position =new System.Drawing.Point((int)RectCorner.Item1+10, (int)RectCorner.Item2+10);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        //双击
        public void DoubleCilckMouseOnPoint((double, double) RectCorner)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)RectCorner.Item1 + 10, (int)RectCorner.Item2 + 5);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN | MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN | MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        //设置文本框
        public bool SetTextValue(string TextValue)
        {
            FindAllBoxByAll("Microsoft Store");
            (double, double) RectCorner = GetRectCorner("搜索");
            DoubleCilckMouseOnPoint(RectCorner);
            SendKeys.SendWait($"{TextValue}");
            SendKeys.SendWait(" ");
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(2000);
            return true;
        }

        //等待加载
        public void WaitToLoad(string Text) 
        {
            while (true) 
            {
                FindAllBoxByAll("Microsoft Store");
                if (BoxIndex.ContainsKey(Text)) 
                {
                    return;
                }
                Thread.Sleep(500);
            }
        }

    }

}
