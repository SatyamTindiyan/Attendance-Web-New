using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.Leave;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;

namespace Attendance.Controllers.Leave
{
    public class LeaveController : BaseController
    {
        public LeaveController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public abstract class TempDataSerializer { }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Leave(LeaveStatus leaveStatus)
        {
            //LeaveStatus leaveStatus = new LeaveStatus();

            leaveStatus.EMPID = MySession.EmpId;
            try
            {
                if (!ModelState.IsValid)
                {
                    LeaveStatus? vMLeaveStatus = null;
                    vMLeaveStatus = _leaveService.EmployeeLeaveStatus(leaveStatus);
                    if (vMLeaveStatus != null)
                    {
                        ViewBag.user = vMLeaveStatus;
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public string SaveLeaveRequest(Leaves modal)
        {
            modal.EMP_ID = MySession.EmpId;
            modal.LEAVE_MONTH = DateTime.Now.Month;
            modal.LEAVE_YEAR = DateTime.Now.Year;

            string strReturn = "";
            var result = _leaveService.SaveLeaveRequest(modal);

            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return strReturn;
            //return strReturn;
            //return Json("", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ManageLeaveRequestsNew()
        {
            //List<ManageLeaveRequest> manageLeaves = new List<ManageLeaveRequest>();

            string EMPID = MySession.EmpId;

            int month = DateTime.Now.Month;
            string year = DateTime.Now.ToString("yyy");
            List<ManageLeaveRequest> manageLeaves = new List<ManageLeaveRequest>();
            manageLeaves = _leaveService.ManageLeaveRequest(month, EMPID, year);

            return Json(new { data = manageLeaves });
        }

        public IActionResult LeaveMaster()
        {
            return View();
        }

        [HttpPost]
        public string SaveLeaveMaster(LeaveMaster modal)
        {

            string strReturn = "";
            modal.OrganizationType = MySession.UserType;
            modal.Organizationid = MySession.id;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            modal.FromDate = Convert.ToDateTime(modal.FromDate).ToString("dd/MM/yyyy");

            modal.ToDate = Convert.ToDateTime(modal.ToDate).ToString("dd/MM/yyyy");
            var result = _leaveService.SaveLeaveMaster(modal);
            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return strReturn;
        }

        public IActionResult LeaveApproval()
        {
            string userName = string.Empty;
            var values = GetUserDetail();
            if (values != null)
            {
                userName = values.UserName;
                ViewBag.GetData = values;
            }

            return View();
        }

        public IActionResult GetLeaveApprovalList(ManageLeaveRequest model)
        {

            model.EMP_ID = MySession.EmpId;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            model.FROM = Convert.ToDateTime(model.FROM).ToString("yyyy/MM/dd");
            model.TO_DATE = Convert.ToDateTime(model.TO_DATE).ToString("yyyy/MM/dd");
            List<ManageLeaveRequest> lstSummary = new List<ManageLeaveRequest>();
            lstSummary = _leaveService.GetLeaveApprovalList(model);
            return Json(new { data = lstSummary });

            //else
            //{
            //    model.EMP_ID = MySession.EmpId;
            //    DateTime date = DateTime.Now;
            //    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            //    string sourceDateText = firstDayOfMonth.ToString();
            //    DateTime sourceDate = DateTime.Parse(sourceDateText);
            //    string formatted = sourceDate.ToString("yyyy-MM-dd");
            //    model.FROM = formatted;

            //    string sourceDateTextt = lastDayOfMonth.ToString();
            //    DateTime sourceDatee = DateTime.Parse(sourceDateTextt);
            //    string formattedd = sourceDatee.ToString("yyyy-MM-dd");
            //    model.TO_DATE = formattedd;
            //    List<ManageLeaveRequest> lstSummary = new List<ManageLeaveRequest>();
            //    lstSummary = _leaveService.GetLeaveApprovalList(model);

            //    return Json(new { data = lstSummary });
            //}
        }
        [HttpPost]
        public JsonResult SaveLeaveApproved(List<LeaveApprovald> appdata)
        {
            LeaveApprovald obj = null;
            obj = new LeaveApprovald();
            string strReturn = "";
            int result = (int)StatusCode1.Error;
            DataTable dt = new DataTable();
            dt.Columns.Add("EMPID", typeof(string));
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("APPLYDATE", typeof(string));
            dt.Columns.Add("APPstatusCORDINATOR", typeof(string));
            dt.Columns.Add("APPstatusMANAGER", typeof(string));
            dt.Columns.Add("APPstatusHEAD", typeof(string));

            foreach (LeaveApprovald item in appdata)

            {

                DataRow dr = dt.NewRow();

                dr["EMPID"] = item.EMPID;
                dr["ID"] = item.ID;
                dr["APPLYDATE"] = item.APPLYDATE;
                dr["APPstatusCORDINATOR"] = item.APPstatusCORDINATOR;
                dr["APPstatusMANAGER"] = item.APPstatusMANAGER;
                dr["APPstatusHEAD"] = item.APPstatusHEAD;
                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count > 0)
            {
                result = _leaveService.SaveLeaveApproved(dt);
            }
            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = " Record already exist.";
            else
                strReturn = "Some error occured.";
            return Json(new {    });

        }

        public IActionResult LeaveReport()
        {
            List<Countries> lstCoun = new List<Countries>();//new 24-03
            lstCoun = _attendanceService.GetCountryList();
            ViewBag.Countrylist = lstCoun;
            return View();
        }

        public IActionResult GetLeaveReport(LeaveReport model)
        {
            if (model.FROMDATE != null)
            {
                //model.EMP_ID = MySession.EmpId;
                string sourceDateText = model.FROMDATE;
                DateTime sourceDate = DateTime.Parse(sourceDateText);
                string formatted = sourceDate.ToString("yyyy-MM-dd");
                string sourceDateTextt = model.TODATE;
                //DateTime sourceDatee = DateTime.Parse(sourceDateTextt);
                //string formattedd = sourceDatee.ToString("yyyy-MM-dd");
                List<LeaveReport> result = new List<LeaveReport>();
                result = _leaveService.GetLeaveReportList(model);
                return Json(new { data = result });
            }
            else
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                string sourceDateText = firstDayOfMonth.ToString();
                DateTime sourceDate = DateTime.Parse(sourceDateText);
                string formatted = sourceDate.ToString("yyyy-MM-dd");
                string sourceDateTextt = lastDayOfMonth.ToString();
                DateTime sourceDatee = DateTime.Parse(sourceDateTextt);
                string formattedd = sourceDatee.ToString("yyyy-MM-dd");
                model.FROMDATE = formatted;
                model.TODATE = formattedd;
                model.Countryid = MySession.Countryid;
                model.EMP_ID = MySession.EmpId;
                model.Depid = 0;
                //var YEAR = DateTime.Now.ToString("yyy");
                List<LeaveReport> result = new List<LeaveReport>();
                result = _leaveService.GetLeaveReportList(model);
                return Json(new { data = result });
            }
        }


        public JsonResult GetCompanyList(int CountryId)
        {
            List<Companylist> lstCoun = new List<Companylist>();
            lstCoun = _leaveService.GetCompanyList(CountryId);
            if (lstCoun != null)
                return Json(lstCoun);
            else
                return Json(string.Empty);
        }

        public JsonResult GetDepartmentList(int company)
        {
            List<Departmentlist> lstCoun = new List<Departmentlist>();
            lstCoun = _leaveService.GetDepartmentList(company);
            if (lstCoun != null)
                return Json(lstCoun);
            else
                return Json(string.Empty);
        }

        public JsonResult GetEmpList(int CompanyId, int DepartmentID)
        {
            List<Empployeelist> lstCoun = new List<Empployeelist>();
            lstCoun = _leaveService.GetEmpList(CompanyId, DepartmentID);
            ViewBag.empid = lstCoun;
            if (lstCoun != null)
                return Json(lstCoun);
            else
                return Json(string.Empty);
        }

        public JsonResult GetLeaveMasterDetail(LeaveMaster model)
        {
            List<LeaveMaster> leaveMasters = new List<LeaveMaster>();
            leaveMasters = _leaveService.GetLeaveMasterDetail(model);

            if (leaveMasters != null)
                return Json(leaveMasters);
            else
                return Json(string.Empty);
        }

        public string DeleteLeaveMasterDetails(int RecordId)
        {

            string strReturn = "Some error occured.";

            if (RecordId > 0)
            {
                int result = _leaveService.DeleteLeaveMasterDetails(RecordId);

                if (result == (int)StatusCode1.Success)
                    strReturn = "Record has been deleted successfully";
                else
                    strReturn = "Some error occured.";
            }
            return strReturn;

        }

        public JsonResult EditLeaveMasterDetail(int RecordId)
        {
            //string OrganizationType = MySession.UserType;
            //int Organizationid = MySession.id;
            List<LeaveMaster> leaveMasters = new List<LeaveMaster>();
            leaveMasters = _leaveService.EditLeaveMasterDetail(RecordId);

            return Json(leaveMasters);
        }

        public JsonResult GetLeaveNameList()
        {
            List<Leaves> lstLeave = new List<Leaves>();
            lstLeave = _leaveService.GetLeaveNameList();
            if (lstLeave != null)
                return Json(lstLeave);
            else
                return Json(string.Empty);
        }

        public IActionResult GetEmployeeLeaveCount(ManageLeaveRequest model)
        {
            model.EMP_ID = MySession.EmpId;
            List<ManageLeaveRequest> manages = new List<ManageLeaveRequest>();
            manages = _leaveService.GetEmployeeLeaveCount(model);

            return Json(new { data = manages });
        }

    }
}
