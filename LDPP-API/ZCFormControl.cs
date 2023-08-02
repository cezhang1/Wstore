using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using System.Diagnostics;
using static LDPP_API.FormControl;
using System.Windows.Automation;
using TechTalk.SpecFlow.Tracing;
using System.Windows;
using System.Threading;
using SpecFlow.Internal.Json;
using System.Text.RegularExpressions;


namespace LDPP_API
{
    class FormControl
    {


        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public extern static IntPtr SetForegroundWindow(IntPtr HW);
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
        public Dictionary<string,int>  FindAllBox(string WinName)
        {
            lock (BoxIndex) 
            {
                BoxIndex.Clear();
                IntPtr hwndParent = FindWindow(null, WinName);
                AutomationElement element = AutomationElement.FromHandle(hwndParent);
                int processIdentifier = (int)element.GetCurrentPropertyValue(AutomationElement.ProcessIdProperty);

                Condition conditions = new AndCondition(new PropertyCondition(AutomationElement.ProcessIdProperty, processIdentifier),
                    new PropertyCondition(AutomationElement.ProcessIdProperty, processIdentifier));

                elementCollection = element.FindAll(TreeScope.Subtree, conditions);
                for (int i = 0; i < elementCollection.Count; i++)
                {

                    string autoId = elementCollection[i].GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
                    if (elementCollection[i].Current.Name != null)
                    {


                        if (BoxIndex.ContainsKey(elementCollection[i].Current.Name))
                        {
                            BoxIndex.Add($"null{i}", i);
                            continue;
                        }

                        BoxIndex.Add(elementCollection[i].Current.Name, i);
                    }
                    else
                    {
                        if (BoxIndex.ContainsKey(autoId))
                        {
                            BoxIndex.Add($"null{i}", i);
                            continue;
                        }
                        BoxIndex.Add(autoId, i);
                    }
                }
                return BoxIndex;
            }
            
        }
        //获取当前控件的坐标
        public (double, double) GetRectCorner(string Func) 
        {
            
            foreach (var x in BoxIndex)
            {
                Console.WriteLine(x.Key);
            }
            (double, double) RectCorner;
            Rect boundingRect = (System.Windows.Rect)elementCollection[BoxIndex[Func]].GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
            RectCorner.Item1 = boundingRect.Left;
            RectCorner.Item2 = boundingRect.Top;
            return RectCorner;
        }
        //单击
        public void CilckMouseOnPoint((double, double) RectCorner) 
        {
            System.Windows.Forms.Cursor.Position =new System.Drawing.Point((int)RectCorner.Item1+10, (int)RectCorner.Item2+5);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        //双击
        public void DoubleCilckMouseOnPoint((double, double) RectCorner)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)RectCorner.Item1 + 5, (int)RectCorner.Item2 + 5);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN | MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            MouseHandler.mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN | MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }


        //初始化连接
        public void Init()
        {
            
            IntPtr hwndParent = FindWindow(null, "UnitySampleApp");
            SetForegroundWindow(hwndParent);
            FindAllBox("UnitySampleApp");
            (double, double) RectCorner;
            System.Windows.Rect boundingRect = (System.Windows.Rect)elementCollection[BoxIndex["Test"] - 1].GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
            RectCorner.Item1 = boundingRect.Left;
            RectCorner.Item2 = boundingRect.Top;
            CilckMouseOnPoint(RectCorner);
            Thread.Sleep(2000);

            FindAllBox("UnitySampleApp");
            (double, double) RectCorner1 = GetRectCorner("LDPP");
            CilckMouseOnPoint(RectCorner1);
            Thread.Sleep(2000);



            FindAllBox("UnitySampleApp");
            (double, double) RectCorner2;
            System.Windows.Rect boundingRect2 = (System.Windows.Rect)elementCollection[BoxIndex["Enter Message"] - 1].GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
            RectCorner2.Item1 = boundingRect2.Left;
            RectCorner2.Item2 = boundingRect2.Top;
            CilckMouseOnPoint(RectCorner2);
            Thread.Sleep(2000);

            FindAllBox("UnitySampleApp");
            (double, double) RectCorner3 = GetRectCorner("LDPP");
            CilckMouseOnPoint(RectCorner3);
            Thread.Sleep(2000);
        }
        //设置文本框
        public bool SetTextValue(string TextValue)
        {
            FindAllBox("Plugin name: LDPP");
            (double, double) RectCorner;
            System.Windows.Rect boundingRect = (System.Windows.Rect)elementCollection[BoxIndex["Connected Plugin Command"] + 2].GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
            RectCorner.Item1 = boundingRect.Left+ boundingRect.Width/2;
            RectCorner.Item2 = boundingRect.Top;
            DoubleCilckMouseOnPoint(RectCorner);
            SendKeys.SendWait("{BACKSPACE}");
            //SendKeys.SendWait("{CAPSLOCK}");
            SendKeys.SendWait($"{TextValue}");
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(2000);
            return true;
        }
        //获取返回结果
        public bool GetResponseText(int IndexFlag)
        {
            IntPtr hwndParent = FindWindow(null, "Plugin name: LDPP");
            SetForegroundWindow(hwndParent);
            FindAllBox("Plugin name: LDPP");
            int Timer = 0;
            while (true) 
            {

                lock (StringArray)
                {
                    for (int i = StringArray.Length - 1; i > IndexFlag; i--)
                    {
                        Console.WriteLine(StringArray[i]);

                        if (StringArray[i].Contains("DeviceList") == false && StringArray[i].Contains("Success"))
                        {

                            Console.WriteLine("Success!");
                            return true;
                        }
                        else if (StringArray[i].Contains("DeviceList") == false && StringArray[i].Contains("Failed"))
                        {

                            Console.WriteLine("Failed!");
                            return false;
                        }
                        
                    }
                }
                Thread.Sleep(1000);
                Timer++;
                if (Timer >= 20) 
                {
                    return false;
                }
            }
            
        }
    }

}
