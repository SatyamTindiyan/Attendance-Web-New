using Attendance.ApplicationCore.Interfaces.Account;
using Attendance.ApplicationCore.Interfaces.Attendance;
using Attendance.Domain.Interfaces.Attendance;
using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.Attendance.ViewModel;
using Attendance.Domain.Models.UserDetail;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain;
using System.Reflection;

namespace Attendance.ApplicationCore.Services.Attendance
{
    public class AttendanceService : BaseService, IAttendanceService
    {


        public AttendanceService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public List<EmployeeVendor> GetEmployeeVendorList(string EMPID , string SearchValue)
        {
            List<EmployeeVendor> lstEmployee = null;

            try
            {
                lstEmployee = new List<EmployeeVendor>();
                lstEmployee = _attendanceRepository.GetEmployeeVendorList(EMPID , SearchValue);

            }
            catch
            {

            }
            return lstEmployee;
        }

        public AttendanceRemote GetInOut(AttendanceRemote modal)
        {
            var result = _attendanceRepository.GetInOut(modal);
            return result;
        }

        public int InTimeMark(AttendanceRemote modal, string timezone2)
        {
            return _attendanceRepository.InTimeMark(modal,timezone2);
        }

        public int OutTimeMark(AttendanceRemote modal)
        {
            return _attendanceRepository.OutTimeMark(modal);

        }

        public List<TeamInOutTime> TeamGetInOutTime(TeamInOutTime model)
        {
            List<TeamInOutTime> lstdtl = new List<TeamInOutTime>();
            lstdtl = _attendanceRepository.TeamGetInOutTime(model);
            return lstdtl;
        }

        public List<VendorType> GetVendorTeamAttendance(VendorType model)
        {
            List<VendorType> lstTeamAttendance = null;

            try
            {
                lstTeamAttendance = new List<VendorType>();
                lstTeamAttendance = _attendanceRepository.GetVendorTeamAttendance(model);

            }
            catch
            {

            }
            return lstTeamAttendance;
        }

        public int TeamInTimeMark(TeamAttendanceRemote modal, string timezone2)
        {
            return _attendanceRepository.TeamInTimeMark(modal, timezone2);
        }
        public int TeamOutTimeMark(TeamInOutTime modal)
        {
            return _attendanceRepository.TeamOutTimeMark(modal);

        } 

        public List<VendorType> GetEmployeeTeamAttendance(VendorType modal)
        {
            List<VendorType> lstTeamAttendance = null;
            try
            {
                lstTeamAttendance = new List<VendorType>();
                lstTeamAttendance = _attendanceRepository.GetEmployeeTeamAttendance(modal);

            }
            catch
            {
            }
            return lstTeamAttendance;
        }

        [HttpPost]
        public List<AttenHistoryVM> GetAttendanceHistoryList(AttendanceRemote model)
        {
            List<AttenHistoryVM> lstattendance = (List<AttenHistoryVM>)null;
            try
            {
                List<AttenHistoryVM> result= new List<AttenHistoryVM> ();

                result = (List<AttenHistoryVM>)this._attendanceRepository.GetAttendanceHistoryList(model);


                if (result != null)
                    //lstattendance = result.Select<AttenHistoryVM,AttenHistoryVM>((Func<AttenHistoryVM,AttenHistoryVM>)(X=> new AttenHistoryVM()
                   lstattendance = result.Select(X => new AttenHistoryVM
                    {
                        EMPID = X.EMPID,
                        Emp_Full_Name = X.Emp_Full_Name,
                        DATE = X.DATE,
                        INTIMEDB = X.INTIMEDB,
                        OUTTIMEDB = X.OUTTIMEDB,
                        RADIALINDISTANCE = X.RADIALINDISTANCE,
                        RADIALOUTDISTANCE = X.RADIALOUTDISTANCE,
                        CORDINATOR = X.CORDINATOR,
                        MANAGER = X.MANAGER,
                        HEAD = X.HEAD,
                        CountryId = X.CountryId,
                        StatusInTime = X.StatusInTime,
                        StatusOutTime = X.StatusOutTime

                   }).ToList();

            }
            catch
            {

            }
            return lstattendance;

            
             
            
            
        }

