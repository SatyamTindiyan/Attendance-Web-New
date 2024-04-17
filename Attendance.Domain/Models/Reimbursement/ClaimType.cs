using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Reimbursement
{
    public class ClaimType
    {
        public int id { get; set; }
        public string PayoutType { get; set; }
        public string Remarks { get; set; }
        public bool status { get; set; }
    }
}
