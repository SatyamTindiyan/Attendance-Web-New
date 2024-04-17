using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance.ViewModel
{
    public class AttendanceRemoteVM
    {
        public string EMPID { get; set; }
        public string Emp_Full_Name { get; set; }
        public string INTIME { get; set; }
        public string OUTTIME { get; set; }

        public decimal INLAT { get; set; }

        public decimal INLOG { get; set; }

        public decimal OUTLAT { get; set; }
        public decimal OUTLOG { get; set; }
        public string REPORTING_CORDINATOR_ID { get; set; }

        public string REPORTING_MANAGER_ID { get; set; }
        public string PROJECT_HEAD_ID { get; set; }

        public string STATUS { get; set; }

        public decimal INDISTAMCE { get; set; }
        public decimal OUTDISTAMCE { get; set; }
        public string HOURS { get; set; }


        public int MONTHNAME { get; set; }
        public int YEARNAME { get; set; }

        public string IP_ADDRESS { get; set; }
    }
}
