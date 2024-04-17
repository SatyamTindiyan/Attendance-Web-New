using Attendance.Domain.Interfaces.Account;
using Attendance.Domain.Interfaces.Attendance;
using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.Attendance.ViewModel;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data.Repositories.Attendance
{
    public class AttendanceRepository : BaseRepository, IAttendanceRepository
    {
        private SqlConnection _conn;
        string errorMethodRoute = "";

        public AttendanceRepository(IConfiguration configuration) : base(configuration)
        {
        }

        //function for mark attendance intime
        public int InTimeMark(AttendanceRemote modal, string timezone2)
        {
            int result = 0;
            AttendaceResponce responce = new AttendaceResponce();
            try
            {
                DynamicParameters param = new DynamicParameters();
                //param.Add("countryid", MySession.Countryid);
                param.Add("LOGINID", modal.LOGINID);
                param.Add("INTIMETZ",modal.INTIMETZ);
                param.Add("UTCTimezone", timezone2);
                param.Add("INLAT", modal.INLAT);
                param.Add("INLOG", modal.INLOG);
                // param.Add("RADIALINDISTANCE", modal.RADIALINDISTANCE);
                // param.Add("ARDATE", modal.AR_DATE);
                // param.Add("MONTHNAME", modal.MONTHNAME);
                //param.Add("YEARNAME", modal.YEARNAME);
                param.Add("ONDUTY", modal.ONDUTY);
                param.Add("VER", modal.VER);
                param.Add("BRAND", modal.BRAND);
                param.Add("DEV_ID", modal.DEV_ID);
                param.Add("DEVICE", modal.DEVICE);
                param.Add("IP_ADDREESS", modal.IP_ADDREESS);
                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("Sp_InsertdailyAttendance", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "InTimeMark");
            }
            return result;
        }

        //function for get emp. in & out details
        public AttendanceRemote GetInOut(AttendanceRemote modal)
        {
            //AttendanceRemote modal = new AttendanceRemote();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            string date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            // string strResult = "";
            AttendanceRemote attendance = null;
            attendance = new AttendanceRemote();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("LOGINID", modal.LOGINID);
                param.Add("ARDATE", date);
                //param.Add("Empid", MySession.EmpId);
                //param.Add("LType", Convert.ToInt32(1));
                _conn = GetConnection();
                OpenConnection(_conn);
                attendance = _conn.Query<AttendanceRemote>("Sp_GetInOut_v1", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetInOut");
            }
            return attendance;
        }

        //function for mark attendance outtime
        public int OutTimeMark(AttendanceRemote modal)
        {
            int result = 0;
            AttendanceRemote attendance = null;
            attendance = new AttendanceRemote();
            try
            {
                DynamicParameters param = new DynamicParameters();
                //param.Add("countryid", MySession.Countryid);
                param.Add("ID", modal.ID);
                param.Add("LOGINID", modal.LOGINID);
                param.Add("OutTimeTZ", modal.OUTTIMETZ);
                param.Add("Outlat", modal.OUTLAT);
                param.Add("Outlog", modal.OUTLOG);
                //param.Add("RADIALOUTDISTANCE", modal.RADIALOUTDISTANCE);

                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("Sp_UpdateOuttime", param, commandType: CommandType.StoredProcedure).ToString();

                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);


            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "OutTimeMark");
            }
            return result;

        }

        //function for mark team attendance intime
        public int TeamInTimeMark(TeamAttendanceRemote modal, string timezone2)
        {
            int result = 0;
            TeamAttendanceRemote attendance = null;
            attendance = new TeamAttendanceRemote();
            try
            {
                //modal.INTIME = "";
                DynamicParameters param = new DynamicParameters();
                param.Add("@LOGINID", modal.LOGINID);
                param.Add("@EMPID", modal.EMPID);
                param.Add("@EMPTYPE", modal.EMPTYPE);
                param.Add("@VENDORID", modal.SUPPLIERID);  
                param.Add("@EMPNAME", modal.EMPNAME);
                param.Add("@INTIMETZ", modal.INTIME);
                param.Add("@UTCTimezone", timezone2);   
                param.Add("@INLAT", modal.INLAT);
                param.Add("@INLOG", modal.INLOG);
             
               param.Add("RADIALINDISTANCE", modal.RADIALINDISTANCE);
                //param.Add("ARDATE", modal.ARDATE);
                //param.Add("MONTHNAME", modal.MONTHNAME);
                //param.Add("YEARNAME", modal.YEARNAME);
                param.Add("@ONDUTY", modal.ONDUTY);
                param.Add("@DEVICE", modal.DEVICE);
                param.Add("@VER", modal.VER);
                param.Add("@BRAND", modal.BRAND);
                param.Add("@DEV_ID", modal.DEV_ID);
                param.Add("@IP_ADDREESS", modal.IP_ADDREESS);
                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("Sp_InsertdailyTeamAttendanceVendor_New", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);

                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "TeamInTimeMark");
            }
            return result;
        } 

        //function for mark team attendance outtime
        public int TeamOutTimeMark(TeamInOutTime modal)
        {
            int result = 0;
            AttendanceRemote attendance = null;
            attendance = new AttendanceRemote();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", Convert.ToInt32(modal.ID));
                param.Add("@OutTime", modal.OUTTIME);
                param.Add("@Outlat", modal.OUTLAT);
                param.Add("@Outlog", modal.OUTLOG);
                param.Add("RADIALOUTDISTANCE", modal.RADIALOUTDISTANCE);

                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("Sp_UpdateTeamOuttime", param, commandType: CommandType.StoredProcedure).ToString();

                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);

            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "TeamOutTimeMark");
            }
            return result;
        }

        //function to get regularization 
        public AttendanceSummary Regularization(string empid, int month, int year)
        {
            AttendanceSummary attendance = null;
            attendance = new AttendanceSummary();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", empid);
                param.Add("MONTH", month);
                param.Add("YEAR", year);
                _conn = GetConnection();
                OpenConnection(_conn);
                attendance = _conn.Query<AttendanceSummary>("EmpAttendanceSummary", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "Regularization");
            }
            return attendance;
        }
        // Get Attendance report for Regularization //
        public List<AttenHistory> GetAttendanceReportList(AttendanceRemote model)
        {
            List<AttenHistory> lstattendance = new List<AttenHistory>().ToList();

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", model.EMPID);
                param.Add("MONTHNAME", model.MONTHNAME);
                //param.Add("INTIME", model.INTIMETZ);
                //param.Add("OUTTIME", model.OUTTIMETZ);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<AttenHistory>("Sp_GetdataAttendance", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceReportList");
            }
            return lstattendance;
        }

        //function for get team in & out details
        public List<TeamInOutTime> TeamGetInOutTime(TeamInOutTime model)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            string date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            // string strResult = "";
            //TeamInOutTime attendance = null;
            List<TeamInOutTime> attendance = new List<TeamInOutTime>();

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Empid", model.Empid);
                param.Add("TeamEmpid", model.TeamEmpid);
                param.Add("Ar_Date", date);
                _conn = GetConnection();
                OpenConnection(_conn);
                //var result = _conn.Query<AttendanceRemote>("Sp_GetInOut", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                attendance = _conn.Query<TeamInOutTime>("SP_TeamAttendanceGetInORout", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetInOut");
            }
            return attendance;
        }

        // function to get attendance details
        public List<AttenDetail> GetAttendanceDetailList(DateSearch model)
        {
            List<AttenDetail> lstattendance = new List<AttenDetail>().ToList();

            try
            {
                DynamicParameters param = new DynamicParameters();
                var Fromdate = model.fromdate;
                var Todate = model.todate;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                var fromdate = Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                var todate = Convert.ToDateTime(Todate).ToString("yyyy-MM-dd");

                param.Add("fromdate", fromdate);
                param.Add("todate", todate);
                param.Add("compid", Convert.ToInt32(model.counid));
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<AttenDetail>("Sp_AttendanceDetails", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceDetailList");
            }
            return lstattendance;
        }

        //function to get summary of Attendance
        public IEnumerable<AttendanceSummary> GetAttendanceSummary(DateSearch model)
        {
            List<AttendanceSummary> lstsummary = new List<AttendanceSummary>().ToList();

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("date", model.fromdate);
                param.Add("COMPID", model.counid);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstsummary = _conn.Query<AttendanceSummary>("Sp_AttendaceSummary", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceSummary");

            }
            return lstsummary;
        }

        // function to get user attendance list
        public IEnumerable<AttendanceRemote> GetUserAttendanctList(AttendanceRemote model)
        {
            List<AttendanceRemote> lstattendance = new List<AttendanceRemote>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", model.EMPID);
                param.Add("MONTHNAME", Convert.ToInt32(model.MONTHNAME));
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<AttendanceRemote>("Sp_GetdataAttendance", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetUserAttendanctList");
            }
            return lstattendance;
        }

        // function to get Regularization approval list
        public List<EmpRegularApproval> GetRegulazationApprovalList(EmpRegularApproval model)
        {
            List<EmpRegularApproval> lstattendance = new List<EmpRegularApproval>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", model.EMPID);
                param.Add("FROM", model.fromdate);
                param.Add("TODATE", model.todate);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<EmpRegularApproval>("Sp_EmpRegularApproval", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetRegulazationApprovalList");
            }

            return lstattendance;
        }

        //Function to get country list
        public List<Countries> GetCountryList()
        {
            List<Countries> countries = new List<Countries>();
            try
            {
                _conn = GetConnection();
                OpenConnection(_conn);
                countries = _conn.Query<Countries>("Sp_Countrymst", commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetCountryList");
            }
            return countries;
            //List<Countries> empCoun = null;
            //try
            //{
            //    _conn = GetConnection();
            //    OpenConnection(_conn);
            //    empCoun = _conn.Query<Countries>("Sp_Countrymst", commandType: CommandType.StoredProcedure).ToList();

            //   CloseConnection(_conn);
            //}
            //catch (Exception ex)
            //{

            //    WriteErrorLog(ex, errorMethodRoute, "GetCountryList");
            //}
            //return empCoun;
        }

        //function to get approved attendance
        public int ApprovedAttendance(DataTable dt)
        {
            int result = 0;
            try
            {
                using (_conn = GetConnection())
                {
                    OpenConnection(_conn);
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@AttendanceApproval", dt.AsTableValuedParameter("dbo.AttendanceApprovalType"));
                    result = _conn.Execute("Sp_AppCoMaHeAttedanceNew", param, commandType: CommandType.StoredProcedure);
                    //result = Convert.ToInt32(strResult);
                }
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "ApprovedAttendance");
            }
            return result;
        }

        // function to get approve regularization
        public int ApprovedRegularization(DataTable dt)
        {
            int result = 0;
            try
            {
                using (_conn = GetConnection())
                {
                    OpenConnection(_conn);
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@RegularizationApproval", dt.AsTableValuedParameter("dbo.RegularizationApprovalType"));
                    result = _conn.Execute("Sp_AppCoMaHeRegularizationNew", param, commandType: CommandType.StoredProcedure);

                }
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "ApprovedRegularization");
            }
            return result;
        }

        // function to insert regularization 
        public int AttendanceRegularizationInsert(AttendanceRemote modal, DateTime AR_DATE2, string Username)
        {
            int result = 0;
            AttendanceRemote attendance = null;
            attendance = new AttendanceRemote();

            string? number = modal.AR_DATE1.Split(',')[0].Trim();
            number = number + " " + modal.OUTTIMETZ;

            try
            {
                
                DynamicParameters param = new DynamicParameters();
                param.Add("LOGINID", Username);
                param.Add("EMPID", modal.EMPID);
                param.Add("A_DATE", modal.AR_DATE1);
                //param.Add("ID", modal.ID);
               // param.Add("A_DATE", modal.AR_DATE1.Replace(",", ""));
                //param.Add("INTIMETZ", modal.INTIMETZ);
                //param.Add("OUTTIMETZ", modal.OUTTIMETZ);
                int InOutStatus = 1;
                if (modal.INTIMETZ != null)
                {
                    //int removecharters = modal.INTIME.Length;
                    //modal.INTIME= modal.AR_DATE1.Replace(",", "").Remove(11, 5).Insert(11, modal.INTIME);
                    //string newdate = modal.AR_DATE1.Split(new[] { ',' }, 8)[0].Trim() + " " + modal.INTIMETZ + modal.AR_DATE1.Substring(modal.AR_DATE1.Length - 0);
                    //string newdate = modal.AR_DATE1.Split(new[] { ',' }, 8)[0].Trim() + " " + modal.INTIMETZ;
                    string newdate = modal.AR_DATE1 + ' ' + modal.INTIMETZ;
                    modal.INTIMETZ = newdate;


                    param.Add("INTIMEDB", Convert.ToDateTime(modal.INTIMETZ));
                }
                else
                {
                    param.Add("InOutStatus", 0);
                    param.Add("INTIMEDB", "");
                }
                if (modal.OUTTIMETZ != null)
                {
                    //modal.OUTTIME= modal.AR_DATE1.Replace(",", "").Insert(11, modal.OUTTIME);
                    string newdate = modal.AR_DATE1.Split(new[] { ',' }, 8)[0].Trim() + " " + modal.OUTTIMETZ + modal.AR_DATE1.Substring(modal.AR_DATE1.Length - 0);
                    //string newdatee = modal.AR_DATE1.Split(new[] { ',' }, 8)[0].Trim() + " " + modal.OUTTIMETZ;
                    modal.OUTTIMETZ = newdate;
                    //modal.OUTTIME = newdate;
                    //param.Add("InOutStatus", 2);
                    InOutStatus = 1;
                    param.Add("OUTTIMEDB", Convert.ToDateTime(modal.OUTTIMETZ));

                }
                else
                {

                    param.Add("InOutStatus", 0);
                    param.Add("OUTTIMEDB", "");
                }

                param.Add("REMARKS", modal.REMARKS);
                param.Add("DEVICE", modal.DEVICE);
                param.Add("InOutStatus", InOutStatus);
                //string outtime = getcurrentdate.Remove(11, 5).Insert(11, modal.OUTTIME);
                //param.Add("OUTTIME", Convert.ToDateTime(outtime));
                //param.Add("REMARKS_OUT", modal.REMARKS);
                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("Sp_AttendanceRegularizationInsert", param, commandType: CommandType.StoredProcedure).ToString();

                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "AttendanceRegularizationInsert");
            }
            return result;
        }

        //function to get vendor list for attandance
        public List<VendorType> GetVendorList(string SearchValue, int countryid)
        {

            List<VendorType> vendorlist = new List<VendorType>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("CountryID", countryid);
                param.Add("parm", SearchValue);
                _conn = GetConnection();
                OpenConnection(_conn);
                vendorlist = _conn.Query<VendorType>("OMMS_GetVendorForAttendance", param, commandType: CommandType.StoredProcedure).ToList();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetVendorList");
            }
            return vendorlist;
        }

        //function to get vendor employee list for attandance
        public List<EmployeeVendor> GetEmployeeVendorList(string EMPID , string SearchValue)
        {
            List<EmployeeVendor> emplist = new List<EmployeeVendor>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EmpId", EMPID);
                param.Add("parm", SearchValue);
                _conn = GetConnection();
                OpenConnection(_conn);
                emplist = _conn.Query<EmployeeVendor>("sp_GetEmpListBasedOnVendorForAttendance", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetEmployeeVendorList");
            }
            return emplist;


        }

        //function to get vendor team attendace
        public List<VendorType> GetVendorTeamAttendance(VendorType model)
        {
            List<VendorType> lstTeamAttendance = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@loginid", model.LOGINID);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstTeamAttendance = _conn.Query<VendorType>("sp_GetEmployeeTeamAttendanceBassedOnVendorId", param, commandType: CommandType.StoredProcedure).ToList();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetVendorTeamAttendance");
            }
            return lstTeamAttendance;
        }

        //function to get vendor employee team attendace

        public List<VendorType> GetEmployeeTeamAttendance(VendorType model)
        {
            List<VendorType> lstTeamAttendance = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("loginid", model.LOGINID);
                param.Add("emptype", model.EMPTYPE);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstTeamAttendance = _conn.Query<VendorType>("sp_GetEmployeeTeamAttendanceBassedOnLoginId", param, commandType: CommandType.StoredProcedure).ToList();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetVendorTeamAttendance");
            }
            return lstTeamAttendance;
        }

        //function for get attendance history list
        public List<AttenHistoryVM> GetAttendanceHistoryList(AttendanceRemote model)
        {
            List<AttenHistoryVM> lstattendance = new List<AttenHistoryVM>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", model.EMPID);
                param.Add("Month", model.Month);
                param.Add("Year", model.Year);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<AttenHistoryVM>("Sp_GetAttendanceHistory_V2", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceHistoryList");
            }
            return lstattendance;
        }

        // function for get attendance approval list
        public List<EmpAttenApproval> GetAttendanceAppList(EmpAttenApproval model)
        {
            List<EmpAttenApproval> lstattendance = new List<EmpAttenApproval>().ToList();
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@EMPID", model.EMPID);
                param.Add("FROM", model.fromdate);
                param.Add("TODATE", model.todate);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<EmpAttenApproval>("Sp_EmpAttendanceApproval", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceAppList");
            }
            return lstattendance;
        }

        public AttenSummary GetAttensummry(AttenSummary attenSummary)
        {

            AttenSummary lstsummary = new AttenSummary();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", attenSummary.EmpId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstsummary = _conn.Query<AttenSummary>("c", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "EmployeeLeaveStatus");

            }
            return lstsummary;
        }

        //function to get attendance details of the day
        public List<AttendanceRemote> Attendancedetailsofday(string eMPID, string date)
        {
            List<AttendanceRemote> lstattendance = new List<AttendanceRemote>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EMPID", eMPID);
                param.Add("ARDATE", date);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<AttendanceRemote>("Attendancedetailsofday", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceAppList");
            }
            return lstattendance;
        }

        public List<AttenHistoryVM> Getintouttime(AttendanceRemote model)
        {
            List<AttenHistoryVM> lstattendance = new List<AttenHistoryVM>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", model.EMPID);
                param.Add("ARDATE", model.AR_DATE1);

                _conn = GetConnection();
                OpenConnection(_conn);
                lstattendance = _conn.Query<AttenHistoryVM>("Sp_GetIntouttime", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetAttendanceHistoryList");
            }
            return lstattendance;
        }

        // function to get Lat & Long
        public AttendanceRemote Getlatlong(AttendanceRemote modal)
        {
            
            AttendanceRemote attendance = null;
            attendance = new AttendanceRemote();
            try
            {
                DynamicParameters param = new DynamicParameters();
                //param.Add("LOGINID", modal.LOGINID);
                param.Add("usertype", modal.usertype);
                //param.Add("userSubType", modal.userSubType);
                param.Add("EMPID", modal.EMPID);
                
                _conn = GetConnection();
                OpenConnection(_conn);
                attendance = _conn.Query<AttendanceRemote>("Sp_GetOfficeLatlong", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetInOut");
            }
            return attendance;
        }
    }
}
