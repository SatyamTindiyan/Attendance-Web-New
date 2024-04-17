using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class AttendanceSummary
    {
        public string EMPID { get; set; }

        public string? Emp_Full_Name { get; set; }

        public int CalendarDays { get; set; }

        public int Present { get; set; }

        public int Leave { get; set; }

        public int Absent { get; set; }


        public int TotalDays { get; set; }

        public int RemainingDays { get; set; }

        public int WeekOff { get; set; }
        public int SL { get; set; }
        public int CL { get; set; }
        public int PL { get; set; }
        public int PublicHolidays { get; set; }
        public int PresentDays { get; set; }


    }
}
