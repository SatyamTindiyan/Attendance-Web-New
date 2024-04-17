using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.Infra;
using Attendance.Domain.Models.Reimbursement;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using FormCollection = Microsoft.AspNetCore.Http.FormCollection;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;



namespace Attendance.Controllers.Reimbursement
{
    public class ReimbursementController : BaseController
    {

        List<SelectListItem> ddlMonths = new List<SelectListItem>();
        List<SelectListItem> ddlYears = new List<SelectListItem>();

        public ReimbursementController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Reimbursement(int? Year)
        {
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = GetUserDetail();
            if (value != null)
            {
                userName = value.UserName;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }

            //ViewBag.linktoYearId = GetYears(Year);
            //ViewBag.linktoMonthId = GetMonths(Year);
            ViewBag.empid = MySession.EmpId;
            ViewBag.user = userName;
            return View();
        }

        public JsonResult Getyear()
        {
            int currentYear = DateTime.Now.Year;
            int startYear = 2021;
            List<SelectListItem> yearList = new List<SelectListItem>();
            for (int year = currentYear; year >= startYear; year--)
            {
                yearList.Add(new SelectListItem
                {
                    Text = year.ToString(),
                    Value = year.ToString()
                });
            }
            return Json(new { yearList });


        }
        public JsonResult Getmonth(int year)
        {
            var months = Enumerable.Range(1, 12).Select(i => new
            {
                A = i,
                B = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
            });

            //int CurrentMonth = 1; //January  

            if (year == DateTime.Now.Year)
            {
                int CurrentMonth = DateTime.Now.Month;
                months = Enumerable.Range(1, CurrentMonth).Select(i => new
                {
                    A = i,
                    B = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                });
            }

            foreach (var item in months)
            {
                ddlMonths.Add(new SelectListItem
                {
                    Text = item.B.ToString(),
                    Value = item.A.ToString()
                });
            }

            return Json(new { ddlMonths });


        }
        public IActionResult ReimbursementHeadApproval(int? Year)
        {
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = GetUserDetail();
            try
            {
                if (value != null)
                {
                    ViewBag.GetReimbursemnetData = value;
                    userName = value.UserName;
                }
                if (Year == null)
                {
                    Year = DateTime.Now.Year;
                }

                //ViewBag.linktoYearId = GetYears(Year);
                //ViewBag.linktoMonthId = GetMonths(Year);
                ViewBag.GetReimbursemnetData = value;
                ViewBag.empid = MySession.EmpId;
                ViewBag.user = userName;
                return View();
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        //* Fetch Data in Get Reimbursement*//
        public JsonResult GetReimbursement(IFormCollection formcollection, int id)
        {
            List<ReimbursementNew> lstReimbursement = new List<ReimbursementNew>();
            List<ClaimType> lstClaimType = new List<ClaimType>();
            List<SubClaimType> lstSubClaimType = new List<SubClaimType>();
            List<ClaimStatus> lstClaimSatus = new List<ClaimStatus>();
            if (formcollection != null)
            {
                int Month = Convert.ToInt32(formcollection["Month"]);
                int year = Convert.ToInt32(formcollection["year"]);
                string EmpId = formcollection["EmpId"];
                int claimType = Convert.ToInt32(formcollection["_ClaimType"]);


                lstReimbursement = _reimbursementservice.GetReimbursementDtl(Month, year, EmpId);
                lstClaimType = _reimbursementservice.GetClaimType();
                lstSubClaimType = _reimbursementservice.GetSubClaimType(claimType);
                lstClaimSatus = _reimbursementservice.GetClaimSatus();
            }
            return Json(new
            {
                dt = lstReimbursement,
                dtt = lstClaimType,
                SubType = lstSubClaimType,
                ClaimSatus = lstClaimSatus
            });
        }
        public string ReimbursementSaveMobile(int Month, int year, string EmpId, string EmpName, int claimType, int clamSubType, string claimDate, int claimAmount, string remarks, int claimStatus, string BaseFile, int kmIN = 0, int kmOUT = 0)
        {
            string strReturn = string.Empty;
            string strfile = System.IO.Path.GetExtension(BaseFile);
            string strfileeee = Path.GetExtension(BaseFile);

            string _FileName = BaseFile.Replace("data:image/png;base64,", "");
            int result = (int)StatusCode1.Error;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            string claimDatee = Convert.ToDateTime(claimDate).ToString("yyyy-MM-dd");

            result = _reimbursementservice.SaveReimbursementMobile(Month, year, EmpId, EmpName, claimType, clamSubType, claimDatee, claimAmount, kmIN, kmOUT, remarks, claimStatus, _FileName);

            if (result == (int)StatusCode1.Success)
            {
                strReturn = "success";
            }
            //}

            // }
            return strReturn;
        }

        //public string ReimbursementSave(IFormCollection formData)
        //{
        //    int result = (int)StatusCode1.Error;
        //    string strReturn = string.Empty;
        //    List<string> list = new List<string>();
        //    if (formData != null)
        //    {
        //        int Month = Convert.ToInt32(formData["Month"]);
        //        int year = Convert.ToInt32(formData["year"]);
        //        string? EmpName = formData["EmpName"];
        //        string? EmpId = formData["EmpId"];
        //        int claimType = Convert.ToInt32(formData["_ClaimType"]);
        //        int clamSubType = Convert.ToInt32(formData["ClaimTypeSub"]);
        //        string? claimDate = formData["ClaimDate"];
        //        int claimAmount = Convert.ToInt32(formData["ClaimAmt"]);
        //        string? remarks = formData["Remarks"];
        //        int claimStatus = Convert.ToInt32(formData["ClaimStatus"]);
        //        string? BaseFile = formData["FileName"];
        //        int kmIN = Convert.ToInt32(formData["KMIN"]);
        //        int kmOUT = Convert.ToInt32(formData["KMOUT"]);


        //        string? strfile = System.IO.Path.GetExtension(BaseFile);
        //        string? strfileeee = Path.GetExtension(BaseFile);

        //        string? _FileName = BaseFile.Replace("data:image/png;base64,", "");

        //        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
        //        string claimDatee = Convert.ToDateTime(claimDate).ToString("yyyy-MM-dd");

        //        result = _reimbursementservice.SaveReimbursement(Month, year, EmpId, EmpName, claimType, clamSubType, claimDatee, claimAmount, kmIN, kmOUT, remarks, claimStatus, _FileName);
        //    }
        //    if (result == (int)StatusCode1.Success)
        //    {
        //        strReturn = "success";
        //    }
        //    return strReturn;
        //}

        public string SaveReimbursement(string ClaimDetail, int Month, int year, string EmpId, string image, IFormCollection formdata)
        {

            string strReturn = string.Empty;
            ReimbursementNew model = new ReimbursementNew();
            model.ClaimDetail = ClaimDetail;
            model.Month = Month;



            if (formdata != null)
            {


            }
            var result = _reimbursementservice.SaveReimbursement(ClaimDetail, Month, year, EmpId);

            if (result == (int)StatusCode1.Success)
            {
                strReturn = "success";
            }

            return strReturn;
        }

        [HttpPost]
        public JsonResult Bindddl(IFormCollection formdata)
        {
            List<ClaimType> lstClaimType = new List<ClaimType>();
            List<SubClaimType> lstSubClaimType = new List<SubClaimType>();
            List<ClaimStatus> ClaimSatus = new List<ClaimStatus>();
            if (formdata != null)
            {
                int claimType = Convert.ToInt32(formdata["_ClaimType"]);

                lstClaimType = _reimbursementservice.GetClaimType();
                lstSubClaimType = _reimbursementservice.GetSubClaimType(claimType);
                ClaimSatus = _reimbursementservice.GetClaimSatus();
            }
            if (lstClaimType != null)

                return Json(new { dtt = lstClaimType, SubType = lstSubClaimType, ClaimSatus = ClaimSatus });
            else
                return Json(string.Empty);
        }

        //* Fetch Data in reimbursement approval//
        public JsonResult GetReimbursementApproval(IFormCollection formcollection)
        {

            List<ReimbursementNew> lstReimbursement = new List<ReimbursementNew>();
            List<ClaimType> lstClaimType = new List<ClaimType>();
            List<SubClaimType> lstSubClaimType = new List<SubClaimType>();
            List<ClaimStatus> lstClaimSatus = new List<ClaimStatus>();
            if (formcollection != null)
            {
                int Month = Convert.ToInt32(formcollection["Month"]);
                int year = Convert.ToInt32(formcollection["year"]);
                string? EmpId = formcollection["EmpId"];
                int claimType = Convert.ToInt32(formcollection["_ClaimType"]);

                lstReimbursement = _reimbursementservice.GetReimbursementDtl(Month, year, EmpId);
                //lstClaimType = _reimbursementservice.GetClaimType();
                //lstSubClaimType = _reimbursementservice.GetSubClaimType(claimType);
                //lstClaimSatus = _reimbursementservice.GetClaimSatus();
            }
            return Json(new { data = lstReimbursement/*, dtt = lstClaimType, SubType = lstSubClaimType, ClaimSatus = lstClaimSatus*/ });
            //if (lstReimbursement != null)
            //    // var jsonData = new { dt = dt, dtt = dtt, SubType = SubType, ClaimSatus = ClaimSatus };
            //    return Json(new { dt = lstReimbursement, dtt = lstClaimType, SubType = lstSubClaimType, ClaimSatus = lstClaimSatus }, JsonRequestBehavior.AllowGet);
            //else
            //    return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DownloadFile(IFormCollection formdata, string fileName, int imageid)
        {

            byte[] imageBytes;

                var splited = fileName.Split(new char[] { ',' });
            if (string.IsNullOrEmpty(fileName))
            {

                return Json(new { success = false, errorMessage = "File name is null or empty" });
            }

            //byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(splited[1]);
            }
            catch (FormatException)
            {

                return Json(new { success = false, errorMessage = "Invalid Base64 string" });
            }
            return Json(new { imageBytes = imageBytes });

        }

        [HttpPost]
        public JsonResult bindSutbtypeClaim(IFormCollection formdata)
        {

            List<SubClaimType> lstSubClaimType = new List<SubClaimType>();
            if (formdata != null)
            {
                int claimtype = Convert.ToInt32(formdata["ClaimType"]);

                lstSubClaimType = _reimbursementservice.GetSubClaimTypes(claimtype);
            }
            return Json(new { lstSubClaimType = lstSubClaimType });
        }
        public IActionResult ReimbursementMobile(int? Year)
        {
            string userName = string.Empty;

            var value = GetUserDetail();
            if (value != null)
            {
                userName = value.UserName;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }

            //ViewBag.linktoYearId = GetYears(Year);
            //ViewBag.linktoMonthId = GetMonths(Year);
            ViewBag.empid = MySession.EmpId;
            ViewBag.user = userName;
            return View();
        }

        public IActionResult ReimbursementHeadApprovalMobile(int? Year)
        {
            string userName = string.Empty;
            var value = GetUserDetail();
            if (value != null)
            {
                userName = value.UserName;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }

            //ViewBag.linktoYearId = GetYears(Year);
            //ViewBag.linktoMonthId = GetMonths(Year);
            ViewBag.GetData = value;
            ViewBag.empid = MySession.EmpId;
            ViewBag.user = userName;
            return View();
        }

        public string SaveHeadReimbursementApproval(int ApprovalId, int Status)
        {
            string strReturn = string.Empty;

            int result = (int)StatusCode1.Error;
            result = _reimbursementservice.SaveHeadReimbursementApproval(ApprovalId, Status);

            if (result == (int)StatusCode1.Success)
            {


                strReturn = "success";
            }

            return strReturn;
        }

        public string SaveHeadReimbursementApprovalDtl(string ClaimDetail, int Month, int year, string EmpId, string ARDATE, IFormCollection formdata)
        {
            string strReturn = string.Empty;
            ReimbursementNew model = new ReimbursementNew();
            model.ClaimDetail = ClaimDetail;
            model.Month = Month;
            if (formdata != null)
            {


            }
            var result = _reimbursementservice.SaveHeadReimbursementApprovalDtl(ClaimDetail, Month, year, EmpId ,ARDATE);
            if (result == (int)StatusCode1.Success)
            {
                strReturn = "success";
            }
            return strReturn;
        }

         public string SaveReimbursementApprovalDtl(string ClaimDetail, string image, IFormCollection formdata)
        {

            string strReturn = string.Empty;
            ReimbursementNew model = new ReimbursementNew();
       
            int Month = 0;
            int year = 0;
            string? EmpId = null;
            List<int> claimId = new List<int> ();

            // Deserialize the JSON data into an object
            var claimDetail = JsonConvert.DeserializeObject<List<ClaimViewModel>>(ClaimDetail);

            // Access the decoded values
            foreach (var claimData in claimDetail)
            {
                // Access the properties of claimData object
                 claimId.Add(claimData.ClaimId);
              
            }


            if (formdata != null)
            {
                Month = Convert.ToInt32(formdata["Month"]);
                year = Convert.ToInt32(formdata["year"]);
                string? EmpName = formdata["EmpName"];
                EmpId = formdata["EmpId"];

            }
            var result = _reimbursementservice.SaveReimbursementApproval(ClaimDetail, Month, year, EmpId,claimId);

            if (result == (int)StatusCode1.Success)
            {
                strReturn = "success";
            }

            return strReturn;
        }

        // Action method for submitting claims
        [HttpPost]
        public IActionResult Submit(ClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Save the claim details and bill image to the database
                    int newClaimId = _reimbursementservice.InsertReimbursementClaim(model.EmployeeId, model.Amount, model.BillImagePath);

                    // Fetch the updated claim status
                    var claimStatus = _reimbursementservice.GetClaimStatusById(newClaimId);

                    return Json(claimStatus);
                }
                catch (Exception ex)
                {
                    // Log the exception

                    return StatusCode(500, "An error occurred while submitting the claim.");
                }
            }

            return BadRequest(ModelState);
        }

