using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class EmpRegularApproval : DateSearch
    {
        public string EMPID { get; set; }

        public string Emp_Full_Name { get; set; }

        public string DATE { get; set; }

        public string INTIME { get; set; }

        public string OUTTIME { get; set; }

        public int APPstatusCORDINATOR { get; set; }
        public int APPstatusMANAGER { get; set; }

        public int APPstatusHEAD { get; set; }

        public string WorkingHour { get; set; }

        public string DisableOption { get; set; }


    }
}
