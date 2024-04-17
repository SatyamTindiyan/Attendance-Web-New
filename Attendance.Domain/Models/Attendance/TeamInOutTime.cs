using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class TeamInOutTime
    {
        public int ID { get; set; }

        public int TEAMATTENDANCEID { get; set; }
        public string INTIME { get; set; }
        public string OUTTIME { get; set; }

        public string Empid { get; set; }

        public string TeamEmpid { get; set; }

        public decimal OUTLAT { get; set; }
        public decimal OUTLOG { get; set; }

        public decimal RADIALOUTDISTANCE { get; set; }
    }
}
