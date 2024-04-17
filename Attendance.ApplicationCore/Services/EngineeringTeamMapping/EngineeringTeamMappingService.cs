using Attendance.ApplicationCore.Interfaces.EngineeringTeamMapping;
using Attendance.Domain;
using Attendance.Domain.Models.Company;
using Attendance.Domain.Models.EngineeringTeamMapping;
using Attendance.Domain.Models.Master;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Services.EngineeringTeamMapping
{
    public class EngineeringTeamMappingService : BaseService, IEngineeringTeamMappingService
    {
        public EngineeringTeamMappingService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public List<EmployeeApproveUnapprove> GetEmpRiggerTechnicianList(int VendorID, int ProjectId, int CompanyID, string EmpId)
        {
            List<EmployeeApproveUnapprove> employeeVendorbaseList = (List<EmployeeApproveUnapprove>)null;
            try
            {
                //VMUserDetail vmUserDetail2 = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                int companyid = MySession.comid;
                int userid = MySession.id;

                if (companyid != 0)
                {
                    if (userid > 0)
                    {
                        employeeVendorbaseList = new List<EmployeeApproveUnapprove>();
                        employeeVendorbaseList = this._etmrepository.GetEmpRiggerTechnicianList(VendorID, ProjectId, CompanyID, EmpId, MySession.UserType, MySession.UserSubType);
                    }
                }
            }
            catch
            {
            }
            return employeeVendorbaseList;
        }

        public int SaveRecord(string objModal, int status, int companyId)
        {
            int num = 0;
            try
            {
                //int userId = 0;
                //VMUserDetail vmUserDetail1 = new VMUserDetail();
                //VMUserDetail vmUserDetail2 = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];

                var userid = MySession.id;
                var username = MySession.EmpId;
                if (username != null)

                    num = this._etmrepository.SaveRecord(objModal, status, userid, companyId);
            }
            catch
            {
            }
            return num;
        }

        public List<CompanyDetail> GetCompanyList(int companyid , int userid)
        {
            List<CompanyDetail> companyList1 = null;
            try
            {
                
                UserDetail vmUserDetail1 = new UserDetail();
                

                if (companyid != 0)
                {
                    if (userid > 0)
                    {
                        // int companyId = vmUserDetail2.CompanyId;
                        companyList1 = new List<CompanyDetail>();
                        List<CompanyDetail> companyList2 = _etmrepository.GetCompanyList(companyid, MySession.UserType, MySession.UserSubType, userid);
                        if (companyList2 != null)
                            companyList1 = companyList2.Select (x => new CompanyDetail()
                            {
                                CompanyEncrpytId = Security.GetEncryptString(Convert.ToString(x.CompanyId)),
                                CompanyName = x.CompanyName
                            }).ToList();
                    }
                }
            }
            catch
            {
            }
            return companyList1;
        }
        public List<VendorMaster> GetVendorListBasedOnCompanyIdforvendor(int companyId, int vendorid , int userid)
        {
            List<VendorMaster> vendorMasterList = null;
            List<VendorMaster> companyIdforvendor = null;
            companyIdforvendor = new List<VendorMaster>();
            try
            {
                VMUserDetail vmUserDetail1 = new VMUserDetail();
                //VMUserDetail vmUserDetail2 = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                string empid = MySession.EmpId;
                //int userid = MySession.id;
                int companyid = MySession.comid;
                companyid = vmUserDetail1.CompanyId;
                if (empid != null)
                {
                    if (userid > 0)
                    {
                        vendorMasterList = new List<VendorMaster>();
                        //List<VendorMaster> basedOnCompanyIduser = _etmrepository.GetVendorBasedOnCompanyIduser(companyId, vendorid, MySession.UserType, MySession.UserSubType, userid);
                        var result = _etmrepository.GetVendorBasedOnCompanyIduser(companyId, vendorid, MySession.UserType, MySession.UserSubType, userid);

                        if (result !=null)
                            companyIdforvendor = result.Select(x => new VendorMaster()
                            {
                                VendorID = x.VendorID,
                                EncryptVendorId = Convert.ToString(Security.GetEncryptString(Convert.ToString(x.VendorID).Trim())),
                                Vendor = x.Vendor,
                                CompanyID = x.CompanyID,
                                VendorCode = x.VendorCode,
                                Status = x.Status,
                                Remarks = x.Remarks,
                                SpaceID = x.SpaceID,
                                VersionNumber = x.VersionNumber,
                                CompanyName = x.CompanyName,
                                Designation = x.Designation,
                                EmailAddress = x.EmailAddress,
                                MobileNumber = x.MobileNumber
                            }).OrderBy(x => x.Vendor).ToList();
                    }
                }
            }
            catch
            {
            }
            return companyIdforvendor;
        }
        public List<ProjectMasterOMMS> GetProjectListBasedOnVendorId(int vendorId)
        {
            List<ProjectMasterOMMS> listBasedOnVendorId = (List<ProjectMasterOMMS>)null;
            try
            {
                //VMUserDetail vmUserDetail1 = new VMUserDetail();
                //VMUserDetail vmUserDetail2 = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                int companyid = MySession.comid;
                int userid = MySession.id;
                if (companyid != 0)
                {
                    if (userid > 0)
                    {
                        listBasedOnVendorId = new List<ProjectMasterOMMS>();
                        listBasedOnVendorId = this._etmrepository.GetProjectListBasedOnVendor(vendorId, MySession.UserType, MySession.UserSubType, userid);
                    }
                }
            }
            catch
            {
            }
            return listBasedOnVendorId;
        }
        public List<EmployeeJobDetails> GetEmployeeProjectbase(int ProjectID)
        {
            List<EmployeeJobDetails> lstDetail = null;
            //VMUserDetail userMaster = new VMUserDetail();
            //userMaster = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
            try
            {
                lstDetail = new List<EmployeeJobDetails>();
                lstDetail = _etmrepository.GetEmployeeProjectbase(ProjectID, MySession.id, MySession.UserType, MySession.UserSubType);
            }
            catch
            {

            }

            return lstDetail;
        }
    }
}
