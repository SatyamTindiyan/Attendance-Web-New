using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;

namespace Attendance.Controllers.User
{
    public class UserController : BaseController
    {
        public UserController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }
        public abstract class TempDataSerializer { }
        public IActionResult ViewProfile()
        {
            
            string userName = string.Empty;
            //VMUserDetail? vMUserDetail = new VMUserDetail();
            //var value = HttpContext.Session.GetString("LoggedUserInfo");
            //vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            var value = GetUserDetail();   
            if (value != null)
            {
                userName = value.UserName;
            }
           // userName = vMUserDetail.UserName;
            UserProfile user = new UserProfile();
            user = _userService.GetUserDetail(userName);
            ViewBag.user = user;
            //TempData["mydata"] = user;
            return View();



        }
    }
}
