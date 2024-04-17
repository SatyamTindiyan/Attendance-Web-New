using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance.ViewModel
{
    public class AttenSummaryVM
    {
        public string EMPID { get; set; }

        public string Emp_full_Name { get; set; }

        public int CaledarDays { get; set; }

        public int Present { get; set; }

        public int Leave { get; set; }

        public string Absent { get; set; }
    }
}
