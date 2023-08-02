
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Xml;

namespace LDPPInterfaceTesting
{

    /// <summary>
    /// 检查点  
    /// </summary>
   public class AutomationElementCheck
    {
        AutomationElementActions aea = new AutomationElementActions();
        public void MouseClick(string ParameterValue)
        {
            int _PX = Cursor.Position.X;
            int _PY = Cursor.Position.Y;
            if (ParameterValue == "1")
            {
                DoMouseClick(_PX, _PY);
            }
            else if (ParameterValue == "0")
            {
                SimpleClick(_PX, _PY);
            }
        }


        #region  移动焦点事件

        /// <summary>
        /// 向上移动 向下移动
        /// 向左移动 向右移动
        /// </summary>
        /// <returns></returns>
        public void RemoveFocusToPosition(string ParameterValue)
        {
            try
            {
                int _PX = 0;  //记录方向
                int _PY = 0;  //记录移动距离
                if (ParameterValue.Contains(","))
                {
                    _PY = StringParseToInt(ParameterValue.Split(',')[1]);
                    _PX = StringParseToInt(ParameterValue.Split(',')[0]);
                }
                if (ParameterValue.Contains("，"))
                {
                    _PY = StringParseToInt(ParameterValue.Split('，')[1]);
                    _PX = StringParseToInt(ParameterValue.Split('，')[0]);
                }
                if(_PX == 0)  //向上移动
                {
                    SetCursorPos(Cursor.Position.X, Cursor.Position.Y +_PY);
                }
                else if (_PX == 1)   //向下移动
                {
                    SetCursorPos(Cursor.Position.X, Cursor.Position.Y - _PY);
                }
                else if (_PX == 2)   //向左移动
                {
                    SetCursorPos(Cursor.Position.X - _PY, Cursor.Position.Y);
                }
                else    //默认向右移动
                {
                    SetCursorPos(Cursor.Position.X + _PY, Cursor.Position.Y);
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        #endregion

        #region 鼠标事件
        [DllImport("User32")] private extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("User32")] public extern static void SetCursorPos(int x, int y);
        private enum MouseEventFlags
        {
            Move = 0x0001, //移动鼠标
            LeftDown = 0x0002,//模拟鼠标左键按下
            LeftUp = 0x0004,//模拟鼠标左键抬起
            RightDown = 0x0008,//鼠标右键按下
            RightUp = 0x0010,//鼠标右键抬起
            MiddleDown = 0x0020,//鼠标中键按下 
            MiddleUp = 0x0040,//中键抬起
            Wheel = 0x0800,
            Absolute = 0x8000//标示是否采用绝对坐标
        }
        public static bool SimpleClick(int xpoint, int ypoint)
        {
            try
            {
                SetCursorPos(xpoint, ypoint);
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), xpoint, ypoint, 0, IntPtr.Zero);
                Thread.Sleep(100);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), xpoint, ypoint, 0, IntPtr.Zero);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// 鼠标事件
        /// </summary>
        /// <param name="flags">事件类型</param>
        /// <param name="dx">x坐标值(0~65535)</param>
        /// <param name="dy">y坐标值(0~65535)</param>
        /// <param name="data">滚动值(120一个单位)</param>
        /// <param name="extraInfo">不支持</param>
        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        /// <summary>鼠标操作标志位集合</summary>
        [Flags]
        enum MouseEventFlag : uint
        {
            /// <summary>鼠标移动事件</summary>
            Move = 0x0001,

            /// <summary>鼠标左键按下事件</summary>
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,

            /// <summary>设置鼠标坐标为绝对位置（dx,dy）,否则为距离最后一次事件触发的相对位置</summary>
            Absolute = 0x8000
        }

