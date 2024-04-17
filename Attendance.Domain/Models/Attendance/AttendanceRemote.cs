using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class AttendanceRemote : DateSearch
    {
        public int ID { get; set; }

        public int OrganizationID { get; set; }
        public string? LOGINID { get; set; }
        public string? UserName { get; set; }
        public string? EMPID { get; set; }
        public string? INTIMETZ{ get; set; }
        public string? INTIMEREVISED { get; set; }   
        public string? DATE { get; set; }
        public string? INTIMEPIC { get; set; }
        public string? OUTTIMETZ { get; set; }
        public string? OUTTIMEREVISED { get; set; }
        public string? OUTTIMEPIC { get; set; }
        public string? INLAT { get; set; }
        public string? LAT { get; set; }
        public decimal LONG { get;set; }
        public string? INLOG { get; set; }
        public string? CorporateAddress { get; set; }
        public decimal OUTLAT { get; set; }
        public decimal OUTLOG { get; set; }
        public string? LOCATIONTYPE { get; set; }
        public decimal RADIALINDISTANCE { get; set; }
        public decimal RADIALOUTDISTANCE { get; set; }
        public TimeZoneInfo UTCTimezone { get; set; }
        public DateTime AR_DATE { get; set; }
        public string? AR_DATE1 { get; set; }
        public string? JOB_STATUS { get; set; }
        public string? CLIENT_ID { get; set; }
        public string? PROJECT_ID { get; set; }
        public string? LOCATION_ID { get; set; }
        public string? DESIG_ID { get; set; }
        public string? usertype { get;set; }
        public string? userSubType { get; set; }
        public int companyID { get; set; }
        public int vendorID { get; set; }
        public string? DEPT_ID { get; set; }

        public string? REPORTING_CORDINATOR_ID { get; set; }

        public string? REPORTING_MANAGER_ID { get; set; }
        public string? PROJECT_HEAD_ID { get; set; }

        public string? APPROVED_CORDINATOR { get; set; }
        public string? APPROVED_MANAGER { get; set; }
        public string? APPROVED_HEAD { get; set; }

        public int MONTHNAME { get; set; }
        public int YEARNAME { get; set; }
        public string? STATUS { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public int ONDUTY { get; set; }

        public string? FIELD_ENGINEER { get; set; }

        public string? SEC_ATTEDANCE { get; set; }
        public string? EMP_NAME { get; set; }
        public string? Emp_Full_Name { get;set; }

        public string? DEVICE { get; set; }

        public string? VER { get; set; }
        public string? BRAND { get; set; }
        public string? DEV_ID { get; set; }

        public string? IP_ADDREESS { get; set; }

        public DateTime OUTTIME_CAPING { get; set; }

        public string? CREATED_BY { get; set; }

        public string? REMARKS { get; set; }

        public string? REMARKS_OUT { get; set; }
        public string? CORDINATOR { get; set; }
        public string? MANAGER { get; set; } 
        public string? HEAD { get; set;}
        public int CountryId { get; set; }





    }

    public class AttendaceResponce
    {
        public int ID { get; set; }
        public string? INTIMETz { get; set; }
        public string? INLAT { get; set; }
        public string? INLOG { get; set; }
        public string? OUTLAT { get; set; }
        public string? OUTLOG { get; set; }
        public string? OUTTIMETz { get; set; }
        public string? EMPID { get; set; }
        public int result { get; set; }
        public string strReturn { get; set; }

    }
}
