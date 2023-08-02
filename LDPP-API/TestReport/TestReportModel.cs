using System.Collections.Generic;

namespace LDPPInterfaceTesting.TestReport
{
    public sealed class TestReportModel
    {
        /// <summary>
        /// 测试报告汇总
        /// </summary>
        public class TestReportSummary
        {
            /// <summary>任务ID</summary>
            public string task_id { get; set; }

            /// <summary>版本</summary>
            public string version { get; set; }

            /// <summary>报告标题</summary>
            public string title { get; set; }

            /// <summary>总数</summary>
            public int total { get; set; }

            /// <summary>通过总数</summary>
            public int passed { get; set; }

            /// <summary>失败总数</summary>
            public int failed { get; set; }

            /// <summary>警告总数</summary>
            public int warnings { get; set; }

            /// <summary>跳过总数</summary>
            public int skipped { get; set; }

            /// <summary>Jira link</summary>
            public string jira_link { get; set; }

            /// <summary>测试报告地址</summary>
            public string report_url { get; set; }

            /// <summary>设备 UUID</summary>
            public string machine_uuid { get; set; }

            /// <summary>设备 MAC Address</summary>
            public string machine_mac_address { get; set; }

            /// <summary>设备名称</summary>
            public string machine_type { get; set; }

            /// <summary>测试开始时间</summary>
            public string start_time { get; set; }

            /// <summary>测试描述</summary>
            public string desc { get; set; }

            /// <summary>测试结束时间</summary>
            public string end_time { get; set; }

            /// <summary>测试执行时长</summary>
            public int duration_time { get; set; }
        }

        /// <summary>
        /// 测试报告详细
        /// </summary>
        public class TestReportDetail
        {
            /// <summary>任务ID</summary>
            public string task_id { get; set; }

            /// <summary>版本</summary>
            public string version { get; set; }

            /// <summary>测试状态  0:失败 1:成功 2:警告 3:跳过</summary>
            public int status  { get; set; }

            /// <summary>功能名称</summary>
            public string feature_name { get; set; }

            /// <summary>功能描述</summary>
            public string feature_desc{ get; set; }

            /// <summary>功能</summary>
            public string tags { get; set; }

            /// <summary>场景名称</summary>
            public string scenario_name { get; set; }

            /// <summary>接口名称</summary>
            public string api_name { get; set; }

            /// <summary>优先级 0:blocker 1:critical  2:normal  3:minor  4:trivial</summary>
            public int severity { get; set; }

            /// <summary>测试步骤信息地址</summary>
            public string step_url { get; set; }

            /// <summary>测试报告地址</summary>
            public string report_url { get; set; }

            /// <summary>测试步骤Jira地址</summary>
            public string jira_link { get; set; }

            /// <summary>设备 UUID</summary>
            public string machine_uuid { get; set; }

            /// <summary>设备 MAC Address</summary>
            public string machine_mac_address { get; set; }

            /// <summary>设备名称</summary>
            public string machine_type { get; set; }

            /// <summary>测试开始时间</summary>
            public string sc_start_time { get; set; }

            /// <summary>测试描述</summary>
            public string desc { get; set; }

            /// <summary>测试结束时间</summary>
            public string sc_end_time { get; set; }

            /// <summary>测试执行时长</summary>
            public int sc_duration_time { get; set; }

            public int version_type { get; set; }

        }

        /// <summary>
        /// 测试步骤字段
        /// </summary>
        public class StepInfo
        {
            public string type { get; set; }

            public string name { get; set; }

            public string status { get; set; }

            public StepDetailInfo detail { get; set; }

        }
        /// <summary>
        /// 测试步骤详细信息
        /// </summary>
        public class StepDetailInfo
        {
            /// <summary>测试数据</summary>
            public string data_info { get; set; }

            /// <summary>测试错误信息</summary>
            public string error_info { get; set; }

            /// <summary>图像信息</summary>
            public List<string> images { get; set; }

            /// <summary>日志信息</summary>
            public List<string> logs { get; set; }

        }


    }
}
