using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class AttendanceApproved
    {
        public string EMPID { get; set; }

        public string ARDATE { get; set; }

        public string APPstatusCORDINATOR { get; set; }

        public string APPstatusMANAGER { get; set; }

        public string APPstatusHEAD { get; set; }
    }
}
