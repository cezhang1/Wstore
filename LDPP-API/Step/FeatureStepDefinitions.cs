using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using LDPP_API.Common.Helper;
using System.IO;
using static LDPP_API.Common.Helper.Models;
using Newtonsoft.Json;
using NUnit.Framework.Internal.Execution;
using LDPPInterfaceTesting;
using System.Reflection;
using Microsoft.FSharp.Core;
using System.Threading;
using WindowsFormsApp1;
using LDPP_API.Common.DCLDPP;
using System.Security.RightsManagement;
using Newtonsoft.Json.Linq;
using NLog.Fluent;
using System.Diagnostics;
using ConsoleDebug;

namespace LDPP_API
{
    [Binding]
    internal class FeatureStepDefinitions
    {
        
        public static Start start = new Start();
        public static FormControl FC = new FormControl();



        [When(@"start exe and init exe")]
        public void WhenStartExeAndInitExe(Table table)
        {
            start.StartExe(table.Rows[0][1]);
            start.InitExe(FC);
            Assert.IsTrue(FC.StringArray!=null,"Init exe failed");
            //Thread.Sleep(2000);
        }

        [When(@"start discover device")]
        public void WhenStartDiscoverDevice(Table table)
        {
            Assert.IsTrue(start.TestStart(FC) == true, "fail to start discover device");
        }

        [When(@"start connect device")]
        public void WhenStartConnectDevice(Table table)
        {
            Assert.IsTrue(start.TestConnect(FC) == true, "fail to Connect discover device");
        }

        [When(@"start Disconnect device")]
        public void WhenStartDisconnectDevice(Table table)
        {
            Assert.IsTrue(start.TestDisConnect(FC) == true, "fail to DisConnect discover device");
        }


        [When(@"init done")]
        public void WhenInitDone(Table table)
        {
            Assert.IsTrue(start.IsOpenLdppPlugin() == true, "fail to OpenLdppPlugin");
        }


        [When(@"close all windows")]
        public void WhenCloseAllWindows(Table table)
        {
            Assert.IsTrue(start.CloseCmdWindow() == true, "fail to CloseCmdWindow");
            Assert.IsTrue(start.ClosePluginWindow() == true, "fail to ClosePluginWindow");
            Assert.IsTrue(start.CloseUnityWindow() == true, "fail to CloseUnityWindow");
        }


        [When(@"start done")]
        public void WhenStartDone(Table table)
        {
            Assert.IsTrue(start.IsDiscoverDevice(FC) == true, "fail to OpenLdppPlugin");
        }

    }
}
