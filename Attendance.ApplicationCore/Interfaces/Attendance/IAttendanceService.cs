using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.Attendance.ViewModel;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.UserDetail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Interfaces.Attendance
{
    public interface IAttendanceService
    {
        int ApprovedAttendance(DataTable dt);
        int ApprovedRegularization(DataTable dt);
        List<AttendanceRemote> Attendancedetailsofday(string EMPID, string Date);
        int AttendanceRegularizationInsert(AttendanceRemote modal, DateTime AR_DATE2, string Username);
        List<EmpAttenApprovalVM> GetAttendanceAppList(EmpAttenApproval model);
        List<AttenDetailVM> GetAttendanceDetailList(DateSearch model);


        //List<EmpAttenApproval> GetAttendanceAppList(EmpAttenApproval model);
        List<AttenHistoryVM> GetAttendanceHistoryList(AttendanceRemote modal);
        List<AttenHistory> GetAttendanceReportList(AttendanceRemote model);
        IEnumerable<AttendanceSummary> GetAttendanceSummary(DateSearch model);
        AttenSummary? GetAttensummry(AttenSummary attenSummary);
        List<Countries> GetCountryList();
        List<VendorType> GetEmployeeTeamAttendance(VendorType model);
        List<EmployeeVendor> GetEmployeeVendorList(string EMPID, string SearchValue);
        

        AttendanceRemote GetInOut(AttendanceRemote modal);
        List<AttenHistoryVM> Getintouttime(AttendanceRemote modal);
        AttendanceRemote Getlatlong(AttendanceRemote modal);
        List<EmpRegApprovalVM> GetRegulazationApprovalList(EmpRegularApproval model);
        List<VendorType> GetVendorList(string searchValue, int countryid);
        List<VendorType> GetVendorTeamAttendance(VendorType model);
        int InTimeMark(AttendanceRemote modal, string timezone2);
        int OutTimeMark(AttendanceRemote modal);
        AttendanceSummary Regularization(string empid, int month, int year);
        List<TeamInOutTime> TeamGetInOutTime(TeamInOutTime modal);
        int TeamInTimeMark(TeamAttendanceRemote modal, string timezone2);
        int TeamOutTimeMark(TeamInOutTime modal);
    }

    
}