        public List<EmpAttenApprovalVM> GetAttendanceAppList(EmpAttenApproval model)
        {
            List<EmpAttenApprovalVM> lstattendance = null;

            lstattendance = new List<EmpAttenApprovalVM>();

            var result = _attendanceRepository.GetAttendanceAppList(model);

            if (result != null)
            {
                lstattendance = result.Select(X => new EmpAttenApprovalVM
                {
                    EMPID = X.EMPID,
                    DATE = X.DATE,
                    INTIME = X.INTIME,
                    OUTTIME = X.OUTTIME,
                    Emp_Full_Name = X.Emp_Full_Name,
                    RADIALINDISTANCE = X.RADIALINDISTANCE,
                    RADIALOUTDISTANCE = X.RADIALINDISTANCE,
                    APPstatusCORDINATOR = X.APPstatusCORDINATOR,
                    APPstatusMANAGER = X.APPstatusMANAGER,
                    APPstatusHEAD = X.APPstatusHEAD,
                    WorkingHour = X.WorkingHour,
                    DisableOption = X.DisableOption

                }).ToList();
            }
            return lstattendance;
        }

        public AttendanceSummary Regularization(string empid, int month, int year)
         {
            var result = _attendanceRepository.Regularization(empid, month, year);
            return result;
        }

        public List<AttenHistory> GetAttendanceReportList(AttendanceRemote model)
        {
            List<AttenHistory> lstattendance = null;

            lstattendance = new List<AttenHistory>();

            var result = _attendanceRepository.GetAttendanceReportList(model);

            if (result != null)
            {
                lstattendance = result.Select(X => new AttenHistory
                {
                    ID=X.ID,
                    EMPID = X.EMPID,
                    INTIMEDB = X.INTIMEDB,
                    OUTTIMEDB = X.OUTTIMEDB,
                    DATE = X.ARDATE,
                    RADIALINDISTANCE = X.RADIALINDISTANCE,
                    RADIALOUTDISTANCE = X.RADIALINDISTANCE,
                    AIntime = X.AIntime,
                    AOuttime = X.AOuttime,
                    RIntime = X.RIntime,
                    ROuttime = X.ROuttime,
                    ONDUTY = X.ONDUTY,
                    APPstatusHEAD = X.APPstatusHEAD,
                    timediffrence = X.timediffrence,
                    ShiftIntime = X.ShiftIntime,
                    Shiftouttime = X.Shiftouttime,
                   //  ShiftINtime = X.shiftINTime,
                    Workhours = X.Workhours,
                    ColorCode = X.ColorCode,

                }).ToList();
            }
            return lstattendance;
        }

        public List<AttenDetailVM> GetAttendanceDetailList(DateSearch model)
        {
            List<AttenDetailVM> lstattendance = null;

            lstattendance = new List<AttenDetailVM>();

            var result = _attendanceRepository.GetAttendanceDetailList(model);

            if (result != null)
            {
                lstattendance = result.Select(X => new AttenDetailVM
                {
                    EMPID = X.EMPID,
                    INTIMEDB = X.INTIMEDB,
                    OUTTIMEDB = X.OUTTIMEDB,
                    DATE = X.DATE,
                    Country = X.Country,
                    Emp_Full_Name = X.Emp_Full_Name,
                    RADIALINDISTANCE = X.RADIALINDISTANCE,
                    RADIALOUTDISTANCE = X.RADIALINDISTANCE,
                    CORDINATOR = X.CORDINATOR,
                    MANAGER = X.MANAGER,
                    HEAD = X.HEAD

                }).ToList();
            }
            return lstattendance;
        }

        public IEnumerable<AttendanceSummary> GetAttendanceSummary(DateSearch model)
        {
            List<AttendanceSummary> lstSummary = null;

            lstSummary = new List<AttendanceSummary>();

            var result = _attendanceRepository.GetAttendanceSummary(model);
            return result;
        }

