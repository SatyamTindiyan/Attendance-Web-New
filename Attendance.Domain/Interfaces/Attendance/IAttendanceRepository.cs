using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.Attendance.ViewModel;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.Attendance
{
    public interface IAttendanceRepository
    {
        List<EmployeeVendor> GetEmployeeVendorList(string EMPID, string SearchValue);

        //AttendanceRemote GetInOut(AttendanceRemote model);
        AttendanceRemote GetInOut(AttendanceRemote modal);
        List<VendorType> GetVendorTeamAttendance(VendorType model);
        int InTimeMark(AttendanceRemote modal, string timezone2);
        int OutTimeMark(AttendanceRemote modal);
        int TeamInTimeMark(TeamAttendanceRemote modal, string timezone2);
        int TeamOutTimeMark(TeamInOutTime modal);
        

        AttendanceSummary Regularization(string empid, int month, int year);
        List<AttenHistory> GetAttendanceReportList(AttendanceRemote model);
        List<VendorType> GetEmployeeTeamAttendance(VendorType modal);
        List<AttenHistoryVM> GetAttendanceHistoryList(AttendanceRemote model);
        List<TeamInOutTime> TeamGetInOutTime(TeamInOutTime modal);
        IEnumerable<AttendanceSummary> GetAttendanceSummary(DateSearch model);
        List<Countries> GetCountryList();
        List<VendorType> GetVendorList(string searchValue, int countryid);
        int ApprovedAttendance(DataTable dt);
        List<AttenDetail> GetAttendanceDetailList(DateSearch model);
        int ApprovedRegularization(DataTable dt);
        List<EmpRegularApproval> GetRegulazationApprovalList(EmpRegularApproval model);
        int AttendanceRegularizationInsert(AttendanceRemote model , DateTime AR_DATE2, string Username);
        List<EmpAttenApproval> GetAttendanceAppList(EmpAttenApproval model);
        AttenSummary? GetAttensummry(AttenSummary attenSummary);
        List<AttendanceRemote> Attendancedetailsofday(string eMPID, string date);
        List<AttenHistoryVM> Getintouttime(AttendanceRemote model);
        AttendanceRemote Getlatlong(AttendanceRemote modal);
    }
}
