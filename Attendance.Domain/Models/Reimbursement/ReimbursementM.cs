using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Reimbursement
{
    public class ReimbursementM
    {
        public int ClaimType { get; set; }
        public int ClaimTypeSub { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmt { get; set; }
        public string KMIN { get; set; }
        public string KMOUT { get; set; }
        public string Remarks { get; set; }
        public int ClaimStatus { get; set; }
        public string MRemarks { get; set; }

        public string FileName { get; set; }

    }
}