        public List<EmpRegApprovalVM> GetRegulazationApprovalList(EmpRegularApproval model)
        {
            List<EmpRegApprovalVM> lstattendance = null;

            lstattendance = new List<EmpRegApprovalVM>();

            var result = _attendanceRepository.GetRegulazationApprovalList(model);

            if (result != null)
            {
                lstattendance = result.Select(X => new EmpRegApprovalVM
                {
                    EMPID = X.EMPID,
                    Emp_full_Name = X.Emp_Full_Name,
                    DATE = X.DATE,
                    INTIME = X.INTIME,
                    OUTTIME = X.OUTTIME,
                    WorkingHour = X.WorkingHour,
                    APPstatusCORDINATOR = X.APPstatusCORDINATOR,
                    APPstatusMANAGER = X.APPstatusMANAGER,
                    APPstatusHEAD = X.APPstatusHEAD,
                    DisableOption = X.DisableOption,


                }).ToList();
            }
            return lstattendance;
        }

        public List<Countries> GetCountryList()
        {
            List<Countries> lstCountry = null;

            try
            {
                lstCountry = new List<Countries>();
               var lstCountrybind = _attendanceRepository.GetCountryList();
                if (lstCountrybind != null)
                {
                    lstCountry = lstCountrybind.Select<Countries, Countries>((Func<Countries, Countries>)(x => new Countries()
                    {
                        CountryID = x.CountryID,
                        EncryptcountryID = Security.GetEncryptString(Convert.ToString(x.CountryID)),
                        Country = x.Country

                    })).ToList<Countries>();
                }

            }
            catch
            {

            }
            return lstCountry;
        }

        public int ApprovedAttendance(DataTable dt)
        {
            return _attendanceRepository.ApprovedAttendance(dt);

        }

        public int ApprovedRegularization(DataTable dt)
        {
            return _attendanceRepository.ApprovedRegularization(dt);

        }
        public int AttendanceRegularizationInsert(AttendanceRemote model , DateTime AR_DATE2, string Username)
        {
            return _attendanceRepository.AttendanceRegularizationInsert(model ,  AR_DATE2, Username);
        
        }

        public List<VendorType> GetVendorList(string SearchValue, int countryid)
        {
            List<VendorType> lstVenodr = null;

            try
            {
                lstVenodr = new List<VendorType>();
                lstVenodr = _attendanceRepository.GetVendorList(SearchValue, countryid);

            }
            catch
            {

            }
            return lstVenodr;
        }
        public AttenSummary GetAttensummry(AttenSummary attenSummary)
        {
            
            AttenSummary lstsummary = null;

            lstsummary = _attendanceRepository.GetAttensummry(attenSummary);

            return lstsummary;
        }

        public List<AttendanceRemote> Attendancedetailsofday(string EMPID, string Date)
        {
            List<AttendanceRemote> lstattendance = null;

            try
            {
                lstattendance = new List<AttendanceRemote>();
                lstattendance = _attendanceRepository.Attendancedetailsofday(EMPID, Date);

            }
            catch
            {

            }
            return lstattendance;
        }

        public List<AttenHistoryVM> Getintouttime(AttendanceRemote modal)
        {
            List<AttenHistoryVM> lstattendance = (List<AttenHistoryVM>)null;
            try
            {
                List<AttenHistoryVM> result = new List<AttenHistoryVM>();

                result = (List<AttenHistoryVM>)this._attendanceRepository.Getintouttime(modal);


                if (result != null)
                    lstattendance = result.Select(X => new AttenHistoryVM
                    {
                        EMPID = X.EMPID,
                        Emp_Full_Name = X.Emp_Full_Name,
                        DATE = X.DATE,
                        INTIMEDB = X.INTIMEDB,
                        OUTTIMEDB = X.OUTTIMEDB,
                      

                    }).ToList();

            }
            catch
            {

            }
            return lstattendance;

        }

        public AttendanceRemote Getlatlong(AttendanceRemote modal)
        {
            var result = _attendanceRepository.Getlatlong(modal);
            return result;
        }
    }



}
