using Attendance.Domain.Interfaces.Infra;
using Attendance.Domain.Models.Infra;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Attendance.Domain.Models.Holiday;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Newtonsoft.Json;

namespace Attendance.Infrastructure.Data.Repositories.Infra
{
    public class InfraRepository : BaseRepository, IInfraRepository
    {
        private SqlConnection _conn;
        string errorMethodRoute = "";
        public InfraRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<InfraType> GetInfraTypeList(string SearchValue)
        {
            List<InfraType> infratype = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Infraname", SearchValue);
                _conn = GetConnection();
                OpenConnection(_conn);

                infratype = _conn.Query<InfraType>("Sp_infraGroupGet", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "GetInfraTypeList");
            }
            return infratype;
        }

        public List<InfraTypeSave> GetEmpAttdenceInfraDetails(InfraType model)
        {
            List<InfraTypeSave> infratype = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EmpID", model.EmpID);
                //param.Add("IntraType", "Mobile");
                //param.Add("RecordId", model.RecordId);
                _conn = GetConnection();
                OpenConnection(_conn);

                infratype = _conn.Query<InfraTypeSave>("OMMS_GetEmpAttdenceInfraDetails", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetEmpAttdenceInfraDetails");
            }
            return infratype;
        }

        //** To get Infra Serial Number **//
        public List<SerialNumber> GetInfraStockSerialNumber(string SearchValue, int infraGrpid, int type)
        {

            List<SerialNumber> SerialNumberlst = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("InfraGrpId", infraGrpid);
                param.Add("p", SearchValue);
                param.Add("type", type);
                _conn = GetConnection();
                OpenConnection(_conn);
                SerialNumberlst = _conn.Query<SerialNumber>("Sp_InfraStockGet", param, commandType: CommandType.StoredProcedure).ToList();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetInfraStockSerialNumber");
            }
            return SerialNumberlst;
        }

        public int SaveInfraType(InfraTypeSave modal, string image)
        {
            int result = 0;
            InfraTypeSave attendance = null;
            attendance = new InfraTypeSave();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EMP_ID", modal.EMP_ID);
                param.Add("InfraGrpID", modal.INFRAGRP_ID);
                param.Add("MODEL_NUMBER", modal.MODEL_NUMBER);
                param.Add("SERIAL_NUMBER", modal.SERIAL_NUMBER);
                //param.Add("SERVICE_TAG_NUMBER", modal.SERVICE_TAG_NUMBER);
                param.Add("IMEI_NUMBER", modal.IMEI_NUMBER);
                param.Add("INFRA_STATUS", modal.INFRA_STATUS);
                param.Add("REMARKS", modal.REMARKS);
                // param.Add("CREATED_BY", modal.CREATED_BY);
                param.Add("INFRA_IMAGE", image);
                param.Add("RecordId", modal.RecordId);
                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.ExecuteScalar("OMMS_SaveUpdateEmpInfraDetails_v1", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "SaveInfraType");
            }
            return result;
        }

        public int DeleteInfraRecordDetails(string EmpID, int RecordId)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Empid", EmpID);
                param.Add("@InfraRecordID", RecordId);

                _conn = GetConnection();
                OpenConnection(_conn);
                string strResult = _conn.ExecuteScalar("OMMS_DeleteDailyInfraDetail", param, commandType: CommandType.StoredProcedure).ToString();
                CloseConnection(_conn);
                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {

                WriteErrorLog(ex, errorMethodRoute, "DeleteInfraRecordDetails");
            }

            return result;
        }

        public List<Getinfravalues> Getinfrastatus()
        {
            List<Getinfravalues> infra = null;
            try
            {
                _conn = GetConnection();
                OpenConnection(_conn);
                infra = _conn.Query<Getinfravalues>("Sp_GetInfraDetailsValues", commandType: CommandType.StoredProcedure).ToList();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "Getinfrastatus");
            }
            return infra;
        }

        public List<GetInfraEditdetails> EditBindInfraTable(int recordId)
        {
            List<GetInfraEditdetails> infraTypes = new List<GetInfraEditdetails>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Recordid", recordId);
                _conn = GetConnection();
                OpenConnection(_conn);
                infraTypes = _conn.Query<GetInfraEditdetails>("GetInfradetialsForedit", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "EditBindInfraTable");
            }
            return infraTypes;
        }

        public List<GetImageInfra> GetInfraImage(int recordId)
        {
            List<GetImageInfra> infraTypes = new List<GetImageInfra>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("ID", recordId);
                _conn = GetConnection();
                OpenConnection(_conn);
                infraTypes = _conn.Query<GetImageInfra>("OMMS_GetImageForInfra", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetInfraImage");
            }
            return infraTypes;
        }

        public List<GetInfraIssue> GetInfraIssue(string empID)
        {
            List<GetInfraIssue> infraTypes = new List<GetInfraIssue>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EmpID", empID);
                _conn = GetConnection();
                OpenConnection(_conn);
                infraTypes = _conn.Query<GetInfraIssue>("GetAttedanceInfraIssue", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetInfraIssue");
            }
            return infraTypes;
        }

        //public int SaveUploadExcelItems(DataTable excelData)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("@jsonData", excelData);
        //        _conn = GetConnection();
        //        OpenConnection(_conn);
        //        string strResult = _conn.ExecuteScalar("Att_SaveInfraExelDetails", param, commandType: CommandType.StoredProcedure).ToString();
        //        result = Convert.ToInt32(strResult);
        //        CloseConnection(_conn);

        //    }
        //    catch (Exception ex)
        //    {

        //        WriteErrorLog(ex, errorMethodRoute, "SaveInfraType");
        //    }
        //    return result;
        //}

        public int SaveUploadExcelItems(DataTable excelData)
        {
            int result = 0;
            try
            {
                // Convert DataTable to JSON string
                string jsonData = ConvertDataTableToJson(excelData);

                DynamicParameters param = new DynamicParameters();
                param.Add("@jsonData", jsonData);

                _conn = GetConnection();
                OpenConnection(_conn);

                // Execute stored procedure
                string strResult = _conn.ExecuteScalar("Att_SaveInfraExelDetails", param, commandType: CommandType.StoredProcedure).ToString();
                result = Convert.ToInt32(strResult);

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                WriteErrorLog(ex, errorMethodRoute, "SaveInfraType");
            }
            return result;
        }

        private string ConvertDataTableToJson(DataTable dataTable)
        {
            string jsonString = string.Empty;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                jsonString = JsonConvert.SerializeObject(dataTable);
            }
            return jsonString;
        }

        public List<GetPreviousDate> GetInfraPreviousDate(string empID)
        {
            List<GetPreviousDate> infradate = new List<GetPreviousDate>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Emp_ID", empID);
                _conn = GetConnection();
                OpenConnection(_conn);
                infradate = _conn.Query<GetPreviousDate>("Get_PreviousDateForInfra", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, "GetInfraPreviousDate");
            }
            return infradate;
        }
    }
}
