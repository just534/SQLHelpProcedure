using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SQLHelperPro
{
    public class SQLHelp
    {
        public static string connstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        /// <summary>
        /// 执行增删改查的通用操作
        /// </summary>
        /// <param name="comdText">sql语句或者是储存过程名称</param>
        /// <param name="param">参数数组</param>
        /// <param name="IsProcedure">是否是执行储存过程</param>
        /// <returns></returns>
        public static int ExeNonQuery(string comdText,SqlParameter[] param=null,bool IsProcedure=false)
        {
            SqlConnection sqlcon = new SqlConnection(connstring);
            SqlCommand command = new SqlCommand(comdText, sqlcon);
            try
            {
                sqlcon.Open();
                if (param != null)
                {
                    command.Parameters.AddRange(param);//加入参数
                }
                if (IsProcedure)
                {
                    command.CommandType = CommandType.StoredProcedure;//执行存储过程
                }
                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                string errorMsg = "应用程序与数据库连接出错，具体内容： " + ex.Message;
                throw new Exception(errorMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }

        /// <summary>
        /// 执行查询操作，返回单一结果
        /// </summary>
        /// <param name="comdText">sql语句或者储存过程名称</param>
        /// <param name="param"></param>
        /// <param name="IsProcedure"></param>
        /// <returns></returns>
        public static object ExeScalar(string comdText,SqlParameter[] param = null, bool IsProcedure= false)
        {
            SqlConnection sqlcon = new SqlConnection(connstring);
            SqlCommand command = new SqlCommand(comdText, sqlcon);
            try
            {
                sqlcon.Open();
                if (param != null)
                {
                    command.Parameters.AddRange(param);//加入参数
                }
                if (IsProcedure)
                {
                    command.CommandType = CommandType.StoredProcedure;//执行存储过程
                }
                return command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                string errorMsg = "应用程序与数据库连接出错，具体内容： " + ex.Message;
                throw new Exception(errorMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }
       
        /// <summary>
        /// 执行查询操作，返回结果集
        /// </summary>
        /// <param name="comdText"></param>
        /// <param name="param"></param>
        /// <param name="IsProcedure"></param>
        /// <returns></returns>
        public static SqlDataReader ExeReader(string comdText,SqlParameter[] param=null,bool IsProcedure = false)
        {
            SqlConnection sqlcon = new SqlConnection(connstring);
            SqlCommand command = new SqlCommand(comdText, sqlcon);
            if (param != null)
            {
                command.Parameters.AddRange(param);
            }
            if (IsProcedure)
            {
                command.CommandType = CommandType.StoredProcedure;
            }
            try
            {
                sqlcon.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {

                throw new Exception("应用程序与数据库连接出错，具体内容： " + ex.Message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// DataSet结果集的查询
        /// </summary>
        /// <param name="comdText"></param>
        /// <param name="tableName">ds中要设置的数据表名称</param>
        /// <param name="param"></param>
        /// <param name="IsProcedure"></param>
        /// <returns></returns>
        public static DataSet ExeDataSet(string comdText, string tableName = null,SqlParameter[] param=null,bool IsProcedure = false)
        {
            SqlConnection sqlcon = new SqlConnection(connstring);
            SqlCommand command = new SqlCommand(comdText,sqlcon);
            try
            {
                if (param != null)
                {
                    command.Parameters.AddRange(param);
                }
                if (IsProcedure)
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                sqlcon.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                if (tableName != null)
                {
                    adapter.Fill(ds, tableName);
                }
                else
                {
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw new Exception("应用程序与数据库连接出错，具体内容： " + ex.Message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
           
            
        }


    }
}
