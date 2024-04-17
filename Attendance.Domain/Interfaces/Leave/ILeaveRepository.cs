using Attendance.Domain.Models.Leave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.Leave
{
    public interface ILeaveRepository
    {
        int DeleteLeaveMasterDetails(int recordId);
        List<LeaveMaster> EditLeaveMasterDetail(int recordId);
        LeaveStatus EmployeeLeaveStatus(LeaveStatus leaveStatus);
        List<Companylist> GetCompanyList(int countryId);
        List<Departmentlist> GetDepartmentList(int company);
        List<Empployeelist> GetEmpList(int companyId, int departmentID);
        List<ManageLeaveRequest> GetEmployeeLeaveCount(ManageLeaveRequest model);
        List<ManageLeaveRequest> GetLeaveApprovalList(ManageLeaveRequest model);
        List<LeaveMaster> GetLeaveMasterDetail(LeaveMaster model);
        List<Leaves> GetLeaveNameList();
        List<LeaveReport> GetLeaveReportList(LeaveReport model);
        List<ManageLeaveRequest> ManageLeaveRequest(int month, string eMPID, string year);
        int SaveLeaveApproved(DataTable dt);
        int SaveLeaveMaster(LeaveMaster modal);
        int SaveLeaveRequest(Leaves modal);
    }
}
