using Attendance.Domain;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.Holiday;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using Attendance.Infrastructure.loc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using FormCollection = Microsoft.AspNetCore.Http.FormCollection;
using ExcelDataReader;
using System.Data.SqlClient;
using System.Data.OleDb;
using Attendance.ApplicationCore.Interfaces.Holiday;
using System;
using Attendance.Domain.Models.Infra;
using System.Linq;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using System.Diagnostics.Metrics;

namespace Attendance.Controllers.Holiday
{
    
    public class HolidayController : BaseController
    {

        public HolidayController(IServiceProvider serviceProvider, IConfiguration configuration, IWebHostEnvironment environment) : base(serviceProvider, configuration)
        {



        }

        public IActionResult Holiday()
        {
            var result = _holiday.GetHolidayList().ToList();
            return View(result);
        }
        public IActionResult HolidayMaster()
        {
            return View();
        }

        //*Save holiday type list*//
        public string SaveHolidayType(HolidayTypeSave modal)
        {
            var value = GetUserDetail();
            modal.EMPID = MySession.EmpId;
            modal.OrganizationType = value.UserType;
            modal.Organizationid = value.Id;
            modal.userid =value.fkUserId;
            modal.usertype = value.UserType;
            modal.usersubtype = value.UserSubType;

            string strReturn = "";
            var result = _holiday.SaveHolidayType(modal);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            //modal.createdOn = Convert.ToDateTime(modal.createdOn).ToString("dd/MM/yyyy");
            //modal.updatedon = Convert.ToDateTime(modal.updatedon).ToString("dd/MM/yyyy");
            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return strReturn;

        }

        // *Get Country list*//
        public JsonResult GetCountry()
        {
            List<Countries> lstCoun = new List<Countries>();

            lstCoun = _attendanceService.GetCountryList();
            return Json(new { lstCoun = lstCoun });
            //if (lstCoun != null)
            //    return Json(new {data = lstCoun});
            //else
            //    return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        // *Get Holiday Description list*//
        [HttpGet]
        public JsonResult HolidayDesclist(string country)
        {
            List<HoilidayType> lstholidaytype = new List<HoilidayType>();
            var value = GetUserDetail();
            int userid = value.Id;
            string usertype = value.UserType;
            string usersubtype = value.UserSubType;

            int countryID = !string.IsNullOrEmpty(country) ? Convert.ToInt32(Security.GetDecryptString(country)) : 0;
            //HoilidayType HM = new HoilidayType();
            //string Holidaytype = HM.Holidaytype;
            lstholidaytype = _holiday.GetHolidayDesc(countryID, userid, usertype, usersubtype);


            return Json(new { lstholidaytype = lstholidaytype });
        }

        [HttpGet]
        public JsonResult HolidayDList(string holidayname)
        {
            List<HoilidayType> lstholidaydesc = new List<HoilidayType>();
            var value = GetUserDetail();
            int userid = value.Id;
            string usertype = value.UserType;
            string usersubtype = value.UserSubType;
             
            
            //int holidayid = !string.IsNullOrEmpty(holidayname) ? Convert.ToInt32(Security.GetDecryptString(holidayname)) : 0;
            lstholidaydesc = _holiday.GetHolidaydList(holidayname, userid, usertype, usersubtype);

            return Json(new { lstholidaydesc = lstholidaydesc });
        }


        // *Get State list  based on Country*//
        [HttpGet]
        public JsonResult GetStateListBasedOnCompanyId(string country)
        {
            List<HolidayMaster> statelist = new List<HolidayMaster>();
            var value = GetUserDetail();
            int userid = value.Id;
            string usertype = value.UserType;
            string usersubtype = value.UserSubType;
            //int countryID = value.CountryId;
            //int stateid = MySession.stateId;
            int countryID = !string.IsNullOrEmpty(country) ? Convert.ToInt32(Security.GetDecryptString(country)) : 0;
            statelist = _holiday.GetstatelistbasedonCountryId(countryID, userid, usertype, usersubtype);
            return Json(new { statelist = statelist });

        }

        // *Get Holiday Master List*//
        [HttpPost]
        public JsonResult GetHolidayMastList(HolidayTypeSave modal)
        {
            int id = 0;
            var value = GetUserDetail();
            int userid = value.Id;
            string usertype = value.UserType;
            string userSubType = value.UserSubType;

            modal.EMPID = MySession.EmpId;
            modal.RecordId = id;
            List<HolidayTypeSave> listholiday = new List<HolidayTypeSave>();
            listholiday = _holiday.GetHolidaymasterList(modal ,userid,usertype,userSubType);
            if (listholiday != null)
                return Json(new { listholiday });
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);

        }

