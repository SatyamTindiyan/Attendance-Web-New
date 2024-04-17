using Attendance.Domain.Interfaces.Reimbursement;
using Attendance.Domain.Models.Reimbursement;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Attendance.Infrastructure.Data.Repositories.Reimbursement
{
    public class ReimbursementRepository : BaseRepository, IReimbursementRepository
    {
        private SqlConnection _conn;
        string errorMethodRoute = "";
        public ReimbursementRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<ClaimStatus> GetClaimStatus(int id)
        {
            List<ClaimStatus> lstGetClaimStatus = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                //param.Add("year", year);
                //param.Add("EmpId", EmpId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetClaimStatus = _conn.Query<ClaimStatus>("sp_GetClaimStatus", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetClaimStatus");
            }
            return lstGetClaimStatus;
        }

        public List<ClaimType> GetClaimType(int id)
        {
            List<ClaimType> lstGetClaimType = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                //param.Add("year", year);
                //param.Add("EmpId", EmpId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetClaimType = _conn.Query<ClaimType>("sp_GetClaimType", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetClaimType");
            }
            return lstGetClaimType;
        }

        //* Fetch Data in reimbursement approval & Get Reimbursement*//
        public List<ReimbursementNew> GetReimbursementDtl(int month, int year, string empId)
        {
            List<ReimbursementNew> lstGetReimbursementDtl = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Month", month);
                param.Add("year", year);
                param.Add("EmpId", empId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetReimbursementDtl = _conn.Query<ReimbursementNew>("sp_FetchcliamData", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetReimbursementDtl");
            }
            return lstGetReimbursementDtl;
        }

        public List<SubClaimType> GetSubClaimType(int id, int claimType)
        {
            List<SubClaimType> lstGetClaimType = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                //param.Add("ClaimType", claimType);
                //param.Add("year", year);
                //param.Add("EmpId", EmpId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetClaimType = _conn.Query<SubClaimType>("sp_GetSubClaimType", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetSubClaimType");
            }
            return lstGetClaimType;
        }

        public int SaveHeadReimbursementApproval(int approvalId, int status)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", approvalId);
                param.Add("@Status", status);
                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.ExecuteScalar("sp_SaveHeadApprovalStatus", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "SaveHeadReimbursementApproval");
            }
            return result;
        }

        public int SaveHeadReimbursementApprovalDtl(string ClaimDetail, int Month, int year, string EmpId, string ARDATE)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters(); 
                param.Add("@EmpId", EmpId);
                param.Add("@year", year);
                param.Add("@Month", Month);
                param.Add("@ARDATE", 1-4-2024);
                param.Add("@jsonData", (object)ClaimDetail, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                //param.Add("@ReimbursementApproval ", dt.AsTableValuedParameter("dbo.ReimbursementApprovalType"));
                _conn = GetConnection();
                OpenConnection(_conn);
                string? strResult = _conn.ExecuteScalar("sp_SaveHeadApprovalStatusNew", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "SaveHeadReimbursementApprovalDtl");
            }
            return result;
        }
        public int SaveReimbursement(string ClaimDetail, int Month, int year, string EmpId)
        {
            int result = 0;

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Month", Month);
                param.Add("@year", year);
                param.Add("@EmpId", EmpId);
                //param.Add("EmpName", reimbursement.EmpName);
                param.Add("@PrevOutstadAmt", "1");
                param.Add("@onOutstanding", "2");
                param.Add("@ReportingManager", "1");
                param.Add("@ProjectHeadID", "1");
                param.Add("@Finance_Approval", "1");
                param.Add("@jsonData", (object)ClaimDetail, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());

                _conn = GetConnection();
                OpenConnection(_conn);
                string? StrResult = _conn.ExecuteScalar("sp_InsClaimReimbursementV1", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(StrResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "SaveReimbursement");
            }
            return result;

        }

        public int SaveReimbursementApproval(string claimDetail, int month, int year, string empId, List<int> claimId)
        {
            int result = 0;
            string StrResult = null;
            try
            {
                _conn = GetConnection();
                OpenConnection(_conn);
                foreach (int claimid in claimId)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ClaimId",claimid);
                    parameters.Add("@EmpId", empId);
                    parameters.Add("@jsonData", (object)claimDetail, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());

                   StrResult= _conn.Execute("updateReimbursementApproval", parameters, commandType: CommandType.StoredProcedure).ToString();
                }

               // string? StrResult = _conn.ExecuteScalar("updateReimbursementApproval", parameters, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(StrResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "updateReimbursementApproval");
            }
            return result;
        }

        public int SaveReimbursementMobile(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Month", month);
                param.Add("@year", year);
                param.Add("@EmpId", empId);
                param.Add("@EmpName", empName);
                param.Add("@ClaimType", claimType);
                param.Add("@ClaimTypeSub", clamSubType);
                param.Add("@ClaimDate", claimDatee);
                param.Add("@ClaimAmt", claimAmount);
                param.Add("@KMIN", kmIN);
                param.Add("@KMOUT", kmOUT);
                param.Add("@REMARKS", remarks);
                param.Add("@ClaimStatus", claimStatus);
                param.Add("@Images", fileName);
                param.Add("@PrevOutstadAmt", "1");
                param.Add("@onOutstanding", "2");
                _conn = GetConnection();
                OpenConnection(_conn);
                string? StrResult = _conn.ExecuteScalar("sp_InsClaimMobile", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(StrResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "SaveReimbursementMobile");
            }
            return result;
        }

        //Approval status update and Manager and RM approval //

        public int InsertReimbursementClaim(int employeeId, decimal amount, string billImagePath)
        {
            _conn = GetConnection();
            OpenConnection(_conn);

            string insertQuery = "INSERT INTO ReimbursementClaims (EmployeeId, Amount, BillImagePath, Status, ManagerApprovalStatus, RegionalManagerApprovalStatus, AccountPersonApprovalStatus) VALUES (@EmployeeId, @Amount, @BillImagePath, 'Pending', 'Pending', 'Pending', 'Pending'); SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(insertQuery, _conn))
            {
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@BillImagePath", billImagePath);


                return Convert.ToInt32(command.ExecuteScalar());
            }

        }

        public void UpdateClaimStatus(int claimId, string approvalStatus, string status)
        {
            _conn = GetConnection();
            OpenConnection(_conn);
            string updateQuery = "";

            switch (approvalStatus.ToLower())
            {
                case "manager":
                    updateQuery = "UPDATE ReimbursementClaims SET ManagerApprovalStatus = 'Approved', RegionalManagerApprovalStatus = 'Pending', AccountPersonApprovalStatus = 'Pending' WHERE ClaimId = @ClaimId";
                    break;
                case "regionalmanager":
                    updateQuery = "UPDATE ReimbursementClaims SET RegionalManagerApprovalStatus = 'Approved', AccountPersonApprovalStatus = 'Pending' WHERE ClaimId = @ClaimId";
                    break;
                case "accountperson":
                    updateQuery = "UPDATE ReimbursementClaims SET AccountPersonApprovalStatus = 'Approved' WHERE ClaimId = @ClaimId";
                    break;
                default:
                    throw new ArgumentException("Invalid approval status.");
            }

            using (SqlCommand command = new SqlCommand(updateQuery, _conn))
            {
                command.Parameters.AddWithValue("@ClaimId", claimId);

                command.ExecuteNonQuery();
            }


        }

        public List<ClaimStatusViewModel> GetReimbursement(int month, int year, string empId)
        {
            List<ClaimStatusViewModel> lstGetReimbursementDtl = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Month", month);
                param.Add("year", year);
                param.Add("EmpId", empId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetReimbursementDtl = _conn.Query<ClaimStatusViewModel>("sp_FetchcliamData", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetReimbursementDtl");
            }
            return lstGetReimbursementDtl;
        }

        public List<ReimbursementNew> GetPaidReimbursement(int month, int year, string empId)
        {
            List<ReimbursementNew> lstGetReimbursementDtl = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Month", month);
                param.Add("year", year);
                param.Add("EmpId", empId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetReimbursementDtl = _conn.Query<ReimbursementNew>("sp_FetchPaidData", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetReimbursementDtl");
            }
            return lstGetReimbursementDtl;
        }

        public List<ReimbursementNew> GetAcReimbursement(int month, int year, string? empId, int userId, string? userType, string? userSubType)
        {
            List<ReimbursementNew> lstGetacReimbursementDtl = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Month", month);
                param.Add("year", year);
                param.Add("EmpId", empId);
                param.Add("@UserId", userId);
                param.Add("@UserType", userType);
                param.Add("@UserSubtype",userSubType);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstGetacReimbursementDtl = _conn.Query<ReimbursementNew>("sp_FetchcliamReimbursement", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetReimbursementDtl");
            }
            return lstGetacReimbursementDtl;


        }

        public int SaveacReimbursementApproval(string claimDetail, int month, int year, string empId, List<int> claimId)
        {
            int result = 0;
            string StrResult = null;
            try
            {
                _conn = GetConnection();
                OpenConnection(_conn);
                foreach (int claimid in claimId)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ClaimId", claimid);
                    parameters.Add("@EmpId", empId);
                    parameters.Add("@jsonData", (object)claimDetail, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());

                    StrResult = _conn.Execute("updateAcReimbursementApproval", parameters, commandType: CommandType.StoredProcedure).ToString();
                }
                CloseConnection(_conn);
                result = Convert.ToInt32(StrResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "updateAcReimbursementApproval");
            }
            return result;
        }
    }

}

