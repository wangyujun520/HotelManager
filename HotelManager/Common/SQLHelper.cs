using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    public class SQLHelper
    {
        //创建连接字符串
        private static readonly string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        #region 执行不带参的SQL语句
        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int QuerySingle(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();//打开数据连接
                SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();//返回受影响行数
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();//关闭数据库链接
            }
        }

        /// <summary>
        /// 查询单一结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();//打开数据连接
                SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();//关闭数据库链接
            }
        }

        /// <summary>
        /// 查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();//打开数据连接
                SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();//关闭数据库链接
                throw ex;
            }
        }
        #endregion

        #region 执行带参数的SQL语句
        /// <summary>
        /// 查询结果集
        /// </summary>
        /// <param name="sql">带参的查询</param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql,SqlParameter[] parameter)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();//打开数据连接
                cmd.Parameters.AddRange(parameter);//添加参数
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();//关闭数据库链接
                throw ex;
            }
        }

        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int QuerySingle(string sql,SqlParameter[] parameter)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();//打开数据连接
                cmd.Parameters.AddRange(parameter);//添加参数
                return cmd.ExecuteNonQuery();//返回受影响行数
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
            finally
            {
                conn.Close();//关闭数据库链接
            }
        }
        #endregion

        #region 分页        
        /// <summary>
        /// 设置一个等待执行的SqlCommand对象
        /// </summary>
        /// <param name="cmd">SqlCommand 对象，不允许空对象</param>
        /// <param name="conn">SqlConnection 对象，不允许空对象</param>
        /// <param name="commandText">Sql 语句</param>
        /// <param name="cmdParms">SqlParameters  对象,允许为空对象</param>
        private static void PrepareCommand(SqlCommand cmd, CommandType commandType, SqlConnection conn, string commandText, SqlParameter[] cmdParms)
        {
            //打开连接
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //设置SqlCommand对象
            cmd.Connection = conn;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 执行Sql 命令
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql语句/参数化sql语句/存储过程名</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>DataSet 对象</returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    return ds;
                }
            }
        }
        #endregion

        #region 存储过程的修改
        public static int UpdateByProcedure(string procedureName, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;//声明当前要执行的是存储过程
                cmd.CommandText = procedureName;//commandText只需要赋值存储过程名称即可
                cmd.Parameters.AddRange(param);//添加存储过程的参数
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 获取一张数据表
        /// <summary>
        /// 获取一张数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

    }
}
