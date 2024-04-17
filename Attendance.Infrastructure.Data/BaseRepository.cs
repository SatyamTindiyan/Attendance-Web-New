using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data
{
    public class BaseRepository 
    {
        private SqlConnection _conn;
        private static string _sqlConnection;
        string errorMethodRoute = "OMMS.BAL.Common.LogHandler";
        // DapperConnection dapper = new DapperConnection(ConnectionFile.db_ANTSDBTemp);
        private string sPathName = "~/ErrorLogs";//HttpContext .Current.Server.MapPath("~/ErrorLogs");

        public BaseRepository(IConfiguration configuration)
        {
            _sqlConnection = configuration.GetConnectionString("Connection_ANTSDB");
        }
        public SqlConnection GetConnection()
        {
            SqlConnection _conn = new SqlConnection();
            string constr = _sqlConnection; //ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            return _conn = new SqlConnection(constr);

        }

        public static void OpenConnection(SqlConnection _conn)
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
            }

        }

        public static void CloseConnection(SqlConnection _conn)
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }

        }

        public void WriteErrorLog(Exception ex, string ControllerName = null, string ActionName = null)
        {
            try
            {
                string fileName = sPathName + "/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                // Check if file already exists. If yes, append it error.     
                if (File.Exists(fileName))
                {
                    File.AppendAllText(fileName, "New Error:" + DateTime.Now.ToString() + " On " + ControllerName + "/" + ActionName +
                        Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine);
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine("New Error:" + DateTime.Now.ToString() + " On " + ControllerName + "/" + ActionName);
                        sw.WriteLine(ex.Message);
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.Flush();
                        sw.Close();
                    }
                }

                if (ControllerName != null && ActionName != null)
                    SaveErrorInDB("CODE", ex.Message, ex.StackTrace, ControllerName, ActionName, DateTime.Now);

            }
            catch (Exception exx)
            {
                if (ControllerName != null && ActionName != null)
                    SaveErrorInDB("CODE", exx.Message, exx.StackTrace, ControllerName, ActionName, DateTime.Now);
            }
        }

        public void SaveErrorInDB(string ErrorFrom, string ExceptionMessage, string ExceptionStackTrack, string ControllerName, string ActionName, DateTime ExceptionLogTime)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("ErrorFrom", ErrorFrom);
                param.Add("ExceptionMessage", ExceptionMessage);
                param.Add("ExceptionStackTrack", ExceptionStackTrack);
                param.Add("ControllerName", ControllerName);
                param.Add("ActionName", ActionName);
                param.Add("ExceptionLogTime", ExceptionLogTime);

                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.Execute("sp_SaveExceptionLog", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

    }
}
