using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Leave
{
    public class LeaveApprovald
    {
        public int ID { get; set; }
        public string EMPID { get; set; }

        public string APPLYDATE { get; set; }

        public string APPstatusCORDINATOR { get; set; }

        public string APPstatusMANAGER { get; set; }

        public string APPstatusHEAD { get; set; }
    }
}
