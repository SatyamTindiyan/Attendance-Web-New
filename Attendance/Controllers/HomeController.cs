using Attendance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Attendance.ApplicationCore.Services.User;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Attendance.Infrastructure.Data.Repositories.User;
using Newtonsoft.Json;
using Attendance.Domain.Interfaces.User;

namespace Attendance.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        //UserService _user = new UserService();

        
        public HomeController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            //VMUserDetail userMaster = new VMUserDetail();
            //UserDetail vMUserDetail = new UserDetail ();
            //var value = HttpContext.Session.GetString("LoggedUserInfo");
            //List<UserProfile> lst = new List<UserProfile>();
            //userMaster = JsonConvert.DeserializeObject<VMUserDetail>(value);
            var value = GetUserDetail();

            if (value != null)
            {
                string userName = value.UserName;
                UserProfile user = new UserProfile();
                UserProfile userProfile = new UserProfile();
                user = _userService.GetUserDetail(userName);
                MySession.EmpId = user.EMP_ID;

                if (user != null)
                {
                    string userProfileJson = JsonConvert.SerializeObject(user);
                    TempData["userprofile"] = userProfileJson;
                    TempData.Keep("userprofile");
                }
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}