using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using LDPP_API.Common.DCLDPP;
using static LDPPInterfaceTesting.TestReport.TestReportModel;
using System.IO;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;
using LDPPInterfaceTesting.TestReport;

namespace LDPP_API
{
    [Binding]
    public sealed class Hooks
    {
        public static bool IsPass = true;
        public static bool IsRequireApprove = false;


        CommonHelper common = new CommonHelper();
        FileConstants Constants = new FileConstants();

        private static DateTime TaskStartTime;
        private static string taskid = null;
        public static string connstr = "server=rm-2ze4o377686492s7hlo.mysql.rds.aliyuncs.com;database=smart_engine_automated_test;username=auto_query;password=cN08i!Og5;";

        //是否开启调试， 测试调试时，请将此值，改为 true
        public static bool DebugFlag = false;

        //测试报告存放路径
        public static string testReportPath = null;
        public static string testReportDataFile = null;

        //详细测试报告
        public static TestReportDetail reportDetail;


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                TaskStartTime = DateTime.Now;
                //测试开始前 数据清理操作
                BeforeTestRunFirst();

                reportDetail = new TestReportDetail();
                if (DebugFlag == false)
                {
                    reportDetail.task_id = CommonHelper.ReadIniValue("Task", "task_id", FileConstants.TaskConfigFile);
                    reportDetail.machine_mac_address = CommonHelper.ReadIniValue("Task", "machine_mac_address", FileConstants.TaskConfigFile);
                    reportDetail.machine_uuid = CommonHelper.ReadIniValue("Task", "machine_uuid", FileConstants.TaskConfigFile);
                    reportDetail.machine_type = CommonHelper.ReadIniValue("Task", "machine_type", FileConstants.TaskConfigFile);
                    reportDetail.version = CommonHelper.ReadIniValue("Task", "dc_version", FileConstants.TaskConfigFile);//tl
                    testReportPath = Path.Combine(FileConstants.TestReportBastPath, reportDetail.version + "/" + reportDetail.machine_uuid + "/" + reportDetail.machine_type + TaskStartTime.ToString("yyyy_MM_dd_HH_mm_ss"));
                    reportDetail.report_url = "test-report/" + testReportPath.Replace("\\", "/") + "/index.html";
                    testReportDataFile = Path.Combine(FileConstants.TestReportDataPath, reportDetail.version + "/" + reportDetail.machine_uuid + "/" + reportDetail.machine_type + TaskStartTime.ToString("yyyy_MM_dd_HH_mm_ss") + ".zip");
                    reportDetail.version_type = int.Parse(CommonHelper.ReadIniValue("Task", "version_type", FileConstants.TaskConfigFile));
                }

