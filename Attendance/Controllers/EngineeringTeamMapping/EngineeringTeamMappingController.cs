using Attendance.Domain.Models.UserDetail;
using Attendance.Domain.Models.Company;
using Attendance.Domain.Models.EngineeringTeamMapping;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using Microsoft.AspNetCore.Mvc.Routing;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.Employee;
using Attendance.Domain.Models.Master;
using Microsoft.AspNetCore.Http;
using Attendance.Domain;
using System.ComponentModel.Design;
using System.Data.OleDb;
using Attendance.Infrastructure.Data.Common;

namespace Attendance.Controllers.EngineeringTeamMappingController
{
    public class EngineeringTeamMappingController : BaseController
    {
        public EngineeringTeamMappingController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [HttpGet]
        public IActionResult EngineerTeamMapping()
        {
            List<CompanyDetail> lstcompany = new List<CompanyDetail>();
            var value = GetUserDetail();
            int companyid = value.CompanyId;
            int userid = value.Id;
            lstcompany = _etmservice.GetCompanyList(companyid , userid);
            ViewBag.Company = lstcompany;
            //foreach (var company in lstcompany)
            //{
                
            //}
            return View();
        }
        public IActionResult GetCompanyList()
        {
            List<CompanyDetail> lstCompany = new List<CompanyDetail>();
            var value = GetUserDetail();
            int companyid = value.CompanyId;
            int userid = value.Id;
            lstCompany = _etmservice.GetCompanyList(companyid,userid);
            ViewBag.Companylst = lstCompany;

            return View();

        }

        public string SaveRecord(EngineerTeamMapping objModal, int status)
        {
            string strReturn = "Some error occured.";
            if (objModal != null)
            {
                //, string CompanyID
                //int companyId = !string.IsNullOrEmpty(CompanyID) ? Convert.ToInt32(BAL.Common.Security.GetDecryptString(CompanyID)) : 0;
                int result = (int)StatusCode1.Error;
                //objModal.CompanyID = companyId;
                var model = JsonConvert.SerializeObject(objModal.ChkSelectAll);
                result = _etmservice.SaveRecord(model, status, 0);
                if (result == (int)StatusCode1.Success)
                    strReturn = "success";
                else
                    strReturn = "Update"; //;"Some error occured.";
                // }
            }

            return strReturn;



        }

        public JsonResult GetEmpRiggerTechnicianList(int VendorId, int ProjectId, string CompanyID, string EmpId)
        {
            int companyId = !string.IsNullOrEmpty(CompanyID) ? Convert.ToInt32(Security.GetDecryptString(CompanyID)) : 0;
            List<EmployeeApproveUnapprove> EmployeeApproveUnapproveList = new List<EmployeeApproveUnapprove>();
            EmployeeApproveUnapproveList = _etmservice.GetEmpRiggerTechnicianList(VendorId, ProjectId, companyId, EmpId);
            return Json(EmployeeApproveUnapproveList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetVendorListBasedOnCompanyIdvendor(string company , int companyId ,int Id )
        {
            
            List<VendorMaster> lstDetail = new List<VendorMaster>();
            var value = GetUserDetail();
            companyId = value.CompanyId;
            int userid = value.Id;
            
             Id = value.Id;


            companyId = !string.IsNullOrEmpty(company) ? Convert.ToInt32(Security.GetDecryptString(company)) : 0;
            lstDetail = _etmservice.GetVendorListBasedOnCompanyIdforvendor(companyId, Id , userid);
            //var data = Json(lstDetail);
            return Json(new {data = lstDetail});
            //if (lstDetail != null)
            //{
            //    return Json(new { data = lstDetail });
            //}
            //else
            //{
            //    return Json(string.Empty);
            //}
        }
        [HttpGet]
        public JsonResult GetProjectBasedOnVendor(int vendor)
        {
            List<ProjectMasterOMMS> lstDetail = new List<ProjectMasterOMMS>();
            //int vendorId = !string.IsNullOrEmpty(vendor) ? Convert.ToInt32(Security.GetDecryptString(vendor)) : 0;
            lstDetail = _etmservice.GetProjectListBasedOnVendorId(vendor);
            var Data = Json(lstDetail, JsonRequestBehavior.AllowGet);

            if (lstDetail != null)
                return Json(new{data = lstDetail});
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmployeeProjectbase(int ProjectId)
        {
            List<EmployeeJobDetails> _EmpMaster = null;
            _EmpMaster = _etmservice.GetEmployeeProjectbase(ProjectId);
            return Json(new{data = _EmpMaster});
        }
    }
}
