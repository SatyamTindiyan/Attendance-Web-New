using Attendance.ApplicationCore.Interfaces.Leave;
using Attendance.Domain.Models.Leave;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Services.Leave
{
    public class LeaveService : BaseService, ILeaveService
    {
        public LeaveService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public int DeleteLeaveMasterDetails(int recordId)
        {
            int num = 0;
            try
            {
                num = this._leaveRepository.DeleteLeaveMasterDetails(recordId);
            }
            catch
            {
            }
            return num;
        }

        public List<LeaveMaster> EditLeaveMasterDetail(int recordId)
        {
            List<LeaveMaster> lstleaveMaster = null;

            lstleaveMaster = new List<LeaveMaster>();

            lstleaveMaster = _leaveRepository.EditLeaveMasterDetail(recordId);
            return lstleaveMaster;
        }

        public LeaveStatus EmployeeLeaveStatus(LeaveStatus leaveStatus)
        {
            //List<VMLeaveStatus> lstleave = new List<VMLeaveStatus>();
            LeaveStatus? lstleave = null;

            lstleave = _leaveRepository.EmployeeLeaveStatus(leaveStatus);

            return lstleave;
        }

        public List<Companylist> GetCompanyList(int countryId)
        {
            List<Companylist> lstcomp = new List<Companylist>();
            lstcomp = _leaveRepository.GetCompanyList(countryId);
            return lstcomp;
        }

        public List<Departmentlist> GetDepartmentList(int company)
        {
            List<Departmentlist> lstcomp = new List<Departmentlist>();
            lstcomp = _leaveRepository.GetDepartmentList(company);
            return lstcomp;
        }

        public List<Empployeelist> GetEmpList(int companyId, int departmentID)
        {
            List<Empployeelist> lstcomp = new List<Empployeelist>();
            lstcomp = _leaveRepository.GetEmpList(companyId, departmentID);
            return lstcomp;
        }

        public List<ManageLeaveRequest> GetEmployeeLeaveCount(ManageLeaveRequest model)
        {
            List<ManageLeaveRequest> lstSummary = null;

            List<ManageLeaveRequest> manages = new List<ManageLeaveRequest>();

            manages = _leaveRepository.GetEmployeeLeaveCount(model);
            return manages;
        }

        public List<ManageLeaveRequest> GetLeaveApprovalList(ManageLeaveRequest model)
        {
            List<ManageLeaveRequest> lstSummary = new List<ManageLeaveRequest>();

            lstSummary = _leaveRepository.GetLeaveApprovalList(model);
            return lstSummary;
        }

        public List<LeaveMaster> GetLeaveMasterDetail(LeaveMaster model)
        {
            List<LeaveMaster> lstleaveMaster = null;

            lstleaveMaster = new List<LeaveMaster>();

            lstleaveMaster = _leaveRepository.GetLeaveMasterDetail(model);
            return lstleaveMaster;
        }

        public List<Leaves> GetLeaveNameList()
        {
            List<Leaves> lstleave = new List<Leaves>();
            lstleave = _leaveRepository.GetLeaveNameList();
            return lstleave;
        }

        public List<LeaveReport> GetLeaveReportList(LeaveReport model)
        {
            List<LeaveReport> lstLeaveReport = null;

            lstLeaveReport = new List<LeaveReport>();

            lstLeaveReport = _leaveRepository.GetLeaveReportList(model);
            return lstLeaveReport;
        }

        public List<ManageLeaveRequest> ManageLeaveRequest(int month, string eMPID, string year)
        {
            List<ManageLeaveRequest> lstSummary = null;

            lstSummary = new List<ManageLeaveRequest>();

            lstSummary = _leaveRepository.ManageLeaveRequest(month, eMPID, year);
            return lstSummary;
        }

        public int SaveLeaveApproved(DataTable dt)
        {
            return _leaveRepository.SaveLeaveApproved(dt);
        }

        public int SaveLeaveMaster(LeaveMaster modal)
        {
            return _leaveRepository.SaveLeaveMaster(modal);
        }

        public int SaveLeaveRequest(Leaves modal)
        {
            return _leaveRepository.SaveLeaveRequest(modal);
        }
    }
}
