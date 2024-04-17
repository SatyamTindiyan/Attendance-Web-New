using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance.ViewModel
{
    public class MarkAttendanceVM
    {
        public string LOGINID { get; set; }
        public string EMPID { get; set; }
        public DateTime INTIME { get; set; }
        public float INLAT { get; set; }

        public float INLOG { get; set; }

        public float RADIALINDISTANCE { get; set; }

        public DateTime ARDATE { get; set; }

        public int MONTHNAME { get; set; }

        public int YEARNAME { get; set; }

        public int STATUS { get; set; }
        public int ONDUTY { get; set; }

        public string REPORTING_CORDINATOR_ID { get; set; }

        public string REPORTING_MANAGER_ID { get; set; }

        public string PROJECT_HEAD_ID { get; set; }

        public string BRAND { get; set; }

        public string DEV_ID { get; set; }

        public string DEVICE { get; set; }

        public string VER { get; set; }

        public string IP_ADDREESS { get; set; }

        public int CREATEDBY { get; set; }

    }
}