        /// <summary>触发鼠标事件</summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static bool DoMouseClick(int x, int y)
        {
            try
            {
                int dx = (int)((double)x / Screen.PrimaryScreen.Bounds.Width * 65535); //屏幕分辨率映射到0~65535(0xffff,即16位)之间
                int dy = (int)((double)y / Screen.PrimaryScreen.Bounds.Height * 0xffff); //转换为double类型运算，否则值为0、1
                mouse_event(MouseEventFlag.Move | MouseEventFlag.LeftDown | MouseEventFlag.LeftUp | MouseEventFlag.Absolute, dx, dy, 0, new UIntPtr(0)); //点击
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static void InputText(string text)
        {
            SendKeys.SendWait(text);
        }

        #endregion

        #region Toggle 事件

        /// <summary>普通切换</summary>
        public void ToggleSwitchElement(AutomationElement element, XmlNode paraNode)
        {
            
            try
            {
                aea.ToggleSwitchByAutomationElement(element);
            }
            catch(Exception ex)
            {
            }
        }
            
        /// <summary>设置开关状态</summary>
        public void SetToggleStatusElement(AutomationElement element, string status)
        {
            try
            {
                
                if (status.ToLower().Trim() == "on")
                {
                    aea.SetToggleStateByAutomationElement(element, true);
                }
                else if(status.ToLower().Trim() == "off")
                {
                    aea.SetToggleStateByAutomationElement(element, false);
                }
            }
            catch(Exception ex)
            {
              
            }
        }

        #endregion

        #region RadioButton  单选按钮事件

        /// <summary>单选按钮 选择事件</summary>
        public void RadioButtonSelectElement(AutomationElement element, XmlNode paraNode)
        {
            try
            {
                aea.RadioButtonByAutomationElement(element);
            }
            catch(Exception ex)
            {
                
            }
        }

        #endregion

        #region ComboBox

        /// <summary>ComboBox 展开事件</summary>
        public void ComboBoxExpandElement(AutomationElement element)
        {
            aea.ExpandOrCollapseByAutomationElement(element, true);
        }

        /// <summary>ComboBox 折叠事件</summary>
        public void ComboBoxCollapseElement(AutomationElement element)
        {
            aea.ExpandOrCollapseByAutomationElement(element, false);
        }

        /// <summary>ComboBox 展开事件 选择指定事件</summary>
        public void ComboBoxExpandSelectElementByIndex(AutomationElement element,int index = 0)
        {
            try
            {
                int intsleep = 300;
                if (aea.ExpandOrCollapseByAutomationElement(element, true) == true)
                {
                    //AutomationElementCollection elements = aea.FindElementByClass(element, "ComboBoxItem", intsleep);
                    //var elements = aea.FindElementByClass(element, "ComboBox", intsleep);
                    //var elements = aea.FindElementByClass(element, "ListBoxItem", intsleep);

                    AutomationElementCollection targetElements = element.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ClassNameProperty, "ListBoxItem"));

                    var elements = aea.FindElementByClass(element, "ListBoxItem", intsleep);
                    elements = aea.FindElementByClass(element, "TextBlock", intsleep);
                    int select = index;
                    if (select > elements.Count - 1)
                    {
                        select = 0;
                    }
                    AutomationElement sel = elements[select];
                    if (sel != null)
                    {
                        aea.SelectComBoxByAutomationElement(sel);
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }


        #endregion

        #region ScrollSlider

        /// <summary>Scroll 设置滚动条 值</summary>
        public void SetScrollRangeValueElement(AutomationElement element, double setvalue)
        {
            try
            {
                aea.ScrollItemBarSetValueByAutomationElement(element, setvalue, false);
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>Scroll 检查滚动条 值</summary>
        public bool CheckScrollRangeValueElement(AutomationElement element, double setvalue)
        {
            try
            {
               
                if (aea.ScrollItemBarGetValueByAutomationElement(element, true, true) == setvalue)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
               
            }
            return false;
        }

        #endregion

        #region  TextBox

        /// <summary>设定文本框内容</summary>
        public void SetTextValueElement(AutomationElement element,string value)
        {
            try
            {
                aea.SetTextByAutomationElement(element, value);
            }
            catch(Exception ex)
            {
               
            }
        }

        #endregion

        #region  向指定事件发送方向键

        public void GetElementByCache(AutomationElement elementList)
        {
            AutomationElement rootElement = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, "Lenovo Vantage"));

            CacheRequest cacheRequest = new CacheRequest();
            cacheRequest.AutomationElementMode = AutomationElementMode.None;
            cacheRequest.TreeFilter = Automation.RawViewCondition;

            cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

            cacheRequest.Add(AutomationElement.NameProperty);
            cacheRequest.Add(SelectionItemPattern.Pattern);
            cacheRequest.Push();
            Condition cond = new PropertyCondition(AutomationElement.IsSelectionItemPatternAvailableProperty, true);
            AutomationElement elementListItem = elementList.FindFirst(TreeScope.Children, cond);

        }

        /// <summary>
        /// 向指定事件发送 方向 按键
        /// </summary>
        public void SendElementByKeys(AutomationElement element, int runcycle = 0,int keys = 7)
        {
            try
            {
                
                element.SetFocus();
                Thread.Sleep(500);
                // element.GetClickablePoint();
                /*
                 * Shift +
                 * Ctrl ^
                 * Alt %
                 * CAPS LOCK {CAPSLOCK}
                 * NUM LOCK {NUMLOCK}
                 * TAB {TAB}
                SendKeys.SendWait("^C");  //Ctrl+C 组合键
                SendKeys.SendWait("+C");  //Shift+C 组合键
                SendKeys.SendWait("%C");  //Alt+C 组合键
                SendKeys.SendWait("+(AX)");  //Shift+A+X 组合键
                SendKeys.SendWait("+AX");  //Shift+A 组合键,之后按X键
                SendKeys.SendWait("{left 5}");  //按←键 5次
                SendKeys.SendWait("{h 10}");   //按h键 10次
                SendKeys.Send("汉字");  //模拟输入"汉字"2个字  
                LEFT ARROW {LEFT}     
                PAGE DOWN {PGDN}   
                PAGE UP {PGUP}    
                RIGHT ARROW {RIGHT}   
                */
                
                do
                {
                    if (keys == 0)
                    {
                        SendKeys.SendWait("{UP}");    //向上发送事件1次
                    }
                    else if (keys == 1)
                    {
                        SendKeys.SendWait("{DOWN}");    //向下发送事件1次
                    }
                    else if (keys == 2)
                    {
                        SendKeys.SendWait("{LEFT}");    //向左发送事件1次
                    }
                    else if (keys == 3)
                    {
                        SendKeys.SendWait("{RIGHT}");    //向右发送事件1次
                    }
                    else if (keys == 4)
                    {
                        SendKeys.SendWait("{PGUP}");    //向上翻页发送事件1次
                    }
                    else if (keys == 5)
                    {
                        SendKeys.SendWait("{PGDN}");    //向下翻页发送事件1次
                    }
                    else
                    {
                        SendKeys.SendWait("{PGDN}");    //向下发送事件1次
                    }
                    Thread.Sleep(300);
                    runcycle--;
                } while (runcycle > 0);
                
            }
            catch (Exception ex)
            {
                
            }
            
        }
       
        #region 获取根节点  
        public AutomationElement GetRootElement(string WindowName)
        {
            
            AutomationElement rootElement = null;
            if(string.IsNullOrEmpty(WindowName))
            {
                rootElement = AutomationElement.RootElement;
                return rootElement;
            }
            if(WindowName == "任务栏" | WindowName == "Taskbar")
            {
                rootElement = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ClassNameProperty, "Shell_TrayWnd"));
                return rootElement;
            }
            //if (WindowName == "任务栏" | WindowName == "Taskbar")
            //{
            //    rootElement = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ClassNameProperty, "Shell_TrayWnd"));
            //    return rootElement;
            //}
            rootElement = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, WindowName));
            return rootElement;
        }