        // Action method to fetch the claim status
        [HttpGet]
        public IActionResult ClaimStatus()
        {
            string empid = "LHQ441";
            int month = 6;
            int year = 2023;
            try
            {
                List<ClaimStatusViewModel> claimStatuses = _reimbursementservice.GetClaimStatuses(month, year, empid);
                return Json(claimStatuses);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while fetching claim status.");
            }
        }

        // Action method for updating claim status
        [HttpPost]
        public IActionResult UpdateStatus(int claimId, string approvalLevel, string status)
        {
            try
            {
                _reimbursementservice.UpdateClaimStatus(claimId, approvalLevel, status);

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the claim status.");
            }
        }

        // Account Reimbursement methods //
        public IActionResult AccountReimbursement(int? Year)
        {
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = GetUserDetail();

            if (value != null)
            {
                ViewBag.GetReimbursemnetData = value;
                userName = value.UserName;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }

            return View();
        }

        // Function for Get all Reimbursement details after manager approval//

        [HttpPost]
        public JsonResult GetAcReimbursementApproval(IFormCollection formcollection)
        {
            int UserId = 0;
            string? UserType = null;
            string? UserSubType = null;
            List<ReimbursementNew> lstReimbursement = new List<ReimbursementNew>();
            var values = GetUserDetail();
            if(values != null)
            {
                UserId = values.Id;
                UserType=values.UserType; 
                UserSubType = values.UserSubType;
            }
            if (formcollection != null)
            {
                int Month = Convert.ToInt32(formcollection["Month"]);
                int year = Convert.ToInt32(formcollection["year"]);
                string? EmpId = formcollection["EmpId"];
                EmpId = MySession.EmpId;
                int claimType = Convert.ToInt32(formcollection["_ClaimType"]);


                lstReimbursement = _reimbursementservice.GetAcReimbursement(Month, year, EmpId ,UserId,UserType,UserSubType);

            }
            return Json(new { data = lstReimbursement });

        }


