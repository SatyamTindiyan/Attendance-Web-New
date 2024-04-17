using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Account
{
    public partial class UserLoginMaster
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserToken { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int CompanyId { get; set; }

        public int RegionId { get; set; }
        public string UserType { get; set; }
        public string UserSubType { get; set; }
        public string LoggedInName { get; set; }
        public string urlQueryString { get; set; }
    }
}