        #endregion

        #region  查询方式
        #endregion


        /// <summary> 查询 单项 ID ClassName</summary>
        public AutomationElement GetAutomationElement(string WindowName,string id,string className,string name,int intsleep = 300)
        {
            AutomationElement element = null;
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(className))
                {
                    element = aea.FindElementByClasssNameAndAutomationID(GetRootElement(WindowName), id, className, intsleep);
                }
                else if(!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                {
                    element = aea.FindElementByNameAndAutomationID(GetRootElement(WindowName), id, name, intsleep);
                }
                else if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(name))
                {
                    element = aea.FindElementByNameAndClassName(GetRootElement(WindowName), name, className, intsleep);
                }
                else if(!string.IsNullOrEmpty(id) && string.IsNullOrEmpty(className))
                {
                    element = aea.FindElementByAutomationIDSimple(GetRootElement(WindowName), id, intsleep);
                }
                else if(string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(className))
                {
                    element = aea.FindElementByClassSimple(GetRootElement(WindowName),className, intsleep);
                }
                
                return element;
            }
            catch(Exception ex)
            {
                Console.WriteLine("GetAutomationElement()" + ex.ToString());
                return element;
            }
        }

        /// <summary> 查询 集合 ID ClassName</summary>
        public AutomationElementCollection GetAutomationElementCollection(string WindowName, string id, string className, string name, int intsleep = 300)
        {
            AutomationElementCollection element = null;
            try
            {
                
                if (!string.IsNullOrEmpty(id) && string.IsNullOrEmpty(className))
                {
                    
                    element = aea.FindElementByAutomationID(GetRootElement(WindowName), id, intsleep);
                }
                else if (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(className))
                {
                    element = aea.FindElementByClass(GetRootElement(WindowName), className, intsleep);
                }
                else if(string.IsNullOrEmpty(id) && string.IsNullOrEmpty(className))
                {
                    element = aea.FindElementByName(GetRootElement(WindowName), name, intsleep);
                }
                else if(!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(className))
                {
                    element = aea.FindElementByClasssNameAndAutomationIDCollection(GetRootElement(WindowName), id, className, intsleep);
                }
                return element;
            }
            catch(Exception ex)
            {
                Console.WriteLine("GetAutomationElementCollection() :" + ex.ToString());
                return element;
            }
        }
       
        #endregion

        #region  字符串 转换
        public int StringParseToInt(string strinfo)
        {
            int temp = 0;
            try
            {
                temp = int.Parse(strinfo);
            }
            catch (Exception ex)
            {
                temp = 0;
            }
            return temp;
        }

        #endregion

    }
}
