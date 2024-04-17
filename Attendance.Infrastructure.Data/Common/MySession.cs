using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Attendance.Infrastructure.Data.Common
{
    [Serializable]
    public class MySession: ActionFilterAttribute
    {
        // private constructor
        public MySession()
        {
            //EMPID = "default value";
        }
       
        public static string UserName { get; set; }
        public static string EmpId { get; set; }
        public static int id { get;set; }
        public static int comid { get; set; }
        public static int Countryid { get; set; }
        public static int stateId { get; set; }
        public static string UserType { get; set; }
        public static string UserSubType { get; set; }
        public static int Approvallevel { get; set; }
        public static int CompanyId { get; set; }
        public static int CountryId { get; set; }

        public static int companyId
        {
            get;
            set;
        }
    }
}
