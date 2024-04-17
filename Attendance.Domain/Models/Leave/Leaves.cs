using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Leave
{
    public class Leaves
    {
        public int ID { get; set; }
        public string EMP_ID { get; set; }

        public string LEAVE_FROM_DATE { get; set; }

        public string LEAVE_TO_DATE { get; set; }

        public int LEAVE_COUNT { get; set; }

        public string LEAVE_CODE { get; set; }
        public string LEAVE_TYPE { get; set; }
        public string LEAVE_DESC { get; set; }

        public int LEAVE_MONTH { get; set; }

        public int LEAVE_YEAR { get; set; }

        public string REASONS { get; set; }

        public int CREATEDBY { get; set; }

        public int UPDATEDBY { get; set; }
        public int ALTMOBILE { get; set; }
        public string REMARKS { get; set; }
    }
}
