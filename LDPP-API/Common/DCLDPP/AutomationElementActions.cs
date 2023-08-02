using System.Threading;
using System.Windows.Automation;

namespace LDPPInterfaceTesting
{
    /// <summary>
    /// AutomationElementActions
    /// 控件查询
    /// 控件事件
    /// </summary>
    public class AutomationElementActions
    {
        #region 单项查询控件

        /// <summary>通过类名查找控件 所有控件集合</summary>
        /// <param name="element">控件节点</param>
        /// <param name="className">控件类名</param>
        /// <param name="timeout">延时</param>
        /// <returns>控件</returns>
        public AutomationElementCollection FindElementByClass(AutomationElement element, string className, int timeout = 300)
        {
            AutomationElementCollection targetElements = null;
            if (string.IsNullOrEmpty(className))
            {
                return targetElements;
            }
            do
            {
                if (element != null)
                {
                    if (!string.IsNullOrEmpty(className))
                    {
                        targetElements = element.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ClassNameProperty, className));
                        if (targetElements.Count > 0)
                        {
                            return targetElements;
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElements;
        }

        /// <summary>通过类名查找控件</summary>
        /// <param name="element">控件节点</param>
        /// <param name="className">控件类名</param>
        /// <param name="timeout">延时</param>
        /// <returns>控件</returns>
        public AutomationElement FindElementByClassSimple(AutomationElement element, string className, int timeout = 300)
        {
            AutomationElement targetElements = null;
            if (string.IsNullOrEmpty(className))
            {
                return targetElements;
            }
            do
            {
                if (element != null)
                {
                    targetElements = element.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ClassNameProperty, className));
                    if (targetElements !=  null)
                    {
                        return targetElements;
                    }
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElements;
        }

        /// <summary>
        /// 通过ID查找控件 集合
        /// </summary>
        /// <param name="element">控件节点</param>
        /// <param name="autoID">控件ID</param>
        /// <param name="timeout">延时</param>
        /// <returns>控件</returns>
        public AutomationElementCollection FindElementByAutomationID(AutomationElement element, string autoID, int timeout = 300)
        {
            AutomationElementCollection targetElements = null;
            if (string.IsNullOrEmpty(autoID))
            {
                return targetElements;
            }
            do
            {
                if (element != null)
                {
                    if (!string.IsNullOrEmpty(autoID))
                    {
                        targetElements = element.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.AutomationIdProperty, autoID));
                        if (targetElements.Count > 0)
                        {
                            return targetElements;
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElements;
        }


        /// <summary>
        /// 通过ID查找控件 
        /// </summary>
        /// <param name="element">控件节点</param>
        /// <param name="autoID">控件ID</param>
        /// <param name="timeout">延时</param>
        /// <returns>控件</returns>
        public AutomationElement FindElementByAutomationIDSimple(AutomationElement element, string autoID, int timeout = 300)
        {
            AutomationElement targetElements = null;
            if (string.IsNullOrEmpty(autoID))
            {
                return targetElements;
            }
            do
            {
                if (element != null)
                {
                    targetElements = element.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.AutomationIdProperty, autoID));
                    if (targetElements != null)
                    {
                        return targetElements;
                    }
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElements;
        }

        /// <summary>
        /// 通过名称查找控件 集合
        /// </summary>
        /// <param name="element">控件节点</param>
        /// <param name="Name">控件名称</param>
        /// <param name="timeout">延时</param>
        /// <returns>控件</returns>
        public AutomationElementCollection FindElementByName(AutomationElement element, string Name, int timeout = 300)
        {
            AutomationElementCollection targetElements = null;
            if (string.IsNullOrEmpty(Name))
            {
                return targetElements;
            }
            do
            {
                if (element != null)
                {
                    if (!string.IsNullOrEmpty(Name))
                    {
                        targetElements = element.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, Name));
                        if (targetElements.Count > 0)
                        {
                            return targetElements;
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElements;
        }

        /// <summary>
        /// 通过名称查找控件
        /// </summary>
        /// <param name="element">控件节点</param>
        /// <param name="Name">控件名称</param>
        /// <param name="timeout">延时</param>
        /// <returns>控件</returns>
        public AutomationElement FindElementByNameSimple(AutomationElement element, string Name, int timeout = 300)
        {
            AutomationElement targetElements = null;
            if (string.IsNullOrEmpty(Name))
            {
                return targetElements;
            }
            do
            {
                if (element != null)
                {
                    targetElements = element.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, Name));
                    if (targetElements != null)
                    {
                        return targetElements;
                    }
                    
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElements;
        }

        public AutomationElementCollection FindElementByClasssNameAndAutomationIDCollection(AutomationElement element, string _autoID, string _classname, int timeout = 300)
        {
            AutomationElementCollection targetElement = null;
            if (string.IsNullOrEmpty(_autoID) | string.IsNullOrEmpty(_classname))
            {
                return targetElement;
            }

            do
            {
                targetElement = element.FindAll(TreeScope.Subtree, new AndCondition(new PropertyCondition(AutomationElement.ClassNameProperty, _classname), new PropertyCondition(AutomationElement.AutomationIdProperty, _autoID)));
                if (targetElement != null)
                {
                    return targetElement;
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElement;
        }

        #endregion

        #region 多项查询控件

        /// <summary>
        /// 通过 Name ClassName 查找
        /// </summary>
        public AutomationElement FindElementByNameAndClassName(AutomationElement element, string _name, string _classname, int timeout = 300)
        {
            AutomationElement targetElement = null;
            do
            {
                targetElement = element.FindFirst(TreeScope.Subtree, new AndCondition(new PropertyCondition(AutomationElement.ClassNameProperty, _classname), new PropertyCondition(AutomationElement.NameProperty, _name)));
                if (targetElement != null)
                {
                    return targetElement;
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElement;
        }

        /// <summary>
        /// 通过 AutomationID ClassName 查找
        /// </summary>
        public AutomationElement FindElementByClasssNameAndAutomationID(AutomationElement element, string _autoID, string _classname, int timeout = 300)
        {
            AutomationElement targetElement = null;
            do
            {

                targetElement = element.FindFirst(TreeScope.Subtree, new AndCondition(new PropertyCondition(AutomationElement.ClassNameProperty, _classname), new PropertyCondition(AutomationElement.AutomationIdProperty, _autoID)));
                if (targetElement != null)
                {
                    return targetElement;
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElement;
        }

        /// <summary>
        /// 通过 AutomationID Name 查找
        /// </summary>
        public AutomationElement FindElementByNameAndAutomationID(AutomationElement element, string _autoID, string _name, int timeout = 300)
        {
            AutomationElement targetElement = null;
            do
            {
                targetElement = element.FindFirst(TreeScope.Subtree, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, _name), new PropertyCondition(AutomationElement.AutomationIdProperty, _autoID)));
                if (targetElement != null)
                {
                    return targetElement;
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return targetElement;
        }

        #endregion

        #region 模拟点击事件 与检查

        /// <summary>
        /// 模拟点击事件 
        /// </summary>
        /// <param name="automationElement">控件</param>
        public bool InvokeByAutomationElement(AutomationElement automationElement)
        {
            try
            {
                var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invokePattern.Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查点击事件是否完成
        /// </summary>
        public bool CheckInvokeIsFinishByAutomationElement(AutomationElement automationElement)
        {
            int timeout = 10;
            do
            {
                if (InvokeByAutomationElement(automationElement) == true | GetSupportPatternByAutomationElement(automationElement).Trim() != "Invoke")
                {
                    return true;
                }
                else
                {
                    InvokeByAutomationElement(automationElement);
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return false;
        }

        #endregion

        #region  模拟开关切换事件 

        /// <summary>
        /// 切换开关
        /// 控件
        /// 开关名称
        /// 窗口大小显示
        /// 开关状态
        /// </summary>
        public bool ToggleSwitch(AutomationElement element, string strToggle, bool windowSizeIsMax = true, bool ToggleIsOn = true)
        {
            try
            {
                bool boolflag = false;
                string tempStr = null;
               // CommonMethod cm = new CommonMethod();
                AutomationElement toggle = element;
                if (ToggleIsOn == true)
                {
                    boolflag = SetToggleStateByAutomationElement(toggle); //设置 开启
                }
                else
                {
                    boolflag = SetToggleStateByAutomationElement(toggle, false); //设置 关闭
                }
                Thread.Sleep(2000);
                if (windowSizeIsMax == true)
                {
                    tempStr = strToggle + "WindowsSizeMax" + "Set" + GetToggleStateByAutomationElement(toggle);
                }
                else
                {
                    tempStr = strToggle + "WindowsSizeNormal" + "Set" + GetToggleStateByAutomationElement(toggle);
                }
                //cm.TakeScreenshot(Constants.ScreenshotsPath + "\\" + tempStr + Constants.ScreenshotsPicName);   //设置开关状态 截图
                Thread.Sleep(2000);
                ToggleSwitchByAutomationElement(toggle);  //切换开关
                Thread.Sleep(2000);
                tempStr += "SwitchTo" + GetToggleStateByAutomationElement(toggle);
                //cm.TakeScreenshot(Constants.ScreenshotsPath + "\\" + tempStr + Constants.ScreenshotsPicName);   //切换完成后截图
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 开关切换
        /// </summary>
        /// <param name="automationElement"></param>
        public bool ToggleSwitchByAutomationElement(AutomationElement automationElement)
        {
            try
            {
                TogglePattern toggle = automationElement.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
                toggle.Toggle();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置开关状态
        /// </summary>
        /// <param name="automationElement"></param>
        /// <param name="ToggleIsON">true ：设置开启  false :设置关闭</param>
        /// <returns>true ：设置成功  false :设置失败</returns>
        public bool SetToggleStateByAutomationElement(AutomationElement automationElement,bool ToggleIsON = true, int timeout = 20)
        {
            try
            {
                TogglePattern toggle = automationElement.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
                if(ToggleIsON == true)  //设置开关状态为 On
                {
                    if (toggle.Current.ToggleState == ToggleState.Off)
                    {
                        toggle.Toggle();
                        do
                        {
                            if(toggle.Current.ToggleState == ToggleState.On)
                            {
                                return true;
                            }
                            System.Threading.Thread.Sleep(100);
                            timeout--;
                        } while (timeout > 0);
                        return false;
                    }
                    return true;
                }
                
                if(ToggleIsON == false)  //设置开关状态为 Off
                {
                     if (toggle.Current.ToggleState == ToggleState.On)
                     {
                        toggle.Toggle();
                        do
                        {
                            if (toggle.Current.ToggleState == ToggleState.Off)
                            {
                                return true;
                            }
                            System.Threading.Thread.Sleep(100);
                            timeout--;
                        } while (timeout > 0);
                        return false;
                     }
                     return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取当前开关状态
        /// </summary>
        /// <param name="automationElement"></param>
        /// <returns>开关状态 </returns>
        public string GetToggleStateByAutomationElement(AutomationElement automationElement)
        {
            try
            {
                TogglePattern toggle = automationElement.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
                return toggle.Current.ToggleState.ToString();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region ComboBox事件

        /// <summary>
        /// ComBox 展开事件
        /// </summary>
        /// <param name="element"> 控件</param>
        /// <param name="IsExpand"> true ： 展开 false: 隐藏</param>
        /// <param name="times">尝试次数</param>
        /// <returns></returns>
        public bool ExpandOrCollapseByAutomationElement(AutomationElement element, bool IsExpand = true ,int times = 20)
        {
            try
            {
                do
                {
                   
                    ExpandCollapsePattern expand = element.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                    if (IsExpand == true)
                    {
                        expand.Expand();
                        Thread.Sleep(3000);
                        //向下测试
                        //SendKeys.SendWait("{DOWN}");    //向下发送事件1次
                        if (element.GetCurrentPropertyValue(AutomationElement.IsExpandCollapsePatternAvailableProperty).ToString().ToLower().Trim() == "true")
                        {
                            return true;
                        }
                    }
                    else
                    {
                        expand.Collapse();
                        Thread.Sleep(100);
                        if (element.GetCurrentPropertyValue(AutomationElement.IsExpandCollapsePatternAvailableProperty).ToString().ToLower().Trim() == "false")
                        {
                            return true;
                        }
                    }
                    times--;
                } while (times > 0);
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool ExpandOrCollapseByAutomationElementTest(AutomationElement element, bool IsExpand = true, int times = 20)
        {
            try
            {
                do
                {

                    ExpandCollapsePattern expand = element.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                    if (IsExpand == true)
                    {
                        expand.Expand();
                        Thread.Sleep(3000);
                        //向下测试

                        if (element.GetCurrentPropertyValue(AutomationElement.IsExpandCollapsePatternAvailableProperty).ToString().ToLower().Trim() == "true")
                        {
                            return true;
                        }
                    }
                    else
                    {
                        expand.Collapse();
                        Thread.Sleep(100);
                        if (element.GetCurrentPropertyValue(AutomationElement.IsExpandCollapsePatternAvailableProperty).ToString().ToLower().Trim() == "false")
                        {
                            return true;
                        }
                    }
                    times--;
                } while (times > 0);
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ComBox 事件 选择子项
        /// </summary>
        public bool SelectComBoxByAutomationElementCollection(AutomationElementCollection elements = null,int InSetfalg = 0)
        {
            try
            {
                if(elements.Count < InSetfalg)
                {
                    InSetfalg = 0;
                }
                SelectionItemPattern selectionItem = elements[InSetfalg].GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                selectionItem.Select();
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public bool SelectComBoxByAutomationElement(AutomationElement element)
        {
            try
            {
                SelectionItemPattern selectionItem = element.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                selectionItem.Select();
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region RadioButton

        /// <summary>
        /// 模拟点击事件 
        /// </summary>
        /// <param name="automationElement">控件</param>
        public bool RadioButtonByAutomationElement(AutomationElement automationElement)
        {
            try
            {
                SelectionItemPattern selectionItemPattern = automationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                selectionItemPattern.Select();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Scrollbar

        /// <summary>
        /// 滚动条滑动操作
        /// </summary>
        /// <param name="automationElement"></param>
        /// <param name="IsLargeIncrement">较大增量</param>
        /// <param name="IsUpRight">向下 或 向右</param>
        /// <returns></returns>
        public bool ScrollBarByAutomationElement(AutomationElement automationElement,bool IsLargeIncrement = true,bool IsDownRight = true)
        {
            try
            {
                ScrollPattern scroll = automationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
                
                if (scroll.Current.VerticallyScrollable)   //垂直滚动
                {
                    if(IsLargeIncrement == true) //较大滑动
                    {
                        if(IsDownRight == true)
                        {
                            scroll.ScrollVertical(ScrollAmount.LargeIncrement);  //垂直向下
                        }
                        else
                        {
                            scroll.ScrollVertical(ScrollAmount.LargeDecrement);  //垂直向上
                        }
                        
                    }
                    else  //较小滑动
                    {
                        if (IsDownRight == true)
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallIncrement);  //垂直向下
                        }
                        else
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallDecrement);  //垂直向上
                        }
                    }
                    
                    return true;
                }
                else  //水平滚动
                {
                    if (IsLargeIncrement == true) // 较大滑动
                    {
                        if (IsDownRight == true)
                        {
                            scroll.ScrollVertical(ScrollAmount.LargeIncrement);  //水平 向右
                        }
                        else
                        {
                            scroll.ScrollVertical(ScrollAmount.LargeDecrement);  //水平 向左
                        }
                    }
                    else // 较小滑动
                    {
                        if (IsDownRight == true)
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallIncrement);  //水平 向右
                        }
                        else
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallDecrement);  //水平 向左
                        }
                    }
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// 默认 水平 向有滑动 
        /// </summary>
        public bool ScrollBarVerticallyHorizontallyByAutomationElement(AutomationElement automationElement, bool IsHorizontally = true, bool IsDownRight = true)
        {
            try
            {
                ScrollPattern scroll = automationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
                if(IsHorizontally == false)   //垂直滚动
                {
                    if(scroll.Current.HorizontallyScrollable == false)
                    {
                        if (IsDownRight == true)
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallIncrement);  //垂直向下
                        }
                        else
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallDecrement);  //垂直向上
                        }
                        return true;
                    }
                    return false;
                }
                else  //水平滚动
                {
                    if (scroll.Current.HorizontallyScrollable == true)
                    {
                        if (IsDownRight == true)
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallIncrement);  //水平 向右
                        }
                        else
                        {
                            scroll.ScrollVertical(ScrollAmount.SmallDecrement);  //水平 向左
                        }
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置滚动条值
        /// </summary>
        /// <param name="automationElement"></param>
        /// <param name="rangevale">值</param>
        /// <param name="ValueIsMax">true : 设置最大值   false ：自定义 </param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool ScrollItemBarSetValueByAutomationElement(AutomationElement automationElement,double rangevalue = 0,bool ValueIsMax = true,int timeout = 20)
        {
            try
            {
                RangeValuePattern range = automationElement.GetCurrentPattern(RangeValuePattern.Pattern) as RangeValuePattern;
                double intmax = range.Current.Maximum;
                double intmin = range.Current.Minimum;
                do
                {
                    if (ValueIsMax == true)
                    {
                        rangevalue = intmax;
                        range.SetValue(rangevalue);
                    }
                    else
                    {
                        range.SetValue(rangevalue);
                    }
                    if (range.Current.Value == rangevalue)
                    {
                        return true;
                    }
                    System.Threading.Thread.Sleep(100);
                    timeout--;
                } while (timeout > 0);
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 查询滚动条值
        /// </summary>
        /// <param name="automationElement"></param>
        /// <param name="ValueIsMax">true 获取最大值， false : 获取当前值</param>
        /// <returns></returns>
        public double ScrollItemBarGetValueByAutomationElement(AutomationElement automationElement,bool ValueIsMax = true,bool ValueIsCurrent = false)
        {
            try
            {
                RangeValuePattern range = automationElement.GetCurrentPattern(RangeValuePattern.Pattern) as RangeValuePattern;
                if(ValueIsCurrent == true)
                {
                    return range.Current.Value;
                }
                else
                {
                    if (ValueIsMax == true)
                    {
                        return range.Current.Maximum;
                    }
                    else
                    {
                        return range.Current.Minimum;
                    }
                }
               
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region 设置 读取 检查 文本内容

        /// <summary>
        /// 设置文本
        /// </summary>
        public bool SetTextByAutomationElement(AutomationElement automationElement,string TestData)
		{
			try
			{
				ValuePattern vpTextEdit = null;
				vpTextEdit = automationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
            	vpTextEdit.SetValue(TestData);
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// 读取文本
		/// </summary>
		public string ReadTextByAutomationElement(AutomationElement automationElement)
		{
			try
			{
				ValuePattern vpTextEdit = null;
				vpTextEdit = automationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
				if (vpTextEdit.Current.Value != null)
	            {
					return vpTextEdit.Current.Value;
	            }
				else
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
		}
		
		/// <summary>
		/// 检查文本，如果未设置成功，默认设置10次文本，若不成功，报错
		/// </summary>
		public bool CheckTextByAutomationElement(AutomationElement automationElement,string SetData, int timeout = 10)
		{
            do
            {
                if(SetTextByAutomationElement(automationElement,SetData) == true)
                {
                    int _timeout = 10;
                    do
                    {
                        System.Threading.Thread.Sleep(100);
                        if (ReadTextByAutomationElement(automationElement) != null)
                        {
                            return true;
                        }
                        else
                        {
                            SetTextByAutomationElement(automationElement, SetData);
                        }
                        System.Threading.Thread.Sleep(100);
                        _timeout--;
                    } while (_timeout > 0);
                    return false;
                }
                System.Threading.Thread.Sleep(100);
                timeout--;
            }
            while (timeout > 0);
            return false;
		}

        #endregion

        #region  返回坐标点
        public System.Windows.Point GetPointByAutomationElement(AutomationElement element, int times = 10)
        {
            System.Windows.Point p = new System.Windows.Point();
            try
            {
                
                do
                {
                    object prop = element.GetCurrentPropertyValue(AutomationElement.ClickablePointProperty);
                    if (prop is System.Windows.Point)
                    {
                        p = (System.Windows.Point)prop;
                        return p;
                    }
                    times--;
                } while (times >0);

                return p;
            }
            catch
            {
                return p;
            }
        }

        /// <summary>
        /// 通过 坐标点 查询事件
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public AutomationElement ElementFromCursor(System.Windows.Point p)
        {
            AutomationElement element = null;
            try
            {
                element = AutomationElement.FromPoint(p);
                return element;
            }
            catch
            {
                return element;
            }
        }
        #endregion

        #region 获取控件的属性 以及相应属性值

        /// <summary>
        /// 获取属性
        /// </summary>
        public string GetSupportPatternByAutomationElement(AutomationElement element)
        {
            string _Info = string.Empty;
			try
			{
				_Info =string.Empty;
				AutomationPattern[] patterns = element.GetSupportedPatterns();
	            foreach (AutomationPattern pattern in patterns)
	            {
	            	_Info += Automation.PatternName(pattern) +";";
	            }
			}
			catch
			{
				_Info = "No Action";
			}
			return _Info;
        }
	 	
	 	/// <summary>
        /// 获取属性值
	 	/// 0 ： Name;
	 	/// 1 ： ClassName
	 	/// 2 :  AutomationID
	 	/// 3 :  ControlType
        /// 4 :  Enable ?
        /// 5 :  clickPoint
	 	/// </summary>
	 	/// <param name="element"></param>
	 	/// <param name="_Flag"></param>
	 	/// <returns></returns>
	 	public string GetTypeStringByIntFromAutomationElement(AutomationElement element,int _Flag = 10)
	 	{
	 		string _TypeStr = string.Empty;
	 		try
	 		{
	 			object TypeDefault = null;
	 			switch(_Flag)
	 			{
	 				case 0:
                        TypeDefault = element.GetCurrentPropertyValue(AutomationElement.NameProperty, false);
	 					break;
	 				case 1:
	 					TypeDefault = element.GetCurrentPropertyValue(AutomationElement.ClassNameProperty);
	 					break;
	 				case 2:
	 					TypeDefault = element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty);
	 					break;
	 				case 3:
	 					TypeDefault = element.GetCurrentPropertyValue(AutomationElement.ControlTypeProperty);
	 					break;
                    case 4:
                        TypeDefault = element.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty);
                        if(TypeDefault.ToString().ToLower().Trim() == "false")
                        {
                            return "Disable";
                        }
                        else if(TypeDefault.ToString().ToLower().Trim() == "true")
                        {
                            return "Enable";
                        }
                        return "EnableIsUnknown";
                    case 5:
                        System.Windows.Point p = new System.Windows.Point();
                        TypeDefault = element.GetCurrentPropertyValue(AutomationElement.ClickablePointProperty);
                        if (TypeDefault is System.Windows.Point)
                        {
                            p = (System.Windows.Point)TypeDefault;
                        }
                        return p.X.ToString() + ',' + p.Y.ToString();
                    default :
	 						break;
	 			}
	 			if (TypeDefault == AutomationElement.NotSupported)
				{
				    _TypeStr = null;
				}
				else
				{
				    _TypeStr = TypeDefault as string;
				}
		 			
		 	}
	 		catch
	 		{
	 			_TypeStr = null;
	 		}
	 		return _TypeStr;
	 	}

        #endregion

    }
}
