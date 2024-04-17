using Attendance.Domain.Models.Company;
using Attendance.Domain.Models.EngineeringTeamMapping;
using Attendance.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Interfaces.EngineeringTeamMapping
{
    public interface IEngineeringTeamMappingService
    {
        //dynamic GetCompanyList();
        List<CompanyDetail> GetCompanyList(int companyid , int userid);
        List<EmployeeJobDetails>? GetEmployeeProjectbase(int projectId);
        List<EmployeeApproveUnapprove> GetEmpRiggerTechnicianList(int vendorId, int projectId, int companyId, string empId);
        List<ProjectMasterOMMS> GetProjectListBasedOnVendorId(int vendor);
        List<VendorMaster> GetVendorListBasedOnCompanyIdforvendor(int companyId, int id , int userid);
        int SaveRecord(string model, int status, int companyId);
    }
}
