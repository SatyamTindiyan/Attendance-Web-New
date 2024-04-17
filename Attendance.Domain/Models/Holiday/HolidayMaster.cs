using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Holiday
{
    public class HolidayMaster
    {
        //public int ID { get; set; }
        public int id { get; set; }
        public string EMPID { get; set; }
        public int Organizationid { get; set; }
        public int stateId { get; set; }
        public string state { get; set; }
        public int countryID { get; set; }
        public string country { get; set; }
        public string OrganizationType { get; set; }

        public string HOLIDAY_DATE { get; set; }

        public string WEEKDAYS { get; set; }
        public string HOLIDAY_NAME { get; set; }
        public string Holidaytype { get; set; }
        public string WEEKDAY { get; set; }

        public string HOLIDAY_DESC { get; set; }

        public string HC_START_DATE { get; set; }

        public string HC_VALID_DATE { get; set; }

        public string HC_STATUS { get; set; }

        public string REMARKS { get; set; }

        public string Createdby { get; set; }

        public string createdOn { get; set; }

        public string updatedby { get; set; }

        public string updatedon { get; set; }

        public string STATUS { get; set; }
        //public string Country { get; set; }

        //public int CountryId { get; set; }
        public string Company { get; set; }
        public string Calender_type { get; set; }
        public string EncryptstateId { get; set; }
        public string ExceljsonData { get; set; }
        public string DtExcelData { get; set; }
        public object RecordId { get; set; }

        public string usertype { get; set; }
        public string usersubtype { get; set; }
        public int userid { get; set; }
    }


    public class HoilidayType
    {
        public int id { get; set; }

        public string Holidaytype { get; set; }
        public string Encryptid { get; set; }
        public string HOLIDAY_DESC { get; set; }
        public string HOLIDAY_NAME { get; set; }
    }


}