                //新增20230426
                //BeforeTestRunTaskStatusUpdate();
            }
            catch (Exception ex)
            {
                //程序异常后，重启客户端,停止测试脚本
                Process.Start(FileConstants.TaskClientStopFile);
                Thread.Sleep(3000);
                Process.Start(FileConstants.TaskClientStartFile);
                Thread.Sleep(3000);
                Process.Start(FileConstants.TestScriptStopFile);//停止自己的脚本执行
            }
            
        }
        public static void BeforeTestRunFirst()
        {
            if (!Directory.Exists(FileConstants.TestHistoryPath))
            {
                Directory.CreateDirectory(FileConstants.TestHistoryPath);
            }
            DirectoryInfo root = new DirectoryInfo(FileConstants.TestRootPath);
            FileInfo[] zip_files = root.GetFiles("*.zip", SearchOption.TopDirectoryOnly);
            FileInfo[] log_files = root.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
            FileInfo[] txt_files = root.GetFiles("*.log", SearchOption.TopDirectoryOnly);
            FileInfo[] xml_files = root.GetFiles("*.xml", SearchOption.TopDirectoryOnly);

            //历史测试报告存放位置
            foreach (var f in zip_files)
            {
                File.Move(f.FullName, Path.Combine(FileConstants.TestHistoryPath, f.Name));
            }

            //删除临时无效文件
            foreach (var f in log_files)
            {
                if (f.LastWriteTime < DateTime.Now.AddDays(-1))
                {
                    File.Delete(f.FullName);
                }
            }

            foreach (var f in txt_files)
            {
                if (f.LastWriteTime < DateTime.Now.AddDays(-1))
                {
                    File.Delete(f.FullName);
                }
            }

            foreach (var f in xml_files)
            {
                File.Delete(f.FullName);
            }

            //删除上一次测试结果
            if (Directory.Exists(FileConstants.TestAllureReportPath))
            {
                Directory.Delete(FileConstants.TestAllureReportPath, true);
            }

            if (!Directory.Exists(FileConstants.TestAllureReportPath))
            {
                Directory.CreateDirectory(FileConstants.TestAllureReportPath);
            }

            //删除上一次日志截图
            if (Directory.Exists(FileConstants.TestLogPath))
            {
                Directory.Delete(FileConstants.TestLogPath, true);
            }

            if (Directory.Exists(FileConstants.TestScreenShotPath))
            {
                Directory.Delete(FileConstants.TestScreenShotPath, true);
            }
        }
        public static void BeforeTestRunTaskStatusUpdate()
        {
            MySqlConnection sqlConnection = MySqlHelper.ConnectMysql();
            string countQuery = $"SELECT * FROM tb_agent_task_info WHERE task_id = '{reportDetail.task_id}'";
            int runCount = int.Parse(MySqlHelper.QueryKeyWordsValue(countQuery, "run_count", sqlConnection));
            int testCount = int.Parse(MySqlHelper.QueryKeyWordsValue(countQuery, "test_count", sqlConnection));
            if (runCount == 0)
            {
                string sqlLine1 = $"UPDATE tb_agent_task_info SET status = '1', start_time = '{TaskStartTime}' WHERE task_id = '{reportDetail.task_id}'";
                MySqlHelper.ExecuteMysql(sqlLine1, sqlConnection);
            }
            else
            {
                if (runCount + 1 > testCount)
                {
                    DebugFlag = true;
                    string sqlLine1 = $"UPDATE tb_agent_task_info SET status = '2', update_time = '{TaskStartTime}' WHERE task_id = '{reportDetail.task_id}'";
                    MySqlHelper.ExecuteMysql(sqlLine1, sqlConnection);

                    //检测到已执行完成，重启客户端,停止测试脚本
                    Process.Start(FileConstants.TaskClientStopFile);
                    Thread.Sleep(3000);
                    Process.Start(FileConstants.TaskClientStartFile);
                    Thread.Sleep(3000);
                    Process.Start(FileConstants.TestScriptStopFile);
                }
            }
            MySqlHelper.CloseMysql(sqlConnection);
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            //更新任务状态及测试结果
            UpdateTaskStatusAndTestResult();

            //测试报告生成与上传
            try
            {
                TestReportHelper test = new TestReportHelper();
                test.GenerateTestReport();
                Thread.Sleep(3000);
                test.TestReportUpload();
            }
            catch { }

            //所有测试结束后，重启客户端,停止测试脚本
            Process.Start(FileConstants.TaskClientStopFile);
            Thread.Sleep(3000);
            Process.Start(FileConstants.TaskClientStartFile);
            Thread.Sleep(3000);
            Process.Start(FileConstants.TestScriptStopFile);

            //taskid
            //string taskId = CommonHelper.ReadIniValue("Task", "task_id", FileConstants.TaskConfigFile);
            ////title
            //string taskVertion = CommonHelper.ReadIniValue("Task", "dc_version", FileConstants.TaskConfigFile);
            //string taskTitle = taskVertion + "DC接口测试";
            //LogOut.WriteInfoLog("taskId : " + taskId);
            //LogOut.WriteInfoLog("taskVertion : " + taskVertion);
            //LogOut.WriteInfoLog("taskTitle : " + taskTitle);
            //int taskPassed = TestContext.CurrentContext.Result.PassCount; //Passed
            //int taskFailed = TestContext.CurrentContext.Result.FailCount; //Failed
            //int taskWarning = TestContext.CurrentContext.Result.WarningCount; //Warning
            //int taskSkiped = TestContext.CurrentContext.Result.SkipCount; //Skiped
            //int taskTotal = taskPassed + taskFailed + taskWarning + taskSkiped; //total


            //string TaskEndTime = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");

            //TimeSpan durationTimeSpan = Convert.ToDateTime(TaskEndTime) - Convert.ToDateTime(TaskStartTime);
            //int taskDuration = (int)(durationTimeSpan.TotalSeconds);

            //MySqlHelper _mysql = new MySqlHelper(connstr);
            //_mysql.Open();

            ////string sqlLine1 = "INSERT INTO  tb_ldpp_interface_report_info  task_id VALUES (\" + Convert.ToInt64(taskId) + \")";
            ////int re = _mysql.ExecuteNonQuery(sqlLine1);

            //string sqlLine1 = "INSERT INTO  tb_ldpp_interface_report_info (task_id,VERSION,title,total,passed,failed,WARNINGS,skipped,start_time,end_time,duration_time) VALUES('" + taskId + "', '" + taskVertion + "','" + taskTitle + "'," + taskTotal + "," + taskPassed + "," + taskFailed + "," + taskWarning + "," + taskSkiped + ",'" + TaskStartTime + "','" + TaskEndTime + "','" + taskDuration + "')";

            //int re = _mysql.ExecuteNonQuery(sqlLine1);


            //string sqltask = "UPDATE tb_agent_task_info SET status = '" + 2 + "',task_end_reason = '" + 2 + "',end_time = '"+ TaskEndTime + "' where task_id = '" + taskId + "'";
            //re = _mysql.ExecuteNonQuery(sqltask);


            //_mysql.Close();


            //////CLOSE client
            ////string FileName = @"C:\LenovoSmartEngineClient\TestScripts\agent_stop.bat";
            ////Process.Start(FileName);
            ////Thread.Sleep(3000);
            //////OPEN client 
            ////FileName = @"C:\LenovoSmartEngineClient\TestScripts\agent_start.bat";
            ////Process.Start(FileName);
            ////Thread.Sleep(3000);

            //LogOut.WriteInfoLog("status : " + 2);
            //LogOut.WriteInfoLog("task_end_reason : " + 2);
            //LogOut.WriteInfoLog("end_time : " + TaskEndTime);
        }
        private static void UpdateTaskStatusAndTestResult()
        {
            try
            {
                string _sTaskStartTime = TaskStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                string TaskEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int taskPassed = 0; //Passed
                int taskFailed = 0; //Failed
                int taskWarning = 0; //Warning
                int taskSkiped = 0; //Skiped


                TimeSpan durationTimeSpan = Convert.ToDateTime(TaskEndTime) - Convert.ToDateTime(_sTaskStartTime);
                int taskDuration = (int)(durationTimeSpan.TotalSeconds);

                using (MySqlConnection sqlConnection = MySqlHelper.ConnectMysql())
                {
                    //测试结果更新
                    string result_sql = $"SELECT STATUS,COUNT(STATUS) AS total_count FROM tb_ldpp_interface_report_detail_info WHERE task_id = '{reportDetail.task_id}' AND sc_start_time >= '{TaskStartTime}' AND sc_end_time <= '{TaskEndTime}' GROUP BY STATUS";//更新为DC的数据表格
                    MySqlCommand cmd = new MySqlCommand(result_sql, sqlConnection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        switch (int.Parse(reader["status"].ToString()))
                        {
                            case 0:
                                taskFailed = int.Parse(reader["total_count"].ToString());
                                break;
                            case 1:
                                taskPassed = int.Parse(reader["total_count"].ToString());
                                break;
                            case 2:
                                taskWarning = int.Parse(reader["total_count"].ToString());
                                break;
                            case 3:
                                taskSkiped = int.Parse(reader["total_count"].ToString());
                                break;
                        }
                    }
                    reader.Close();

                    int taskTotal = taskPassed + taskFailed + taskWarning + taskSkiped; //total

                    string taskTitle = reportDetail.version + "接口测试";
                    string report_download_url = "test-report/" + testReportDataFile.Replace("\\", "/");
                    string sqlLine1 = $"insert into tb_ldpp_interface_report_summary_info(task_id,version,title,total,passed,failed,warnings,skipped,start_time,end_time,duration_time,machine_mac_address,machine_type,machine_uuid,report_url,jira_link,report_download_url,version_type)" +
                       $"values('{reportDetail.task_id}','{reportDetail.version}','{taskTitle}','{taskTotal}','{taskPassed}' ,'{taskFailed}','{taskWarning}','{taskSkiped}','{TaskStartTime}','{TaskEndTime}'" +
                       $",'{taskDuration}','{reportDetail.machine_mac_address}','{reportDetail.machine_type}','{reportDetail.machine_uuid}','{reportDetail.report_url}','{reportDetail.jira_link}','{report_download_url}',{reportDetail.version_type})";//更新为DC的数据库表格
                    MySqlHelper.ExecuteMysql(sqlLine1, sqlConnection);

                    //任务状态更新
                    string countQuery = $"SELECT * FROM tb_agent_task_info WHERE task_id = '{reportDetail.task_id}'";
                    int runCount = int.Parse(MySqlHelper.QueryKeyWordsValue(countQuery, "run_count", sqlConnection));
                    int testCount = int.Parse(MySqlHelper.QueryKeyWordsValue(countQuery, "test_count", sqlConnection));
                    runCount++;
                    if (runCount < testCount)
                    {
                        string updateRun_count = $"UPDATE tb_agent_task_info SET run_count = '{runCount}',update_time = '{DateTime.Now}' WHERE task_id = '{reportDetail.task_id}'";
                        MySqlHelper.ExecuteMysql(updateRun_count, sqlConnection);
                    }
                    else
                    {
                        string updateStatusEnd = $"UPDATE tb_agent_task_info SET status = '2',run_count = '{runCount}', end_time = '{DateTime.Now}' WHERE task_id = '{reportDetail.task_id}'";
                        MySqlHelper.ExecuteMysql(updateStatusEnd, sqlConnection);
                    }
                }

            }
            catch { }
        }
        [BeforeFeature]
        public static void BeforeFeature()
        {
            reportDetail.feature_name = FeatureContext.Current.FeatureInfo.Title;
            reportDetail.feature_desc = FeatureContext.Current.FeatureInfo.Description;
        }
        [AfterFeature]
        public static void AfterFeature()
        {
            
        }
        [BeforeFeature]
        public static void BeforeStep()
        {
            
        }


        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // 关联测试用例失败了，直接结束执行。
            if (IsRequireApprove == true && IsPass == false )
           {
                // 重置IsPass的值
               IsPass = true;
               Assert.Fail("前面步骤运行结果异常，无法继续执行");

            }

        }
        [BeforeScenario]
        public static void BeforeScenario()
        {
            reportDetail.status = 10; //设置默认值
            reportDetail.sc_start_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            reportDetail.scenario_name = ScenarioContext.Current.ScenarioInfo.Title;
            reportDetail.api_name = ScenarioContext.Current.ScenarioInfo.Title;
            reportDetail.tags = string.Join(";", ScenarioContext.Current.ScenarioInfo.Tags);
            reportDetail.severity = 2;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //if(ScenarioContext.Current.TestError != null)
            //{
            //    IsPass = false;
            //}
            //else
            //{
            //    IsPass = true;
            //}
            if (ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.UndefinedStep)
            {
                TestReportHelper test = new TestReportHelper();
                test.TestReportAddLogInfo(TestContext.CurrentContext.Result.Message);
                test.TestErrorScreenShot();
            }

            reportDetail.sc_end_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            TimeSpan durationTimeSpan = DateTime.Parse(reportDetail.sc_end_time).Subtract(DateTime.Parse(reportDetail.sc_start_time));
            reportDetail.sc_duration_time = (int)(durationTimeSpan.TotalSeconds);

            if (ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.OK && reportDetail.status == 10)
            {
                reportDetail.status = 1;
            }
            else
            {
                if (reportDetail.status == 10)
                {
                    reportDetail.status = 0;
                }
                if (ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.UndefinedStep)
                {
                    reportDetail.status = 2;
                }
            }

            if (DebugFlag == false)
            {
                UpdateScenarioInfoToMysql();
            }

            if (reportDetail.status == 3)
            {
                Assert.Ignore(TestContext.CurrentContext.Result.Message);
            }
            if (reportDetail.status == 2)
            {
                Assert.Inconclusive("请手动验证，该测试用例是否通过");
            }

        }

        private void UpdateScenarioInfoToMysql()
        {
            using (MySqlConnection sqlConnection = MySqlHelper.ConnectMysql())
            {
                //更新为DC
                string sqlcommand = $"insert into tb_ldpp_interface_report_detail_info (task_id,version,status,feature_name,feature_desc," +
                    $"tags,scenario_name,api_name,severity,machine_uuid," +
                    $"machine_mac_address,machine_type,sc_start_time,sc_end_time," +
                    $"sc_duration_time,description,report_url,step_url,jira_link,version_type)" +
                    $"values('{reportDetail.task_id}','{reportDetail.version}',{reportDetail.status},'{reportDetail.feature_name}','{reportDetail.feature_desc}' ," +
                    $"'{reportDetail.tags}','{reportDetail.scenario_name}','{reportDetail.api_name}',{reportDetail.severity},'{reportDetail.machine_uuid}'," +
                    $"'{reportDetail.machine_mac_address}','{reportDetail.machine_type}','{reportDetail.sc_start_time}','{reportDetail.sc_end_time}'," +
                    $"{reportDetail.sc_duration_time},'{reportDetail.desc}','{reportDetail.report_url}','{reportDetail.step_url}','{reportDetail.jira_link}',{reportDetail.version_type})";
                MySqlHelper.ExecuteMysql(sqlcommand, sqlConnection);
                foreach (var tag in ScenarioContext.Current.ScenarioInfo.Tags)
                {
                    //tb_smart_engine_common_report_keywords_info为公共表
                    sqlcommand = $"select * from tb_smart_engine_common_report_keywords_info where type={reportDetail.version_type} and feature_name='{reportDetail.feature_name}' and tags='{tag}' and scenario_name='{reportDetail.scenario_name}' and api_name = '{reportDetail.api_name}'";

                    if (!MySqlHelper.QueryIsExist(sqlcommand, sqlConnection))
                    {
                        sqlcommand = $"insert into tb_smart_engine_common_report_keywords_info (feature_name,tags,scenario_name,api_name,type) values( " +
                            $"'{reportDetail.feature_name}', '{tag}', '{reportDetail.scenario_name}', '{reportDetail.api_name}',{reportDetail.version_type})";
                        MySqlHelper.ExecuteMysql(sqlcommand, sqlConnection);
                    }
                }
            }
        }

        [AfterStep]
        public void AfterStep()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TestReportHelper test = new TestReportHelper();
                test.TestReportAddLogInfo(ScenarioContext.Current.TestError.Message);
                test.TestErrorScreenShot();
            }
        }
    }
}