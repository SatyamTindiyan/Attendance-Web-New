using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Attendance
{
    public class VendorType
    {
        public int ID { get; set; }

        public int VendorID { get; set; }

        //public int SUPPLIERID { get; set; }

        public string? Vendor { get; set; }

        //public string? SUPPLIER { get; set; }

        public string? EMPNAME { get; set; }

        public string? INTIMETZ { get; set; }

        public string? OUTTIMETZ { get; set; }

        public string? INTIME { get; set; }

        public string? OUTTIME { get; set; }

        public string? LOGINID { get; set; }

        public string? EMPID { get; set; }

        public string? EMPTYPE { get; set; }

        public string? Emp_Full_Name { get; set; }

        public int SupplierId { get; set; }
        public string? Supplier { get; set; }
    }
}
