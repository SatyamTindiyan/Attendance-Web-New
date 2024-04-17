using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Employee
{
    public class EngineerTeamMapping
    {
        public string? ManagerId { get; set; }
        public string? EmpId { get; set; }
        public int CompanyID { get; set; }
        public int VendorID { get; set; }
        public int ProjectID { get; set; }
        public string? Remarks { get; set; }
        public List<EngineerTeamList> ChkSelectAll { get; set; }

    }
    public class EngineerTeamList
    {
        public string? ManagerId { get; set; }
        public string? EmpId { get; set; }
        public int CompanyID { get; set; }
        public int VendorID { get; set; }
        public int ProjectID { get; set; }
        public string? Remarks { get; set; }
        public string? Mapped { get; set; }

    }
}
