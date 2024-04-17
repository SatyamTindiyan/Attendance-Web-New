using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Reimbursement
{
    public class SubClaimType
    {
        public int ID { get; set; }
        public int PayoutTypeID { get; set; }
        public string PayoutSubType { get; set; }
        public bool status { get; set; }
    }
}
