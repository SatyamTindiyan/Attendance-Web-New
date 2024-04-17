using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Leave
{
    public class LeaveStatus
    {
        public string EMPID { get; set; }

        public int SL { get; set; }
        public int CL { get; set; }
        public int PL { get; set; }

        public int RSL { get; set; }
        public int RPL { get; set; }
        public int RCL { get; set; }

        public int USL { get; set; }
        public int UCL { get; set; }
        public int UPL { get; set; }

        public int TU { get; set; }

        public int TR { get; set; }
        public int TotalLeaves { get; set; }
    }
    public class VMLeaveStatus
    {
        public string EMPID { get; set; }

        public int SL { get; set; }
        public int CL { get; set; }
        public int PL { get; set; }

        public int RSL { get; set; }
        public int RPL { get; set; }
        public int RCL { get; set; }

        public int TOTALLEAVE { get; set; }
    }
}
