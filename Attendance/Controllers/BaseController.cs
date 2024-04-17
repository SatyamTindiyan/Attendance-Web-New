using Attendance.ApplicationCore.Interfaces.Account;
using Attendance.ApplicationCore.Interfaces.Attendance;
using Attendance.ApplicationCore.Interfaces.EngineeringTeamMapping;
using Attendance.ApplicationCore.Interfaces.Holiday;
using Attendance.ApplicationCore.Interfaces.Infra;
using Attendance.ApplicationCore.Interfaces.Leave;
using Attendance.ApplicationCore.Interfaces.Reimbursement;
using Attendance.ApplicationCore.Interfaces.User;
using Attendance.ApplicationCore.Services.User;
using Attendance.Domain.Interfaces.EngineeringTeamMapping;
using Attendance.Domain.Models.UserDetail;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Attendance.Controllers
{
    public class BaseController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        protected readonly IAccountService _accountService;
        protected readonly IUserService _userService;
        protected readonly IAttendanceService _attendanceService;
        protected readonly IEngineeringTeamMappingService _etmservice;
        protected readonly IHolidayService _holiday;
        protected readonly IInfraService _infraservice;
        protected readonly ILeaveService _leaveService;
        protected readonly IReimbursementService _reimbursementservice;
        private readonly IConfiguration _configuration;

        public BaseController(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _accountService = serviceProvider.GetService<IAccountService>();
            _userService = serviceProvider.GetService<IUserService>();
            _attendanceService = serviceProvider.GetService<IAttendanceService>();
            _etmservice = serviceProvider.GetService<IEngineeringTeamMappingService>();
            _holiday = serviceProvider.GetService<IHolidayService>();
            _leaveService = serviceProvider.GetService<ILeaveService>();
            _infraservice = serviceProvider.GetService<IInfraService>();
            _reimbursementservice = serviceProvider.GetService<IReimbursementService>();
            _configuration = configuration;
        }

        public UserDetail GetUserDetail()
        {
            UserDetail vMUserDetail = null;
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    vMUserDetail = new UserDetail();
                    vMUserDetail = JsonConvert.DeserializeObject<UserDetail>(value);
                }
            }
            return vMUserDetail;
        }

        public object GetUserDetail(string ParamName)
        {
            object returnValue = string.Empty;
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    UserDetail vMUserDetail = new UserDetail();
                    vMUserDetail = JsonConvert.DeserializeObject<UserDetail>(value);
                    Type myType = vMUserDetail.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                   // var sss= propsx.GetType.)

                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.Name.ToLower() == ParamName.ToLower())
                            returnValue = prop.GetValue(vMUserDetail, null);
                    }
                }
            }
            return returnValue;
        }

    }
}

namespace Attendance
{
    class CustomAuthenticationFilterAttribute : Attribute
    {
    }

    class CustomExceptionFilterAttribute : Attribute
    {
    }


}