﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance.ViewModel
{
    public class AttenHistoryVM
    {
        public string EMPID { get; set; }

        public string Emp_Full_Name { get; set; }

        public string DATE { get; set; }

        public string INTIMEDB { get; set; }

        public string OUTTIMEDB { get; set; }
        public string StatusInTime { get; set; }
        public string StatusOutTime { get; set; }

        public string thedate { get; set; }

        public decimal RADIALINDISTANCE { get; set; }

        public decimal RADIALOUTDISTANCE { get; set; }

        public string CORDINATOR { get; set; }

        public string MANAGER { get; set; }

        public string HEAD { get; set; }


        public string AIntime { get; set; }
        public string AOuttime { get; set; }

        public string RIntime { get; set; }
        public string ROuttime { get; set; }

        public int ONDUTY { get; set; }

        public string APPstatusHEAD { get; set; }
        public int timediffrence { get; set; }
        public string ShiftIntime { get; set; }
        public string Shiftouttime { get; set; }

      // public string ShiftINtime { get; set; }
        public int Workhours { get; set; }
        public string ARDATE { get; set; }
        public int CountryId { get; set; }
    }
}