        // *Update Holiday Master Through Excel Upload*//
        [HttpPost]
        public string SaveSiteUploadFromExcel(IFormCollection formCollection)
        {
            HolidayTypeSave Obj = null;
            int result = (int)StatusMessageCode.StatusCode.Error;
            string strReturn = "Some error occured.";
            Boolean SuccesStatus = false;
            if (formCollection != null)
            {

                var country = formCollection["Country"];
                var countryid = formCollection["Encryptedcountryid"];
                var HolidayName = formCollection["holidayname"];
                int Countryid = Convert.ToInt32(!string.IsNullOrEmpty(countryid) ? Convert.ToInt32(Security.GetDecryptString(countryid)) : 0);
                //var stateid = formCollection["Encrpytedstate"];
                //int Stateid = Convert.ToInt32(!string.IsNullOrEmpty(stateid) ? Convert.ToInt32(Security.GetDecryptString(stateid)) : 0);
                //var holidaytypeid = formCollection["Encrytedholidaytype"];
                Obj = new HolidayTypeSave();
                DataTable dt = new DataTable();
                string jsondata = string.Empty;
                //int result = (int)StatusMessageCode.StatusCode.Error;

                if (Request.Form.Files["Excelfile"] != null)
                {
                    try
                    {
                        string[] validFileTypes = { ".xls", ".xlsx"};
                        string extension = System.IO.Path.GetExtension(Request.Form.Files["Excelfile"].FileName);
                        var FileName = Request.Form.Files["Excelfile"];
                        string Rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Excelfile");

                        //create folder if not exist
                        if (!Directory.Exists(Rootpath))
                            Directory.CreateDirectory(Rootpath);

                        //get file extension
                        FileInfo fileInfo = new FileInfo(FileName.Name);
                        string fileName = FileName + fileInfo.Extension;
                        string RootpathSaveindatabase = DateTime.Now.Ticks + FileName.Name;
                        string RootpathFiles = Path.Combine(Directory.GetCurrentDirectory(), Rootpath + "/" + RootpathSaveindatabase);
                        if (!Directory.Exists(RootpathFiles))
                            Directory.CreateDirectory(RootpathFiles);

                        string fileNameWithPath = Path.Combine(RootpathFiles, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            FileName.CopyTo(stream);
                            SuccesStatus = true;
                        }
                        int countOtherHoliday = 0;

                        if (validFileTypes.Contains(extension))
                        {

                            DataTable dt2 = new DataTable();
                            if (extension.Trim() == ".xls" || extension.Trim() == ".xlsx")
                            {


                                using (var client = new WebClient())
                                {
                                    var file = client.DownloadData(fileNameWithPath);

                                    var excelContent = ParseExcel(new MemoryStream(file));
                                    jsondata = JsonConvert.SerializeObject(excelContent);
                                    //DataTable dt = new DataTable();

                                    var list1 = JsonConvert.DeserializeObject<List<Holidaymasterexcel>>(jsondata);
                                    var lstCountry = list1.Where(x => x.Acountry != country).ToList();
                                    if (lstCountry != null)
                                    {
                                        if (lstCountry.Any())
                                        {
                                            countOtherHoliday = lstCountry.Count();
                                        }
                                    }

                                }
                                if (countOtherHoliday == 0)
                                {
                                    if (jsondata != null)
                                    {
                                        Obj.CountryId = Countryid;
                                        //Obj.stateId = Stateid;
                                        //Obj.HOLIDAY_NAME = holidaytypeid;
                                        Obj.DtExcelData = jsondata;
                                        string returnFilePath = "";
                                        //result = 1; _service.SavecmDataFromExcel(Obj, ref returnFilePath);
                                        result = _holiday.SavecmDataFromExcel(Obj, returnFilePath, jsondata, Countryid);
                                        if (result == 102)
                                        {
                                            if (!string.IsNullOrEmpty(returnFilePath))
                                                strReturn = " <u><a href='../Download" + returnFilePath + "'>Download your error list file.</a></u>";
                                        }

                                        else if (result == 0)
                                        {

                                            strReturn = "Please Upload Files in Excel in Required format only   ";

                                        }

                                        else
                                        {
                                            //TempData["successMessage"] = "your Site upload Data successfully saved.";

                                            strReturn = strReturn = "success";

                                        }
                                    }
                                }
                                else
                                {
                                    strReturn = "Please select country according to excel record";
                                }
                            }

                            else
                            {
                                strReturn = "Please Upload Files in .xlsx format only";
                            }

                        }
                    }


                    catch (Exception ex)
                    {

                    }

                }
            }


            return strReturn; //Json(ObjSiteUploadModel, JsonRequestBehavior.AllowGet);
        }
        public static IEnumerable<Dictionary<string, object>> MapDatasetData(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                yield return row;
            }
        }
        public static IEnumerable<Dictionary<string, object>> ParseExcel(Stream document)
        {
            List<char> charsToRemove = new List<char>() { '@', ':', '-', '%', '[', ']' };
            using (var reader = ExcelReaderFactory.CreateReader(document))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = true,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }

                });
                foreach (DataColumn column in result.Tables.Cast<DataTable>().First().Columns)
                {
                    foreach (char c in charsToRemove)
                    {
                        column.ColumnName = column.ColumnName.Replace(c.ToString(), String.Empty);
                    }
                    column.ColumnName = 'A' + (String.Join("", column.ColumnName.Split()));
                }
                return MapDatasetData(result.Tables.Cast<DataTable>().First());
            }
        }
        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public DataTable ExcelToDataTableUsingExcelDataReader(string storePath)
        {
            FileStream stream = System.IO.File.Open(storePath, FileMode.Open, FileAccess.Read);

            string fileExtension = Path.GetExtension(storePath);
            IExcelDataReader excelReader = null;
            if (fileExtension == ".xls")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (fileExtension == ".xlsx")
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }


            DataSet result = excelReader.AsDataSet();
            var test = result.Tables[0];
            return result.Tables[0];
        }
        /* Get HolidAY Master table */
        public JsonResult GetHolidayMasterDetails( string countryId)
        {
            var value = GetUserDetail();
            if(value != null)
            {
                int userid = value.Id;
                string usertype = value.UserType;
                string usersubtype = value.UserSubType;

            }
            //int ID = 1;
            //model.Organizationid = value.Id;
            ////model.id = ID;
            //model.RecordId = model.Organizationid;
            List<HolidayMaster> lstholidaydetails = new List<HolidayMaster>();
            int countryID = !string.IsNullOrEmpty(countryId) ? Convert.ToInt32(Security.GetDecryptString(countryId)) : 0;
            lstholidaydetails = _holiday.GetHolidayMasterDetails( countryID);
            return Json(new { lstholidaydetails = lstholidaydetails });
        }


        // *Update Holiday Master list of all country Through Excel Upload*//
        public IActionResult MasterHoliday()
        {

            return View();
        }
        [HttpPost]
        public string HolidayUploadFromExcel(IFormCollection formCollection)
        {
            HolidayTypeSave Obj = null;
            int result = (int)StatusMessageCode.StatusCode.Error;
            string strReturn = "Some error occured.";
            Boolean SuccesStatus = false;

            if (formCollection != null)
            {

                var country = formCollection["Country"];
                var countryid = formCollection["Encryptedcountryid"];
                //string value = GetCountry(countryid)
                int Countryid = Convert.ToInt32(!string.IsNullOrEmpty(countryid) ? Convert.ToInt32(Security.GetDecryptString(countryid)) : 0);
                string userid = null;
                string usertype = null;
                string usersubtype = null;
                var value = GetUserDetail();
                if (value != null)
                {
                    userid = value.UserName;
                    usertype = value.UserType;
                    usersubtype = value.UserSubType;
                }


                Obj = new HolidayTypeSave();
                DataTable dt = new DataTable();
                string jsondata = string.Empty;
                //int result = (int)StatusMessageCode.StatusCode.Error;

                if (Request.Form.Files["Excelfile"] != null)
                {
                    try
                    {
                        string[] validFileTypes = { ".xls", ".xlsx"};
                        string extension = System.IO.Path.GetExtension(Request.Form.Files["Excelfile"].FileName);
                        var FileName = Request.Form.Files["Excelfile"];
                        string Rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Excelfile");

                        //**create folder if not exist**//
                        if (!Directory.Exists(Rootpath))
                            Directory.CreateDirectory(Rootpath);

                        //**get file extension**//
                        FileInfo fileInfo = new FileInfo(FileName.Name);
                        string fileName = FileName + fileInfo.Extension;
                        string RootpathSaveindatabase = DateTime.Now.Ticks + FileName.Name;
                        string RootpathFiles = Path.Combine(Directory.GetCurrentDirectory(), Rootpath + "/" + RootpathSaveindatabase);
                        if (!Directory.Exists(RootpathFiles))
                            Directory.CreateDirectory(RootpathFiles);

                        string fileNameWithPath = Path.Combine(RootpathFiles, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            FileName.CopyTo(stream);
                            SuccesStatus = true;
                        }
                        int countOtherCountry = 0;
                        if (validFileTypes.Contains(extension))
                        {

                            DataTable dt2 = new DataTable();
                            if (extension.Trim() == ".xls" || extension.Trim() == ".xlsx")
                            {


                                using (var client = new WebClient())
                                {
                                    var file = client.DownloadData(fileNameWithPath);

                                    var excelContent = HolidayParseExcel(new MemoryStream(file));

                                    // excelContent.ToList().ForEach(x => dt2.Rows.Add(x));

                                    jsondata = JsonConvert.SerializeObject(excelContent);

                                    var list1 = JsonConvert.DeserializeObject<List<HolidayExcel>>(jsondata);
                                    var lstCountry = list1.Where(x => x.Acountry != country).ToList();
                                    if (lstCountry != null)
                                    {
                                        if (lstCountry.Any())
                                        {
                                            countOtherCountry = lstCountry.Count();
                                        }
                                    }

                                }
                                if (countOtherCountry == 0)
                                {
                                    if (jsondata != null)
                                    {

                                        Obj.CountryId = Countryid;
                                        Obj.userid = userid;
                                        Obj.usertype = usertype;
                                        Obj.usersubtype = usersubtype;
                                        Obj.DtExcelData = jsondata;
                                        string returnFilePath = "";

                                        result = _holiday.SaveHolidayDataFromExcel(Obj, returnFilePath, jsondata, Countryid, userid, usertype, usersubtype);
                                        if (result == 102)
                                        {
                                            if (!string.IsNullOrEmpty(returnFilePath))
                                                strReturn = " <u><a href='../Download" + returnFilePath + "'>Download your error list file.</a></u>";
                                        }

                                        else if (result == 0)
                                        {

                                            strReturn = "Please Upload Files in Excel in Required format only   ";

                                        }

                                        else
                                        {
                                            //TempData["successMessage"] = "your Holiday list successfully uploaded.";

                                            strReturn = strReturn = "success";

                                        }
                                    }
                                }
                                else
                                {
                                    strReturn = "Please select country according to excel record";
                                }
                            }

                            else
                            {
                                strReturn = "Please Upload Files in .xlsx format only";
                            }

                        }
                    }


                    catch (Exception ex)
                    {

                    }

                }
            }


            return strReturn; //Json(ObjSiteUploadModel, JsonRequestBehavior.AllowGet);
        }
        public static IEnumerable<Dictionary<string, object>> MapHolidayDatasetData(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                yield return row;
            }
        }
        public static IEnumerable<Dictionary<string, object>> HolidayParseExcel(Stream document)
        {
            List<char> charsToRemove = new List<char>() { '@', ':', '-', '%', '[', ']' };
            using (var reader = ExcelReaderFactory.CreateReader(document))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = true,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }

                });
                foreach (DataColumn column in result.Tables.Cast<DataTable>().First().Columns)
                {
                    foreach (char c in charsToRemove)
                    {
                        column.ColumnName = column.ColumnName.Replace(c.ToString(), String.Empty);
                    }
                    column.ColumnName = 'A' + (String.Join("", column.ColumnName.Split()));
                }
                return MapHolidayDatasetData(result.Tables.Cast<DataTable>().First());
            }
        }
        public DataTable ConvertHolidayToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public DataTable HolidayExcelToDataTableUsingExcelDataReader(string storePath)
        {
            FileStream stream = System.IO.File.Open(storePath, FileMode.Open, FileAccess.Read);

            string fileExtension = Path.GetExtension(storePath);
            IExcelDataReader excelReader = null;
            if (fileExtension == ".xls")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (fileExtension == ".xlsx")
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }


            DataSet result = excelReader.AsDataSet();
            var test = result.Tables[0];
            return result.Tables[0];
        }

        public JsonResult BindHolidayTable(HolidayTypemaster model, string countryid)
        {

            int Countryid = Convert.ToInt32(!string.IsNullOrEmpty(countryid) ? Convert.ToInt32(Security.GetDecryptString(countryid)) : 0);
            model.CountryId = Countryid;
            List<HolidayTypemaster> lstholidaytable = new List<HolidayTypemaster>();
            lstholidaytable = _holiday.BindHolidaytable(model, Countryid);
            return Json(new { lstholidaytable = lstholidaytable });


        }

        public string DeleteBindHolidayTable(int RecordId)
        {

            string strReturn = "Some error occured.";

            if (RecordId > 0)
            {
                int result = _holiday.DeleteBindHolidayTable(RecordId);

                if (result == (int)StatusCode1.Success)
                    strReturn = "Record has been deleted successfully";
                else
                    strReturn = "Some error occured.";
            }
            return strReturn;

        }

        public JsonResult EditBindHolidayTable(int RecordId)
        {
            List<HolidayTypeSave> holidayTypeSaves = new List<HolidayTypeSave>();
            holidayTypeSaves = _holiday.EditBindHolidayTable(RecordId);
            return Json(holidayTypeSaves);
        }

        public IActionResult HolidayList()
        {
            return View();
        }

        [HttpPost]
        public string HolidayDetails()
        {
            List<HolidayDetails> holidayDetails = new List<HolidayDetails>();
            holidayDetails = _holiday.HolidayList();
            var result = JsonConvert.SerializeObject(new { data = holidayDetails });
            return result;
        }
    }
}