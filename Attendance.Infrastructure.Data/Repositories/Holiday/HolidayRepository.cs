using Attendance.Domain.Models.Holiday;
using Attendance.Infrastructure.Data;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Attendance.Domain.Interfaces.Holiday;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.Design;
using Attendance.Domain.Models.Company;
using Attendance.Domain.Models.Leave;
using static Dapper.SqlMapper;
using Attendance.Domain.Models.Infra;
using Attendance.Domain.Helper;
using Attendance.Domain;
using System.Security.Cryptography;

namespace Attendance.Infrastructure.Data.Repositories.Holiday
{
   
    public class HolidayRepository : BaseRepository,IHolidayRepository
    {
        private SqlConnection _conn;
        string errorMethodRoute = "";
        public HolidayRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Holidays> GetHolidayList()
        {

            List<Holidays> holidaylist = null;

            try
            {
                _conn = GetConnection();
                OpenConnection(_conn);
                holidaylist = _conn.Query<Holidays>("Sp_HolidaysCalender", commandType: CommandType.StoredProcedure).ToList();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                
                WriteErrorLog(ex, errorMethodRoute, "GetHolidayList");
            }
            return holidaylist;
        }

        // Save form holiday type //
        public int SaveHolidayType(HolidayTypeSave modal)
        {
            int result = 0;
            HolidayTypeSave attendance = null;
            attendance = new HolidayTypeSave();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", modal.Id);
                param.Add("Organizationid", modal.Organizationid);
                param.Add("OrganizationType", modal.OrganizationType);
                param.Add("HOLIDAY_DATE", Convert.ToDateTime(modal.HOLIDAY_DATE));
                param.Add("WEEKDAY", modal.WEEKDAY);
                param.Add("HOLIDAY_NAME", modal.HOLIDAY_NAME);
                param.Add("HOLIDAY_DESC", modal.HOLIDAY_DESC);
                param.Add("HC_START_DATE",modal.HC_START_DATE);
                param.Add("HC_VALID_DATE", modal.HC_VALID_DATE);
                param.Add("HC_STATUS", modal.HC_STATUS);
                param.Add("REMARKS", modal.REMARKS);
                param.Add("STATUS", modal.STATUS);
                param.Add("Createdby", modal.Createdby);
                param.Add("createdOn", modal.createdOn);
                param.Add("updatedby", modal.updatedby);
                param.Add("updatedon", modal.updatedon);          
                param.Add("CountryId", modal.CountryId= Convert.ToInt32(Security.GetDecryptString(modal.countryid)));
                param.Add("Company", modal.Company);
                param.Add("Country", modal.Country);
                param.Add("State", modal.State);
                param.Add("userid", modal.userid);
                param.Add("usertype", modal.usertype);
                param.Add("usersubtype", modal.usersubtype);


                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.ExecuteScalar("OMMS_SaveUpdateMstHolidayCalendarformnew", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);
            
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
               
                WriteErrorLog(ex, errorMethodRoute, "SaveInfraType");
            }
            return result;
        }

        // Get Holiday Name in Dropdown //
        public List<HoilidayType> GetHolidayDesc(int countryID, int userid, string usertype, string usersubtype)
        {
            List<HoilidayType> lstholitype = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("countryId", countryID);
                param.Add("userid", userid);
                param.Add("usertype", usertype);
                param.Add("usersubtype", usersubtype);
                _conn = GetConnection();
                OpenConnection(_conn);
                SqlConnection conn = this._conn;
                lstholitype = _conn.Query<HoilidayType>("Att_mstHolidaytypelist", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch(Exception ex)
            {
                this.WriteErrorLog(ex, this.errorMethodRoute);
            }
            return lstholitype;
        }

        // Get State List based on Country //
        public List<HolidayMaster> GetstatelistbasedonCountryId(int countryID, int userid, string usertype, string usersubtype) 
        {
            List<HolidayMaster> holidaystatelis = new List<HolidayMaster>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("CountryID", countryID);
                param.Add("userid", userid);
                param.Add("usertype", usertype);
                param.Add("usersubtype", usersubtype);
                //param.Add("CountryID", (object)countryID, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                _conn = GetConnection();
                OpenConnection(_conn);
                SqlConnection conn = this._conn;
                              
                holidaystatelis = conn.Query<HolidayMaster>("Sp_StatelistbasedonCountryId ", param, commandType:CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute);
            }
          return holidaystatelis;
        }

