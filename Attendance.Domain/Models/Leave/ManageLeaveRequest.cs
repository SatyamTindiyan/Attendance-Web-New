using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Leave
{
    public class ManageLeaveRequest
    {
        public int ID { get; set; }
        public string EMP_ID { get; set; }
        public string Emp_Full_Name { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string TODATE { get; set; }
        public string FROMDATE { get; set; }
        public string LeaveType { get; set; }
        public string LEAVE_CODE { get; set; }
        public int LEAVE_COUNT { get; set; }
        public string CORDINATOR { get; set; }
        public string MANAGER { get; set; }
        public string HEAD { get; set; }
        public int APPstatusCORDINATOR { get; set; }
        public int APPstatusMANAGER { get; set; }
        public int APPstatusHEAD { get; set; }

        public int Month { get; set; }

        public string year { get; set; }
        public string REASONS { get; set; }
        //public string LeaveType { get; set; }
        public string APPLYDATE { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }

        public int Leavecount { get; set; }
        public string FROM { get; set; }
        //public string TODATE { get; set; }
        public string DisableOption { get; set; }
    }
    public class Companylist
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
    }
    public class Departmentlist
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; }

    }
    public class Empployeelist
    {
        public string EmpId { get; set; }
        public string Empname { get; set; }
    }
}
