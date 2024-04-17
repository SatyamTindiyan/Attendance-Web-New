using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class TeamAttendanceRemote
    {
        public int ID { get; set; }
        public string EMPTYPE { get; set; }
        public int VENDORID { get; set; }

        public int SUPPLIERID { get; set; }
        public string EMPID { get; set; }
        public string EMPNAME { get; set; }
        public string LOGINID { get; set; }
        public DateTime AR_DATE { get; set; }

        public string INTIME { get; set; }
        public string DATE { get; set; }

        public string OUTTIME { get; set; }

        public decimal INLAT { get; set; }
        public decimal INLOG { get; set; }
        public decimal OUTLAT { get; set; }
        public decimal OUTLOG { get; set; }
        public decimal RADIALINDISTANCE { get; set; }
        public decimal RADIALOUTDISTANCE { get; set; }

        public DateTime ARDATE { get; set; }

        public TimeZoneInfo UTCTimezone { get; set; }
        public string APPROVED_CORDINATOR { get; set; }
        public string APPROVED_MANAGER { get; set; }
        public string APPROVED_HEAD { get; set; }

        public int MONTHNAME { get; set; }
        public int YEARNAME { get; set; }
        public string STATUS { get; set; }

        public int ONDUTY { get; set; }

        public string DEVICE { get; set; }

        public string VER { get; set; }
        public string BRAND { get; set; }
        public string DEV_ID { get; set; }

        public string IP_ADDREESS { get; set; }

        public string CREATED_BY { get; set; }
    }
}
