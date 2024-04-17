using Attendance.Domain.Interfaces.Leave;
using Attendance.Domain.Models.Leave;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data.Repositories.Leave
{
    public class LeaveRepository : BaseRepository, ILeaveRepository
    {

        private SqlConnection _conn;
        string errorMethodRoute = "";
        public LeaveRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int DeleteLeaveMasterDetails(int recordId)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("LeaveRecordID", recordId);
                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("[OMMS_DeleteLeaveMasterDetail]", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetHolidayList");
            }

            return result;
        }

        public List<LeaveMaster> EditLeaveMasterDetail(int recordId)
        {
            List<LeaveMaster> lstleaveMaster = new List<LeaveMaster>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("RecordId", recordId);
                //param.Add("Organizationid", 0);
                //param.Add("OrganizationType", "");
                _conn = GetConnection();
                OpenConnection(_conn);
                lstleaveMaster = _conn.Query<LeaveMaster>("OMMS_GetLeaveMasterDetails", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "EditLeaveMasterDetail");

            }
            return lstleaveMaster;
        }

        public LeaveStatus EmployeeLeaveStatus(LeaveStatus leaveStatus)
        {
            LeaveStatus? lstleave = new LeaveStatus();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMPID", leaveStatus.EMPID);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstleave = _conn.Query<LeaveStatus>("Sp_LeaveStatus", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetHolidayList");

            }
            return lstleave;
        }

        public List<Companylist> GetCompanyList(int countryId)
        {
            List<Companylist>? lstOhsDetails = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CountryId", countryId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstOhsDetails = _conn.Query<Companylist>("OMMS_GetCompanyForAttendanceByCountryID", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetCompanyList");
            }
            return lstOhsDetails;
        }

        public List<Departmentlist> GetDepartmentList(int company)
        {
            List<Departmentlist>? lstOhsDetails = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CompanyId", 0);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstOhsDetails = _conn.Query<Departmentlist>("OMMS_GetDepartmentForAttendance", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetDepartmentList");
            }
            return lstOhsDetails;
        }

        public List<Empployeelist> GetEmpList(int companyId, int departmentID)
        {
            List<Empployeelist>? lstOhsDetails = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CompanyId", companyId);
                param.Add("@DepartmentId", departmentID);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstOhsDetails = _conn.Query<Empployeelist>("OMMS_GetEmployeeListForLeaveReport", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetEmpList");
            }
            return lstOhsDetails;
        }

        public List<ManageLeaveRequest> GetEmployeeLeaveCount(ManageLeaveRequest model)
        {
            List<ManageLeaveRequest> lstleave = new List<ManageLeaveRequest>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("LeaveCode", model.LEAVE_CODE);
                param.Add("EmpId", model.EMP_ID);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstleave = _conn.Query<ManageLeaveRequest>("OMMS_GetLeaveCountBasedonLCode", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetEmployeeLeaveCount");

            }
            return lstleave;
        }

        public List<ManageLeaveRequest> GetLeaveApprovalList(ManageLeaveRequest model)
        {
            List<ManageLeaveRequest> lstSummary = new List<ManageLeaveRequest>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();  
                param.Add("EmpId", model.EMP_ID);
                param.Add("FROM", model.FROM);
                param.Add("TODATE", model.TO_DATE);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstSummary = _conn.Query<ManageLeaveRequest>("Sp_leaveApproval", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetLeaveApprovalList");

            }
            return lstSummary;
        }

        public List<LeaveMaster> GetLeaveMasterDetail(LeaveMaster model)
        {
            List<LeaveMaster> lstleaveMaster = new List<LeaveMaster>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("RecordId", model.RecordId);
                // param.Add("@Organizationid", 0);
                //param.Add("@OrganizationType", "");
                _conn = GetConnection();
                OpenConnection(_conn);
                lstleaveMaster = _conn.Query<LeaveMaster>("OMMS_GetLeaveMasterDetails", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetLeaveMasterDetail");

            }
            return lstleaveMaster;
        }

        public List<Leaves> GetLeaveNameList()
        {
            List<Leaves>? lstOhsDetails = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                _conn = GetConnection();
                OpenConnection(_conn);
                lstOhsDetails = _conn.Query<Leaves>("Sp_GetLeaveTypeName", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetLeaveMasterDetail");
            }
            return lstOhsDetails;
        }

        public List<LeaveReport> GetLeaveReportList(LeaveReport model)
        {
            List<LeaveReport> lstleaveReport = new List<LeaveReport>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Countryid", model.Countryid);
                //param.Add("Compid", compid);
                param.Add("Depid", model.Depid);
                param.Add("Empid", model.EMP_ID);
                param.Add("Fromdate", model.FROMDATE);
                param.Add("Todate", model.TODATE);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstleaveReport = _conn.Query<LeaveReport>("Sp_LeaveReport", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetLeaveReportList");

            }
            return lstleaveReport;
        }

        public List<ManageLeaveRequest> ManageLeaveRequest(int month, string eMPID, string year)
        {
            List<ManageLeaveRequest> lstleave = new List<ManageLeaveRequest>().ToList();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EmpId", eMPID);
                param.Add("month", month);
                param.Add("year", year);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstleave = _conn.Query<ManageLeaveRequest>("Sp_LeaveRecordStatus", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "ManageLeaveRequest");

            }
            return lstleave;
        }

        public int SaveLeaveApproved(DataTable dt)
        {
            int result = 0;
            try
            {

                using (_conn = GetConnection())
                {
                    OpenConnection(_conn);
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@LeaveApproval", dt.AsTableValuedParameter("dbo.LeaveApprovalType_V3"));
                    result = _conn.Execute("Sp_AppCoMaHeLeaveNew", param, commandType: CommandType.StoredProcedure);
                    //result = Convert.ToInt32(strResult);
                }
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "SaveLeaveApproved");
            }
            return result;
        }

        public int SaveLeaveMaster(LeaveMaster modal)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("RecordId", modal.RecordId);
                param.Add("UserID", modal.UserID);
                param.Add("Organizationid", modal.Organizationid);
                param.Add("OrganizationType", modal.OrganizationType);
                param.Add("LEAVE_CODE", modal.LEAVE_CODE);
                param.Add("LEAVE_COUNT", modal.LEAVE_COUNT);
                param.Add("LEAVE_DESC", modal.LEAVE_DESC);
                param.Add("LEAVE_YEAR", modal.LEAVE_YEAR);
                param.Add("Createdby", modal.Createdby);
                param.Add("STATUS", modal.STATUS);
                param.Add("MaxleavApply", modal.MaxleavApply);
                param.Add("LeaveCrediteType", modal.LeaveCrediteType);
                param.Add("LeaveCredite", modal.LeaveCredite);
                param.Add("updatedby", modal.updatedby);
                param.Add("Remarks", modal.Remarks);

                //param.Add("FromDate", Convert.ToDateTime(modal.FromDate));
                //param.Add("ToDate", Convert.ToDateTime(modal.ToDate));
                //param.Add("LeaveGroupname", modal.LeaveGroupType);


                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("OMMS_SaveUpdateLeaveMaster", param, commandType: CommandType.StoredProcedure).ToString();
                //already exist 103
                //something went wrong 102
                //successs 101
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "ManageLeaveRequest");
            }
            return result;
        }

        public int SaveLeaveRequest(Leaves modal)
        {
            int result = 0;
            LeaveMaster? attendance = null;
            attendance = new LeaveMaster();
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("EMP_ID", modal.EMP_ID);
                param.Add("LEAVE_FROM_DATE", modal.LEAVE_FROM_DATE);
                param.Add("LEAVE_TO_DATE", modal.LEAVE_TO_DATE);
                param.Add("LEAVE_COUNT", modal.LEAVE_COUNT);
                param.Add("LEAVE_CODE", modal.LEAVE_CODE);
                param.Add("LEAVE_MONTH", modal.LEAVE_MONTH);
                param.Add("LEAVE_YEAR", modal.LEAVE_YEAR);
                param.Add("REASONS", modal.REASONS);
                param.Add("CREATEDBY", modal.CREATEDBY);
                param.Add("UPDATEDBY", modal.UPDATEDBY);

                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("Sp_DtlEmpLeaveRecordsInsert", param, commandType: CommandType.StoredProcedure).ToString();
                //already exist 103
                //something went wrong 102
                //successs 101
                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "SaveLeaveRequest");
            }
            return result;
        }

    }
}
