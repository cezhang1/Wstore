using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.DCLDPP
{
    internal class CommonModels
    {

    }

    #region Mysql 

    public class MySQL
    {
        /// <summary>Mysql 用户名</summary>
        public string UserName { get; set; }

        // <summary>Mysql 密码</summary>
        public string Password { get; set; }

        /// <summary>Mysql Server</summary>
        public string Server { get; set; }

        /// <summary>Mysql Port</summary>
        public uint Port { get; set; }

        public string DataBase { get; set; }

        /// <summary>Mysql DataBase 测试库</summary>
        public string TestDataBase { get; set; }

        /// <summary>Mysql DataBase 线上库</summary>
        public string OnlineDataBase { get; set; }

        public int Is_Online { get; set; }

    }

    #endregion
    #region FTP

    public class FTP
    {
        public string FtpName { get; set; }

        /// <summary>FTP Server</summary>
        public string Server { get; set; }

        /// <summary>FTP Port</summary>
        public uint Port { get; set; }

        /// <summary>FTP 用户名 </summary>
        public string UserName { get; set; }

        /// <summary>FTP 密码 </summary>
        public string Password { get; set; }

        /// <summary>远程路径</summary>
        public string RemotePath { get; set; }

    }
    public enum FTPTYPE
    {
        FTP_APP_SE,
        FTP_APP_DC,
        FTP_APP_VANTAGE,
        FTP_APP_PCM,
        FTP_PA_Automated_Test_Report,
        FTP_ETL_SE

    }
    #endregion
}
