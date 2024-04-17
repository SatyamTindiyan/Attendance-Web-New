using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.UserDetail
{
    public class AttenSummary
    {
        public int id { get; set; }
        public int Id { get; set; }
        
        public string UserName { get; set; }

        public string EmpId { get; set; }

        
        public string Password { get; set; }
        public string UserToken { get; set; }

        public int DesignationID { get; set; }
        public int CountryId { get; set; }
        public int comid { get; set; }
        public string Country { get; set; }
        public int CompanyId { get; set; }

        public int RegionId { get; set; }
        public string UserType { get; set; }
        public string UserSubType { get; set; }

        public int Approvallevel { get; set; }
        public string EMP_ID { get; set; }

    }
}
