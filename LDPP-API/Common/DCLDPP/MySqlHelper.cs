using LDPP_API.Common.DCLDPP;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API
{
    public class MySqlHelper
    {

        //public static string connstr = "server=127.0.0.1;database=ldpp;username=root;password=123456;";
        /// <summary>
        public static string connstr = "server=rm-2ze4o377686492s7hlo.mysql.rds.aliyuncs.com;database=smart_engine_automated_test;username=auto_query;password=cN08i!Og5;";
        /// </summary>

        MySqlConnection conn;

        public MySqlHelper(string connstr)
        {
            this.conn = new MySqlConnection(connstr);
        }

        public void Open()
        {
            try
            {
                this.conn.Open();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Close()
        {
            try
            {
                this.conn.Close();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ExecuteNonQuery(string sqlStr)
        {
            MySqlCommand cmd = new MySqlCommand(sqlStr, this.conn);

            try
            {
                //this.conn.Open();
                int row = cmd.ExecuteNonQuery();
                return row;
            }
            catch (System.Data.SqlClient.SqlException e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                cmd.Dispose();
                //this.conn.Close();//tl
            }
            
            return 0;
        }

        //获取数据库文本数据
        public static MySqlDataReader ExecuteReader(string sqlStr, string connstr)
        {
            MySqlConnection connection = new MySqlConnection(connstr);
            MySqlCommand cmd = new MySqlCommand(sqlStr, connection);
            MySqlDataReader myReader = null;

            try
            {
                connection.Open();
                myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                connection.Close();
                throw new Exception(e.Message);
            }
            finally
            {
                if (myReader == null)
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }

        }


        #region 执行带参数的查询语句，返回DataSet

        /// <summary>
        /// 执行带参数的查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sqlString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connstr))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        //connection.Close();
                    }
                    return ds;
                }
            }
        }

        #endregion

        #region 装载MySqlCommand对象

        /// <summary>
        /// 装载MySqlCommand对象
        /// </summary>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText,
            MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text; //cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
        #endregion


        /// <summary>查询SQL 语句</summary>
        /// <param name="sqlCommand">sql 命令</param>
        /// <param name="sqlConnection">数据库连接实例</param>
        /// <param name="value">关键字</param>
        /// <returns></returns>
        public static string QueryKeyWordsValue(string sqlCommand, string keyWords, MySqlConnection sqlConnection)
        {
            string info = "";
            MySqlCommand cmd = new MySqlCommand(sqlCommand, sqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                info = reader[keyWords].ToString();
            }
            reader.Close();
            return info;
        }

        /// <summary>执行SQL 语句</summary>
        /// <param name="sqlCommand">sql 命令</param>
        /// <param name="sqlConnection">数据库连接实例</param>
        public static void ExecuteMysql(string sqlCommand, MySqlConnection sqlConnection)
        {
            MySqlCommand cmd = new MySqlCommand(sqlCommand, sqlConnection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>获取数据库配置信息</summary>
        /// <returns>数据库配置信息</returns>
        public static MySQL GetMysqlInfo()
        {
            SecurityHelper sh = new SecurityHelper();
            MySQL sql = new MySQL();
            sql.Server = sh.DESDecrypt(CommonHelper.ReadIniValue("MySQL", "Server", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            sql.Port = uint.Parse(CommonHelper.ReadIniValue("MySQL", "Port", FileConstants.BsaeConfigFile));
            if (CommonHelper.ReadIniValue("MySQL", "Is_Online", FileConstants.BsaeConfigFile) == "1")
            {
                sql.DataBase = sh.DESDecrypt(CommonHelper.ReadIniValue("MySQL", "OnlineDataBase", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            }
            if (CommonHelper.ReadIniValue("MySQL", "Is_Online", FileConstants.BsaeConfigFile) == "0")
            {
                sql.DataBase = sh.DESDecrypt(CommonHelper.ReadIniValue("MySQL", "TestDataBase", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            }

            sql.UserName = sh.DESDecrypt(CommonHelper.ReadIniValue("MySQL", "UserName", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            sql.Password = sh.DESDecrypt(CommonHelper.ReadIniValue("MySQL", "Password", FileConstants.BsaeConfigFile), FileConstants.key, FileConstants.iv);
            return sql;
        }

        /// <summary>获取数据库连接实例</summary>
        /// <returns>数据库连接实例</returns>
        public static MySqlConnection ConnectMysql()
        {
            MySqlConnection connection = null;
            MySQL mysql = GetMysqlInfo();
            try
            {
                //与数据库连接的信息
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                //用户名
                builder.UserID = mysql.UserName;
                //密码
                builder.Password = mysql.Password;
                //服务器地址
                builder.Server = mysql.Server;
                builder.Port = mysql.Port;
                //连接时的数据库
                builder.Database = mysql.DataBase;

                //定义与数据连接的链接
                connection = new MySqlConnection(builder.ConnectionString);
                //打开这个链接
                connection.Open();
                // WriteLogHelper.WriteLog("ConnectMysql() 连接成功");
                return connection;
            }
            catch (Exception ex)
            {
                //WriteLogHelper.WriteLog("ConnectMysql() Exception:" + ex.ToString());
            }
            return connection;
        }

        /// <summary>关闭数据库</summary>
        /// <param name="sqlConnection">数据库连接实例</param>
        public static void CloseMysql(MySqlConnection sqlConnection)
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        public static bool QueryIsExist(string sqlCommand, MySqlConnection sqlConnection)
        {
            MySqlCommand cmd = new MySqlCommand(sqlCommand, sqlConnection);
            int sum = Convert.ToInt32(cmd.ExecuteScalar());
            if (sum > 0)
            {
                return true;
            }
            return false;
        }
    }
}
