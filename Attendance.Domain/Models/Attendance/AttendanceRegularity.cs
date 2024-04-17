using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class AttendanceRegularity
    {
        public int ID { get; set; }

        public string EMP_ID { get; set; }

        public DateTime A_DATE { get; set; }

        public DateTime INTIME { get; set; }
        public DateTime OUTTIME { get; set; }
        public string REMARKS { get; set; }

        public bool STATUS { get; set; }
        public DateTime C_DATE { get; set; }

        public string APPROVED_CORDINATOR { get; set; }
        public string APPROVED_MANAGER { get; set; }
        public string APPROVED_HEAD { get; set; }

        public string DEVICE { get; set; }
    }
}
