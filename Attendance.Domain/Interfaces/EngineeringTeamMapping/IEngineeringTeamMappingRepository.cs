using Attendance.Domain.Models.EngineeringTeamMapping;
using Attendance.Domain.Models.Company;
using Attendance.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.EngineeringTeamMapping
{
    public interface IEngineeringTeamMappingRepository
    {
        List<CompanyDetail> GetCompanyList(int CompanyId, string UserType, string UserSubType, int UserId);
        //List<CompanyDetail> GetCompanyList(int companyid, string userType, string userSubType, int userid);
       
        
        List<EmployeeJobDetails> GetEmployeeProjectbase(int projectID, int id, string userType, string userSubType);
        List<EmployeeApproveUnapprove> GetEmpRiggerTechnicianList(int vendorID, int projectId, int companyID, string empId, string userType, string userSubType);
        List<ProjectMasterOMMS> GetProjectListBasedOnVendor(int vendorId, string userType, string userSubType, int userid);
        List<VendorMaster> GetVendorBasedOnCompanyIduser(int companyId, int vendorid, string userType, string userSubType, int userid);
        int SaveRecord(string objModal, int status, int userid, int companyId);
    }


}