        // Function for Get Paid Reimbursement details //

        [HttpPost]
        public JsonResult GetPaidReimbursement(IFormCollection formcollection, int id)
        {
            List<ReimbursementNew> lstReimbursement = new List<ReimbursementNew>();

            if (formcollection != null)
            {
                int Month = Convert.ToInt32(formcollection["Month"]);
                int year = Convert.ToInt32(formcollection["year"]);
                string EmpId = formcollection["EmpId"];
                int claimType = Convert.ToInt32(formcollection["_ClaimType"]);


                lstReimbursement = _reimbursementservice.GetPaidReimbursement(Month, year, EmpId);

            }
            return Json(new { lstReimbursement = lstReimbursement });
        }

        // Function for save account approval details //
        public string SaveAccountReimbursementApproval(string ClaimDetail, int Month, int year, string EmpId, string ARDATE, IFormCollection formdata)
        {
            string strReturn = string.Empty;
            ReimbursementNew model = new ReimbursementNew();
            model.ClaimDetail = ClaimDetail;
            model.Month = Month;
            List<int> claimId = new List<int>();

            // Deserialize the JSON data into an object
            var claimDetail = JsonConvert.DeserializeObject<List<ClaimViewModel>>(ClaimDetail);

            // Access the decoded values
            foreach (var claimData in claimDetail)
            {
                // Access the properties of claimData object
                claimId.Add(claimData.ClaimId);

            }
            if (formdata != null)
            {


            }
            var result = _reimbursementservice.SaveacReimbursementApproval(ClaimDetail, Month, year, EmpId, ARDATE ,claimId);

            if (result == (int)StatusCode1.Success)
            {
                strReturn = "success";
            }

            return strReturn;

        }

        //Get Paid report//
        public IActionResult PaidReport(int? Year)
        {
            string userName = string.Empty;
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = GetUserDetail();
            if (value != null)
            {
                userName = value.UserName;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }

            //ViewBag.linktoYearId = GetYears(Year);
            //ViewBag.linktoMonthId = GetMonths(Year);
            ViewBag.empid = MySession.EmpId;
            ViewBag.user = userName;
            return View();
        }


    }

}
