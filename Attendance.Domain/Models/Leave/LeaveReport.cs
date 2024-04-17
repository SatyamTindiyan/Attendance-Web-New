using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Leave
{
    public class LeaveReport
    {
        public string EMP_ID { get; set; }
        public string Emp_Full_Name { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string LEAVE_CODE { get; set; }
        public int CompanyId { get; set; }
        public int LEAVE_COUNT { get; set; }
        public string CORDINATOR { get; set; }
        public string MANAGER { get; set; }
        public string HEAD { get; set; }

        public int Month { get; set; }

        public int year { get; set; }
        public string REASONS { get; set; }
        public string LeaveType { get; set; }
        public string APPLYDATE { get; set; }

        public int Countryid { get; set; }

        public int Depid { get; set; }


    }
}
