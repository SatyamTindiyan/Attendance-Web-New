using Attendance.Domain.Interfaces.EngineeringTeamMapping;
using Attendance.Domain.Models.EngineeringTeamMapping;
using Dapper;
using Microsoft.Extensions.Configuration;
using Attendance.Domain.Models.Company;
using Attendance.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data.Repositories.EngineeringTeamMapping
{
    public class EngineeringTeamMappingRepository : BaseRepository, IEngineeringTeamMappingRepository
    {
        private SqlConnection _conn;
        string errorMethodRoute = "";
        public EngineeringTeamMappingRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public List<EmployeeApproveUnapprove> GetEmpRiggerTechnicianList(int VendorID, int ProjectId, int CompanyID, string EmpId, string UserType, string UserSubType)
        {
            List<EmployeeApproveUnapprove> lstOhsDetails = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CompanyId", CompanyID);
                param.Add("@VendorId", VendorID);
                param.Add("@ProjectId", ProjectId);
                param.Add("@EmpId", EmpId);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstOhsDetails = _conn.Query<EmployeeApproveUnapprove>("OMMS_GetEmpRiggerTechnicianList", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                
                WriteErrorLog(ex, errorMethodRoute, "GetEmpRiggerTechnicianList");
            }
            return lstOhsDetails;

        }
        public int SaveRecord(string objproj,int status,int userId,int companyId)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("status", status);
                param.Add("UserId", userId);
                param.Add("Jsondata", objproj);

                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.ExecuteScalar("OMMS_SaveUpdateDtlEngineerTeamMapping", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {
                
                WriteErrorLog(ex, errorMethodRoute, "SaveRecord");
            }
            return result;
        }

        public List<CompanyDetail> GetCompanyList(int CompanyId,string UserType,string UserSubType,int UserId)
        {
            List<CompanyDetail> companyList = (List<CompanyDetail>)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add(nameof(CompanyId), (object)CompanyId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserType), (object)UserType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserSubType), (object)UserSubType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("empid", (object)UserId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
               this. _conn = this.GetConnection();
                OpenConnection(_conn);
                SqlConnection conn = this. _conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                companyList = SqlMapper.Query<CompanyDetail>((IDbConnection)conn, "OMMS_GetCompanyDetails", (object)dynamicParameters2, (IDbTransaction)null, true, nullable2, nullable3).ToList<CompanyDetail>();
                CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
               
                this.WriteErrorLog(ex, this.errorMethodRoute, "GetCompanyDetail");
            }
            return companyList;
        }

        public List<VendorMaster> GetVendorBasedOnCompanyIduser(int companyid,int userid,string UserType,string UserSubType,int empid)
        {
            List<VendorMaster> basedOnCompanyIduser = (List<VendorMaster>)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add(nameof(companyid), companyid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserType),UserType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserSubType),UserSubType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(userid), userid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                _conn = GetConnection();
                OpenConnection(_conn);
                SqlConnection conn = _conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                basedOnCompanyIduser = SqlMapper.Query<VendorMaster>(conn, "OMMS_GetVendorListBasedOnCompanyId", dynamicParameters2, null, true, nullable2, nullable3).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                
                this.WriteErrorLog(ex, errorMethodRoute, "OMMS_GetVendorListBasedOnCompanyId");
            }
            return basedOnCompanyIduser;
        }
        public List<ProjectMasterOMMS> GetProjectListBasedOnVendor(int vendorId, string UserType, string UserSubType, int UserId)
        {
            List<ProjectMasterOMMS> listBasedOnVendor = (List<ProjectMasterOMMS>)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("VendorId", (object)vendorId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserType), (object)UserType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserSubType), (object)UserSubType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserId), (object)UserId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.GetConnection();
                OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                listBasedOnVendor = SqlMapper.Query<ProjectMasterOMMS>((IDbConnection)conn, "OMMS_GetOmmsProjectListBasedOnVendor", (object)dynamicParameters2, (IDbTransaction)null, true, nullable2, nullable3).ToList<ProjectMasterOMMS>();
                CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                
                this.WriteErrorLog(ex, this.errorMethodRoute, nameof(GetProjectListBasedOnVendor));
            }
            return listBasedOnVendor;
        }
        public List<EmployeeJobDetails> GetEmployeeProjectbase(int ProjectID, int userid, string UserType, string UserSubType)
        {
            List<EmployeeJobDetails> lstDetail = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("ProjectID", ProjectID);
                //param.Add("userid", userid);
                //param.Add("UserType", UserType);
                //param.Add("UserSubType", UserSubType);
                _conn = GetConnection();
                OpenConnection(_conn);
                lstDetail = _conn.Query<EmployeeJobDetails>("OMMS_GetEmployeeBasedonProject", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                
                WriteErrorLog(ex, errorMethodRoute, "GetEmployeeProjectbase");
            }
            return lstDetail;
        }


    }
}
