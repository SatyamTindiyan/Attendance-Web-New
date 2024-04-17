using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.UserDetail;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Dapper;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using Attendance.Domain;

namespace Attendance.Controllers
{
   
    public class AttendanceController : BaseController
    {
        public AttendanceController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }
        public IActionResult AttendanceMark()
        {
            //AttendanceRemote attendanceRemote = new AttendanceRemote();

            AttendanceRemote modal = new AttendanceRemote();

            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (value != null)
            {
                userName = vMUserDetail.UserName;
                //strUserToken = vMUserDetail.CompanyId;

            }
            //userName = vMUserDetail.UserName;

            if (userName != null)
            {
                modal.LOGINID = vMUserDetail.UserName;

                InoutTime obj = null;
                var result = _attendanceService.GetInOut(modal);
                if (result != null)
                {
                    int ID = result.ID;
                    vMUserDetail.id = Convert.ToInt32(ID);
                    obj = new InoutTime
                    {
                        INTIMEDB = result.INTIMETZ,
                        OUTTIMEDB = result.OUTTIMETZ,
                    };
                }
                else
                {
                    obj = null;
                }
                return View(obj);

            }
            else
            {
                RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public string InTimeMark(AttendanceRemote modal)
        {
            AttendaceResponce responce = new AttendaceResponce(); 
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (value != null)
            {
                userName = vMUserDetail.UserName;
            }
            string hn = Dns.GetHostName();

            string strReturn = "";
            IPAddress[] ipaddress = Dns.GetHostAddresses(hn);
            foreach (IPAddress ip4 in ipaddress.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                modal.IP_ADDREESS = (ip4.ToString());
            }

            modal.VER = Convert.ToString(Environment.OSVersion);
            userName = vMUserDetail.UserName;
            modal.LOGINID = userName;
            string a = Environment.MachineName.ToString();
            modal.DEV_ID = a;
            modal.DEVICE = "Laptop";
           
            string q1 = Environment.UserName;
            string Timezone = DateTime.UtcNow.ToString();
            modal.UTCTimezone = TimeZoneInfo.Local;
            string timezone = modal.UTCTimezone.StandardName;
            string timezone2 = modal.UTCTimezone.DisplayName;
            string userAgent = HttpContext.Request.Headers["User-Agent"].ToString().ToLower();
            
            //string uaParser = Parser.GetDefault();
            //ClientInfo c = uaParser.Parse(userAgent);
            int mounth = DateTime.Now.Month;
            var YEAR = DateTime.Now.ToString("yyy");
            modal.AR_DATE = DateTime.UtcNow.Date;
            modal.MONTHNAME = mounth;
            modal.YEARNAME = Convert.ToInt32(YEAR);
            DateTime intime;
            if (DateTime.TryParseExact(modal.INTIMETZ, "M/d/yyyy h:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out intime))
            {
                modal.INTIMETZ = intime.ToString("yyyy-M-dd HH:mm:ss");
            }
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            //modal.INTIMETZ = Convert.ToDateTime(modal.INTIMETZ).ToString("yyyy-M-dd HH:mm:ss");
            ////string intimedate = Convert.ToString(DateTime.ParseExact(modal.INTIMETZ, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)).ToString();
            var result = _attendanceService.InTimeMark(modal,timezone2 );

            if (result == (int)StatusCode1.Success)
                strReturn = "Success";
            else if (result == (int)StatusCode1.NotExist)
                strReturn = "NotExist";
            else if (result == (int)StatusCode1.Punchintimeexist)
                strReturn = "Punchintimeexist";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = "Record already exist";
            else
                strReturn = "Some error occured.";
            return strReturn;

        }

        //Attendance outtime mark
        [HttpPost]
        public string OutTimeMark(AttendanceRemote modal)
        {
            //int id = MySession.id ;
            //modal.ID = id;
            //int result = 0;
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (value != null)
            {
                userName = vMUserDetail.UserName;
            }
            modal.LOGINID = userName;
            DateTime outtime;
            if (DateTime.TryParseExact(modal.OUTTIMETZ, "M/d/yyyy h:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out outtime))
            {
                modal.OUTTIMETZ = outtime.ToString("yyyy-M-dd HH:mm:ss");
            }
            string strReturn = "";
            var attendance = _attendanceService.OutTimeMark(modal);
            if (attendance == (int)StatusCode1.Success)
                strReturn = "Success";
            else if (attendance == (int)StatusCode1.Error)
                strReturn = "NotExist";
            else if (attendance == (int)StatusCode1.Punchouttimeexist)
                strReturn = "Punchouttimeexist";
            else if (attendance == (int)StatusCode1.IntimeisMissing)
                strReturn = "In-time is not Marked";
            else if (attendance == (int)StatusCode1.OuttimealreadyMarked)

                strReturn = " Out-time already Marked ";
            else
                strReturn = "Some error occured.";
            return strReturn;

        }

        public string TeamInTimeMark(TeamAttendanceRemote modal)
        {
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (value != null)
            {
                userName = vMUserDetail.UserName;
            }
            string hn = Dns.GetHostName();
            string strReturn = "";
            IPAddress[] ipaddress = Dns.GetHostAddresses(hn);
            foreach (IPAddress ip4 in ipaddress.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                modal.IP_ADDREESS = (ip4.ToString());
            }

            modal.VER = Convert.ToString(Environment.OSVersion);
            userName = vMUserDetail.UserName;
            modal.LOGINID = userName;
            string a = Environment.MachineName.ToString();
            modal.DEV_ID = a;
            modal.DEVICE = "Laptop";
            string q1 = Environment.UserName;
            string Timezone = DateTime.UtcNow.ToString();
            modal.UTCTimezone = TimeZoneInfo.Local;
            string timezone = modal.UTCTimezone.StandardName;
            string timezone2 = modal.UTCTimezone.DisplayName;
            string userAgent = HttpContext.Request.Headers["User-Agent"].ToString().ToLower();

            //string uaParser = Parser.GetDefault();
            //ClientInfo c = uaParser.Parse(userAgent);
            int mounth = DateTime.Now.Month;
            var YEAR = DateTime.Now.ToString("yyy");
            modal.AR_DATE = DateTime.UtcNow.Date;
            modal.MONTHNAME = mounth;
            modal.YEARNAME = Convert.ToInt32(YEAR);
            var result = _attendanceService.TeamInTimeMark(modal, timezone2);

            if (result == (int)StatusCode1.Success)
                strReturn = "Success";
            else if (result == (int)StatusCode1.NotExist)
                strReturn = "NotExist";
            else if (result == (int)StatusCode1.Punchintimeexist)
                strReturn = "Punchintimeexist";
            else if (result == (int)StatusCode1.AlreadyExist)

                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return strReturn;

            //if (result == (int)StatusCode1.Success)
            //    strReturn = "succss";
            //else if (result == (int)StatusCode1.NotExist)
            //    strReturn = "NotExist";
            //else if (result == (int)StatusCode1.AlreadyExist)

            //    strReturn = " Record already exist.";
            //else
            //    strReturn = "Some error occured.";
            //return strReturn;


        }
        //Attendance outtime mark
        [HttpPost]
        public string TeamOutTimeMark(TeamInOutTime modal)
        {
            string strReturn = "";
            var result = _attendanceService.TeamOutTimeMark(modal);
            if (result == (int)StatusCode1.Success)
                strReturn = "Success";
            else if (result == (int)StatusCode1.NotExist)
                strReturn = "NotExist";
            else if (result == (int)StatusCode1.Punchouttimeexist)
                strReturn = "Punchouttimeexist";
            else if (result == (int)StatusCode1.IntimeisMissing)
                strReturn = "In-time is not Marked";
            else if (result == (int)StatusCode1.OuttimealreadyMarked)

                strReturn = " Out-time already Marked ";
            else
                strReturn = "Some error occured.";
            return strReturn;
        }

        public JsonResult GetInOutTime(AttendanceRemote modal)
        {
            var value = GetUserDetail();
            modal.LOGINID = value.UserName;
            var result = _attendanceService.GetInOut(modal);
            return Json( new{ result = result});
        }

        public JsonResult Getlatlong (AttendanceRemote modal)
        {
            var value = GetUserDetail();
            modal.LOGINID = value.UserName;
            modal.usertype = value.UserType;
          
            modal.userSubType = value.UserSubType;
            modal.EMPID= value.EMP_ID;
            modal.EMPID = MySession.EmpId;
            modal.companyID= value.CompanyId;
            
            var result = _attendanceService.Getlatlong(modal);
            return Json(new { result = result });

        }
        public JsonResult GetTeamInOutTime(TeamInOutTime modal)
        {
            //string EmpId = string.Empty;
            //VMUserDetail vMUserDetail = new VMUserDetail();
            //var value = HttpContext.Session.GetString("LoggedUserInfo");
            //vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            //if (value != null)
            //{
            //    EmpId = vMUserDetail.EmpId;
            //}

            modal.Empid = MySession.EmpId;
            var result = _attendanceService.TeamGetInOutTime(modal);
            return Json(new { data = result });
        }

        public IActionResult AttendanceHistory()
        {
            return View();
        }

        [HttpPost]
        public string AttendanceHistory(AttendanceRemote modal)
        {

            modal.EMPID = MySession.EmpId;
            var countryid = 0;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (value != null)
            {
                countryid = vMUserDetail.CountryId;
            }
            var list = _attendanceService.GetAttendanceHistoryList(modal);

            //var SSS = JsonConvert.SerializeObject(result);
            var result = JsonConvert.SerializeObject(new { data = list });
            return result;




        }

        public JsonResult GetVendorTeamAttendance(VendorType model)
        {
            var value = GetUserDetail();
            model.LOGINID = value.UserName;
            List<VendorType> lstTeamAttendance = new List<VendorType>();
            lstTeamAttendance = _attendanceService.GetVendorTeamAttendance(model);
            //return Json(new { lstTeamAttendance = lstTeamAttendance });
            if (lstTeamAttendance != null)
                return Json(lstTeamAttendance = lstTeamAttendance);
            else
                return Json(string.Empty);
        }

        public JsonResult GetVendorList(string SearchValue)
        {
            var value = GetUserDetail();
            int countryid = value.CountryId;
            List<VendorType> lstVendor = new List<VendorType>();

            lstVendor = _attendanceService.GetVendorList(SearchValue, countryid);

            if (lstVendor != null)
                return Json(new { data = lstVendor });
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeVendorList(string SearchValue)
        {
            var value = GetUserDetail();
            var EMPID = value.EMP_ID;
            EMPID = MySession.EmpId;
            List<EmployeeVendor> lstEmployee = new List<EmployeeVendor>();
            lstEmployee = _attendanceService.GetEmployeeVendorList(EMPID , SearchValue);
            if (lstEmployee != null)
                return Json(new { lstEmployee = lstEmployee });
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetEmployeeTeamAttendance(VendorType modal)
        {
            var value = GetUserDetail();
            modal.LOGINID = value.UserName;
            //modal.EMPTYPE = value.UserSubType;
            List<VendorType> lstTeamAttendance = new List<VendorType>();
            lstTeamAttendance = _attendanceService.GetEmployeeTeamAttendance(modal);
            if (lstTeamAttendance != null)

                return Json(lstTeamAttendance = lstTeamAttendance);
            else
                return Json(string.Empty);

        }

        public IActionResult Regularization()
        {
            var value = GetUserDetail();
            string userName = value.UserName;
            try
            {
                if (userName != null)
                {
                    string empid = MySession.EmpId;
                    int month = DateTime.Now.Month;
                    var year = DateTime.Now.ToString("yyy");

                    AttendanceSummary obj = null;
                    var result = _attendanceService.Regularization(empid, month, Convert.ToInt32(year));
                    if (result != null)
                    {


                        obj = new AttendanceSummary
                        {
                            TotalDays = result.TotalDays,
                            RemainingDays = result.RemainingDays,
                            WeekOff = result.WeekOff,
                            SL = result.SL,
                            CL = result.CL,
                            PL = result.PL,
                            PublicHolidays = result.PublicHolidays,
                            PresentDays = result.PresentDays,
                        };
                    }
                    else
                    {
                        obj = null;
                    }
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
            

            // return View();
        }

        // Get Attendance report for Regularization //
        [HttpPost]
        public JsonResult Regularization(AttendanceRemote model)
        {
            List<AttenHistory> lstattendance = new List<AttenHistory>().ToList();
            model.EMPID = MySession.EmpId;
            model.ID = MySession.id;
            model.MONTHNAME = DateTime.Now.Month;

            // Initialization.
            //JsonResult result = new JsonResult();

            // Loading.
             lstattendance = _attendanceService.GetAttendanceReportList(model);
            // Processing.
            return Json(new { lstattendance = lstattendance });
            //return new JsonResult { Data = lstattendance, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            // var result = JsonConvert.SerializeObject(new { data = lstattendance });
        }

        [HttpPost]
        public JsonResult AttendanceRegularizationInsert(AttendanceRemote modal)
        {
            
            string? Username = null;
            var value = GetUserDetail();

            VMUserDetail usvmdtail = new VMUserDetail();
            if (value != null)
            {               
                Username = value.UserName;
            }
            modal.EMPID = MySession.EmpId;
            //modal.LOGINID =MySession.UserName;
            //modal.ID = 1;
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            //modal.INTIMEDB = Convert.ToDateTime(modal.INTIMEDB).ToString("yyyy/mm/dd");
             DateTime AR_DATE2 = Convert.ToDateTime(modal.AR_DATE1);
         
            //modal.DEVICE = "LAPTOP";
            var attendance = _attendanceService.AttendanceRegularizationInsert(modal , AR_DATE2, Username);
            return Json(new{attendance= attendance });
        }

        public IActionResult RegulazationApproval()
        {
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var valueS = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(valueS);
            var value = GetUserDetail();
            if (value != null)
            {
                //TempData["mydata"] = value;
                ViewBag.GetData = value;
            }


            return View();
        }

        [HttpPost]
        public JsonResult RegulazationApproval(EmpRegularApproval model)
        {
            if (model.fromdate != null)
            {
                model.EMPID = MySession.EmpId;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(model.fromdate).ToString("yyyy-MM-dd");

                model.todate = Convert.ToDateTime(model.todate).ToString("yyyy-MM-dd");

                var result = _attendanceService.GetRegulazationApprovalList(model).ToList();

                return Json(new { data = result });
            }
            else
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(firstDayOfMonth).ToString("yyyy-MM-dd");

                model.todate = Convert.ToDateTime(lastDayOfMonth).ToString("yyyy-MM-dd");

                model.EMPID = MySession.EmpId;

                //model.fromdate = firstDayOfMonth.ToString();
                //model.todate = lastDayOfMonth.ToString();

                var result = _attendanceService.GetRegulazationApprovalList(model).ToList();

                return Json(new { data = result });
            }

        }
        [HttpPost]
        public JsonResult ApprovedRegularization(List<RegularizationApproved> appdata)
        {
            RegularizationApproved obj = null;
            obj = new RegularizationApproved();
            string? strReturn = null;

            int result = (int)StatusCode1.Error;
            DataTable dt = new DataTable();
            dt.Columns.Add("EMPID", typeof(string));
            dt.Columns.Add("ARDATE", typeof(string));
            dt.Columns.Add("APPstatusCORDINATOR", typeof(string));
            dt.Columns.Add("APPstatusMANAGER", typeof(string));
            dt.Columns.Add("APPstatusHEAD", typeof(string));

        
            foreach (RegularizationApproved item in appdata)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                string date = Convert.ToDateTime(item.DATE).ToString("yyyy-MM-dd");

                DataRow dr = dt.NewRow();
                dr["EMPID"] = item.EMPID;
                dr["ARDATE"] = date;
                dr["APPstatusCORDINATOR"] = item.APPstatusCORDINATOR;
                dr["APPstatusMANAGER"] = item.APPstatusMANAGER;
                dr["APPstatusHEAD"] = item.APPstatusHEAD;
                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count > 0)
            {
                result = _attendanceService.ApprovedRegularization(dt);
            }
            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return Json(new { strReturn });
        }

        public IActionResult AttendanceApproval()
        {
            string userName = string.Empty;
            //VMUserDetail vMUserDetail = new VMUserDetail();
            //var value = HttpContext.Session.GetString("LoggedUserInfo");
            //vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            var values = GetUserDetail();
            if (values != null)
            {
                userName = values.UserName;
                ViewBag.GetData = values;
            }
            


            return View();
        }

        [HttpPost]
        public string AttendanceApproval(EmpAttenApproval model)
        {
            if (model.fromdate != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(model.fromdate).ToString("yyyy-MM-dd");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.todate = Convert.ToDateTime(model.todate).ToString("yyyy-MM-dd");
                model.EMPID = MySession.EmpId;
                var list = _attendanceService.GetAttendanceAppList(model);
                var result = JsonConvert.SerializeObject(new { data = list });
                return result;
            }
            else
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(firstDayOfMonth).ToString("yyyy-MM-dd");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.todate = Convert.ToDateTime(lastDayOfMonth).ToString("yyyy-MM-dd");

                model.EMPID = MySession.EmpId;
                //model.fromdate = firstDayOfMonth.ToString();
                //model.todate = lastDayOfMonth.ToString();

                var list = _attendanceService.GetAttendanceAppList(model);
                var result = JsonConvert.SerializeObject(new { data = list });
                return result;
            }
        }
        public IActionResult AttendanceDetail()
        {
            List<Countries> lstCoun = new List<Countries>();

            lstCoun = _attendanceService.GetCountryList();
            ViewBag.Countrylst = lstCoun;
            return View();
        }

        [HttpPost]
        public string AttendanceDetail(DateSearch model)
        {

            if (model.fromdate != null)
            {
                var list = _attendanceService.GetAttendanceDetailList(model);
               var result = JsonConvert.SerializeObject(new { data = list });
               return result;
            }
            else
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(firstDayOfMonth).ToString("yyyy-MM-dd");

                model.todate = Convert.ToDateTime(lastDayOfMonth).ToString("yyyy-MM-dd");

                // model.todate = lastDayOfMonth.ToString();
                model.counid = MySession.Countryid;

                var list = _attendanceService.GetAttendanceDetailList(model);

                var result = JsonConvert.SerializeObject(new { data = list });
                return result;
            }
        }

        [HttpPost]
        public JsonResult ApprovedAttendance(List<AttendanceApproved> appdata)
        {
            AttendanceApproved obj = null;
            obj = new AttendanceApproved();
            string strReturn = "";
            int result = (int)StatusCode1.Error;
            DataTable dt = new DataTable();
            dt.Columns.Add("EMPID", typeof(string));
            dt.Columns.Add("ARDATE", typeof(string));
            dt.Columns.Add("APPstatusCORDINATOR", typeof(string));
            dt.Columns.Add("APPstatusMANAGER", typeof(string));
            dt.Columns.Add("APPstatusHEAD", typeof(string));

       
            foreach (AttendanceApproved item in appdata)

            {

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                string date = Convert.ToDateTime(item.ARDATE).ToString("yyyy-MM-dd");

                DataRow dr = dt.NewRow();
                dr["EMPID"] = item.EMPID;
                dr["ARDATE"] = date;
                dr["APPstatusCORDINATOR"] = item.APPstatusCORDINATOR;
                dr["APPstatusMANAGER"] = item.APPstatusMANAGER;
                dr["APPstatusHEAD"] = item.APPstatusHEAD;
                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count > 0)
            {
                result = _attendanceService.ApprovedAttendance(dt);
            }
            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return Json(new { strReturn });
        }

        public IActionResult AttendanceSummary()
        {
            List<Countries> lstCoun = new List<Countries>();

            lstCoun = _attendanceService.GetCountryList();
            ViewBag.Countrylst = lstCoun;
            return View();
        }

        [HttpPost]
        public string AttendanceSummary(DateSearch model)
       {
            if (model.fromdate != null)
            {

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(model.fromdate).ToString("yyyy-MM-dd");

                var list = _attendanceService.GetAttendanceSummary(model);
                var result = JsonConvert.SerializeObject(new { data = list });
                return result;
                //return new JsonResult (result , JsonRequestBehavior.AllowGet );
            }
            else
            {
                model.counid = MySession.Countryid;
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                model.fromdate = Convert.ToDateTime(lastDayOfMonth).ToString("yyyy-MM-dd");

                // model.fromdate = lastDayOfMonth.ToString();
                var list = _attendanceService.GetAttendanceSummary(model);
                //return new JsonResult ( result, JsonRequestBehavior .AllowGet );
                var result = JsonConvert.SerializeObject(new { data = list });
                return result;

                //return Json(new { result = result });
            }

        }

        public IActionResult GetCountry()
        {
            List<Countries> lstCoun = new List<Countries>();

            lstCoun = _attendanceService.GetCountryList();
            ViewBag.Countrylst = lstCoun;
            return Json(new { data = lstCoun });

        }
        //public JsonResult GetCountries()
        //{
        //    List<Countries> lstCoun = new List<Countries>();

        //    lstCoun = _attendanceService.GetCountryList();

        //    return Json(new { data = lstCoun });

        //}

       

         //Get Attendance details on click of date in Regularization //
        public JsonResult Attendancedetailsofday(IFormCollection formData)
        {
            List<AttendanceRemote> lstattendance = new List<AttendanceRemote>();
            


            string EMPID = MySession.EmpId;
            int ID = MySession.id;
            string Date = formData["Date"];
            // Loading.
             lstattendance = _attendanceService.Attendancedetailsofday(EMPID,Date);
            // Processing.
            return Json(new { lstattendance = lstattendance });
         

        }

        public string Getintouttime(AttendanceRemote modal)
        {

            modal.EMPID = MySession.EmpId;
            var list = _attendanceService.Getintouttime(modal);

            var result = JsonConvert.SerializeObject(new { data = list });
            return result;




        }
    }
}
