using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class AttenDetail
    {
        public string? EMPID { get; set; }

        public string? Emp_Full_Name { get; set; }

        public string? DATE { get; set; }

        public string? thedate { get; set; }

        public string? INTIMEDB { get; set; }

        public string? Country { get; set; }
        public string? OUTTIMEDB { get; set; }

        public decimal RADIALINDISTANCE { get; set; }

        public decimal RADIALOUTDISTANCE { get; set; }

        public string? CORDINATOR { get; set; }

        public string? MANAGER { get; set; }

        public string? HEAD { get; set; }
    }
}
