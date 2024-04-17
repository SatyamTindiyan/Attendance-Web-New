using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Holiday
{
    public class HolidayTypeSave
    {
        public string? EMPID { get; set; }
        public int Id { get; set; }
        public string userid { get; set; }
        public string? usertype { get; set; }
        public string? usersubtype { get; set; }
        public int Organizationid { get; set; }
        public string? OrganizationType { get; set; }
        public string? HOLIDAY_DATE { get; set; }
        public string? HOLIDAY_NAME { get; set; }
        public string? WEEKDAY { get; set; }
        public string? HOLIDAY_TYPE { get; set; }
        public string? HOLIDAY_DESC { get; set; }
        public DateTime? HC_START_DATE { get; set; }
        public DateTime? HC_VALID_DATE { get; set; }
        public string? HC_STATUS { get; set; }
        public string? REMARKS { get; set; }
        public int Createdby { get; set; }
        public DateTime? createdOn { get; set; }
        public int updatedby { get; set; }
        public DateTime? updatedon { get; set; }
        public bool STATUS { get; set; }
        public int CountryId { get; set; }
        public int RecordId { get; set; }
        public object DtExcelData { get; set; }
        public int stateId { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string countryid { get; set; }
    }

    public class HolidayTypemaster
    {
        public string? EMPID { get; set; }
        public int Id { get; set; }
        public string userid { get; set; }
        public string? usertype { get; set; }
        public string? usersubtype { get; set; }
        public int Organizationid { get; set; }
        public string? OrganizationType { get; set; }
        public string? HOLIDAY_DATE { get; set; }
        public string? HOLIDAY_NAME { get; set; }
        public string? WEEKDAY { get; set; }
        public string? HOLIDAY_TYPE { get; set; }
        public string? HOLIDAY_DESC { get; set; }
        public DateTime? HC_START_DATE { get; set; }
        public DateTime? HC_VALID_DATE { get; set; }
        public string? HC_STATUS { get; set; }
        public string? REMARKS { get; set; }
        public int Createdby { get; set; }
        public DateTime? createdOn { get; set; }
        public int updatedby { get; set; }
        public DateTime? updatedon { get; set; }
        public bool STATUS { get; set; }
        public int CountryId { get; set; }
        public int RecordId { get; set; }
        public object DtExcelData { get; set; }
        public int stateId { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

    }

    public class HolidayDetails
    {
        public string? HOLIDAY_DATE { get; set; }
        public string? WEEKDAY { get; set; }
        public string? HOLIDAY_NAME { get; set; }
        public string? Country { get; set; }
    }
}
