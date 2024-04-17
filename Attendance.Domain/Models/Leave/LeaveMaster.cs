using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Leave
{
    public class LeaveMaster
    {
        public int Id { get; set; }

        public int RecordId { get; set; }
        public int UserID { get; set; }
        public int Organizationid { get; set; }
        public string OrganizationType { get; set; }
        public string LEAVE_CODE { get; set; }
        public decimal LEAVE_COUNT { get; set; }
        public string LEAVE_DESC { get; set; }
        public int LEAVE_YEAR { get; set; }
        public int Createdby { get; set; }
        public string createdOn { get; set; }
        public int updatedby { get; set; }
        public int updatedon { get; set; }
        public int STATUS { get; set; }
        public string Remarks { get; set; }
        public decimal MaxleavApply { get; set; }
        public string Document { get; set; }
        public string LeaveCrediteType { get; set; }
        public decimal LeaveCredite { get; set; }
        public int LEAVE_Month { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string LeaveGroupType { get; set; }

    }
}