        // procedure dont have select query for GetHolidaymasterList//
        public List<HolidayTypeSave> GetHolidaymasterList(HolidayTypeSave modal, int userid, string usertype, string usersubtype)
        {
            List<HolidayTypeSave> lstholidaymaster = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("userid", userid);
                param.Add("usertype", usertype);
                param.Add("usersubtype", usersubtype);
                _conn = GetConnection();
                OpenConnection(_conn);

                lstholidaymaster = _conn.Query<HolidayTypeSave>("OMMS_SaveUpdateMstHolidayCalendar", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch
            {

            }
            return lstholidaymaster;
        }

        // Save holiday type with excel upload //
        public int SavecmDataFromExcel(HolidayTypeSave obj, string userType, string userSubType, int id, string returnFilePath , string jsondata , int Countryid)
        {
            int num = 0;
            // int result = 0;

            List<HolidayTypeSave> lstholidaytype = (List<HolidayTypeSave>)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();

                dynamicParameters1.Add("CountryId", (object)obj.CountryId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("@jsonData", (object)obj.DtExcelData, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
             
                this._conn = this.GetConnection();
                // DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                //lstholidaytype = _conn.Query("OMMS_SaveUpdateMstHolidayCalendar", dynamicParameters1, commandType: CommandType.StoredProcedure);  
                string strResult = SqlMapper.ExecuteScalar((IDbConnection)conn, "Att_SaveUpdateMstHolidayCalendarnewholiday", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                num = Convert.ToInt32(strResult);
                CloseConnection(this._conn);

            }
            catch (Exception ex)
            {
                
                WriteErrorLog(ex, this.errorMethodRoute, nameof(SavecmDataFromExcel));

            }

            return num;
        }

        public List<HolidayMaster> GetHolidayMasterDetails( int countryID)
        {
            List<HolidayMaster> holidaydetails = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@countryId", (object)countryID, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());

                //param.Add("ID", model.id);
                //param.Add("Organizationid", model.Organizationid);
                //param.Add("RecordId", model.RecordId);
                //param.Add("userid", model.userid);
                //param.Add("usertype", model.usertype);
                //param.Add("usersubtype", model.usersubtype);
                _conn = GetConnection();
                OpenConnection(_conn);

                holidaydetails = _conn.Query<HolidayMaster>("Att_GetHolidayDetails", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetEmpAttdenceInfraDetails");
            }
            return holidaydetails;
        }

        // method for upload Master holiday list of All Countries //
        public int SaveHolidayDataFromExcel(HolidayTypeSave obj, string userType, string userSubType, int id, string returnFilePath, string jsondata , int Countryid, string userid, string usertype, string usersubtype)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("userid",(object)obj.userid,new DbType?(), new ParameterDirection?(),new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("usertype", (object)obj.usertype, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("usersubtype", (object)obj.usersubtype, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("@countryId",(object)obj.CountryId,new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("@country", (object)obj.Country, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("@jsonData", (object)obj.DtExcelData, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());

                this._conn = this.GetConnection();
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string strResult = SqlMapper.ExecuteScalar((IDbConnection)conn, "Spo_Attmasterholidayupload", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                num = Convert.ToInt32(strResult);
                CloseConnection(this._conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, this.errorMethodRoute, nameof(SavecmDataFromExcel));

            }

            return num;

        }

        public List<HolidayTypemaster> BindHolidaytable(HolidayTypemaster model, int Countryid)
        {
            List<HolidayTypemaster> lstholidaylist = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                //param.Add("id", model.Id);
                param.Add("@countryId", (object)model.CountryId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());

                //param.Add("countryId", model.CountryId);
                //param.Add("country", model.Country);
                //param.Add("HOLIDAY_NAME", model.HOLIDAY_NAME);
                //param.Add("HOLIDAY_DESC", model.HOLIDAY_DESC);
               
                _conn = GetConnection();
                OpenConnection(this._conn);
                lstholidaylist = _conn.Query<HolidayTypemaster>("Getholidaylisttable", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(this._conn);

            }
            catch(Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "Getholidaylisttable");

            }
            return lstholidaylist;
        }

         public int DeleteBindHolidayTable(int recordId)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("HolidayRecordID", recordId);
                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.ExecuteScalar("deletegetholidaylist", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetHolidayList");
            }

            return result;
        }

        public List<HolidayTypeSave> EditBindHolidayTable(int recordId)
        {
            List<HolidayTypeSave> holidayTypeSaves = new List<HolidayTypeSave>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("HolidayRecordID", recordId);
                _conn = GetConnection();
                OpenConnection(_conn);
                holidayTypeSaves= _conn.Query<HolidayTypeSave>("updategetholidaylist",param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "updategetholidaylist");
            }
            return holidayTypeSaves;
        }

        public List<HoilidayType> GetHolidaydList(string holidayname, int userid, string usertype, string usersubtype)
        {
            List<HoilidayType> lstholidesc = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("HOLIDAY_NAME", holidayname);
                param.Add("userid", userid);
                param.Add("usertype", usertype);
                param.Add("usersubtype", usersubtype);
                _conn = GetConnection();
                OpenConnection(_conn);
                SqlConnection conn = this._conn;
                lstholidesc = _conn.Query<HoilidayType>("Att_mstHolidaydesclist", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                this.WriteErrorLog(ex, this.errorMethodRoute);
            }
            return lstholidesc;
        }

        public List<HolidayDetails> HolidayList()
        {
            List<HolidayDetails> lstholidesc = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                _conn = GetConnection();
                OpenConnection(_conn);
                SqlConnection conn = this._conn;
                lstholidesc = _conn.Query<HolidayDetails>("GetHolidayDetails", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                this.WriteErrorLog(ex, this.errorMethodRoute);
            }
            return lstholidesc;
        }
    }
}
