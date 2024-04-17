using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Reimbursement
{
    public class ClaimStatus
    {
        public int Id { get; set; }
        public string ClaimStatuss { get; set; }
        public bool Status { get; set; }

    }
}
