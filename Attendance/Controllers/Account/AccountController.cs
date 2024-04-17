using Attendance.Domain;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Attendance.Infrastructure.loc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Attendance.Controllers.Account
{
    [CustomExceptionFilter]
    public class AccountController : BaseController
    {
        //UserService _user = new UserService();
        //LogHandler log = new LogHandler();
        public AccountController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }


        [CustomAuthenticationFilter]
        public IActionResult Index()
        {
            return View();
        }

        // [AllowAnonymous]
        [HttpGet]
        // [Route("authenticate")]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            string strUserToken = Security.GetOneWayEncryptMD5("PASSWORD");
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            //HttpContext.MySession["empid"] = null;
            if (value == null)
            {
                return View(vMUserDetail);
            }
            return View(vMUserDetail);


        }

        [HttpPost]

        public ActionResult Login(VMUserDetail vMUserDetail)
        {
            try
            {
                string Rststus = "";
                if (vMUserDetail != null)
                {
                    string encryptPassword = string.Empty;
                    UserDetail userdt = null;

                    if (!string.IsNullOrEmpty(vMUserDetail.Password))
                    {
                        //encryptPassword = Security.GetEncryptString(userDetail.Password.Trim());
                        encryptPassword = Security.GetOneWayEncryptMD5(vMUserDetail.Password.Trim());
                    }

                    if (!string.IsNullOrEmpty(vMUserDetail.UserName) && !string.IsNullOrEmpty(encryptPassword))
                    {
                        userdt = _accountService.checkUserExist(vMUserDetail.UserName, encryptPassword, out Rststus);

                        //string _userDetail = Security.GetEncryptString(vMUserDetail.UserName.Trim() + "|" + encryptPassword.Trim());

                        if (userdt != null)
                        {
                            HttpContext.Session.SetString("LoggedUserInfo", JsonConvert.SerializeObject(userdt));
                            var value = HttpContext.Session.GetString("LoggedUserInfo");
                            string _userDetail = Security.GetEncryptString(userdt.UserName.Trim() + "|" + encryptPassword.Trim());
                            //userdt = GetUserDetail();
                            if (value != null)
                            {

                                TempData["mydata"] = value;
                                ViewBag.mydata = value;
                            }

                            //MySession.UserName = userdt.UserName;
                            //// DateTime myDate = MySession.Current.MyDate;
                            //// MySession.Current.MyDate = DateTime.Now;
                            //MySession.comid = userdt.CompanyId;
                            //MySession.Countryid = userdt.CountryId;
                            //MySession.UserType = userdt.UserType;
                            //MySession.id = userdt.Id;
                            //MySession.UserSubType = userdt.UserSubType;
                            //MySession.Approvallevel = userdt.Approvallevel;
                            //MySession.EmpId = userdt.EMP_ID;
                            //HttpContext.Session.GetString("MYVALUE");
                            //ViewBag.MyValue = HttpContext.Session.GetString("MYVALUE");
                            ViewBag.user = userdt;

                            //var str = this.GetUserDetail("usertype");

                            return RedirectToAction("Index", "Home");

                            
                        }
                        else
                            ViewBag.ReturnMessage = Rststus; // "Some error occured.please contact to admin.";
                    }
                    else
                    {
                        ViewBag.ReturnMessage = "You enter wrong Username or email";
                    }
                }
                else
                {
                    ViewBag.ReturnMessage = "1";
                }
            }
            catch (Exception)
            {


            }
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public ActionResult Login(VMUserDetail userDetail)
        //{
        //    try
        //    {
        //        string Rstatus = "";
        //        if (userDetail != null)
        //        {
        //            string encryptPassword = string.Empty;
        //            VMUserDetail vMUserDetail = null;
        //            if (!string.IsNullOrEmpty(userDetail.Password))
        //            {
        //                encryptPassword = Security.GetOneWayEncryptMD5(userDetail.Password.Trim());
        //            }

        //            if (!string.IsNullOrEmpty(userDetail.UserName) && !string.IsNullOrEmpty(encryptPassword))
        //            {
        //                vMUserDetail = _accountService.checkUserExist(userDetail.UserName, encryptPassword, out Rstatus);

        //                //user = _user.GetUserDetail().ToList();
        //                if (vMUserDetail != null)
        //                {
        //                    MySession mySession = new MySession();
        //                    MySession.UserName = vMUserDetail.UserName;
        //                    // DateTime myDate = MySession.Current.MyDate;
        //                    // MySession.Current.MyDate = DateTime.Now;
        //                    MySession.comid = vMUserDetail.CompanyId;
        //                    MySession.Countryid = vMUserDetail.CountryId;
        //                    MySession.UserType = vMUserDetail.UserType;
        //                    MySession.id = vMUserDetail.Id;
        //                    MySession.UserSubType = vMUserDetail.UserSubType;

        //                    //HttpContext.Session["LoggedUserInfo"] = vMUserDetail;
        //                    HttpContext.Session.SetString("LoggedUserInfo", JsonConvert.SerializeObject(vMUserDetail));
        //                    HttpContext.Session.SetString("MYVALUE", JsonConvert.SerializeObject(vMUserDetail.Approvallevel));
        //                    ViewBag.MyValue = HttpContext.Session.GetString("MYVALUE");
        //                    ViewBag.user = vMUserDetail;
        //                    TempData["mydata"] = vMUserDetail;
        //                    MySession.Approvallevel = vMUserDetail.Approvallevel;
        //                    MySession.EmpId = vMUserDetail.EMP_ID;


        //                    return RedirectToAction("Index", "Home");
        //                }
        //                else
        //                    ViewBag.ReturnMessage = Rstatus; // "Some error occured.please contact to admin.";
        //            }
        //            else
        //            {
        //                ViewBag.ReturnMessage = "You enter wrong Username or email";
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.ReturnMessage = "1";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();

        //}

        [HttpGet]
        public IActionResult adduser()
        {

            return View();


        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }


        [HttpGet]
        public IActionResult Home()
        {
            UserDetail vMUserDetail = new UserDetail();
            var value = HttpContext.Session.GetString("loginSession");
            vMUserDetail = JsonConvert.DeserializeObject<UserDetail>(value);
            if (vMUserDetail != null)
            {
                string usertype = vMUserDetail.UserType.ToLower().Trim();
                string userSubtype = vMUserDetail.UserSubType.ToLower().Trim();

                return RedirectToAction("Index", "Home");
                //if (usertype.ToLower() == "superadmin" && userSubtype.ToLower() == "employee")
                //    return RedirectToAction("Dashboard", "Vendor");//--6-12-2021

            }

            else
                return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult LoginModule(string param, int moduleid = 0)
        {
            string _userDetail = string.Empty;
            string[] IdPass = null;
            string Rstatus = "";
            if (!string.IsNullOrEmpty(param))
            {
                UserDetail vMUserDetail = new UserDetail();
                //var value = HttpContext.Session.GetString("loginSession");
                //vMUserDetail = JsonConvert.DeserializeObject<UserDetail>(value);
                _userDetail = Security.GetDecryptString(param);
                // UserId = _userDetail.Split("|");
                IdPass = _userDetail.Split(new char[] { '|' });
                string _Id = IdPass[0];
                string encryptPassword = IdPass[1];
                if (!string.IsNullOrEmpty(_Id) && !string.IsNullOrEmpty(encryptPassword))
                {
                    vMUserDetail = _accountService.checkUserExist(_Id,encryptPassword, out Rstatus);
                    if (vMUserDetail != null)
                    {
                        vMUserDetail.urlQueryString = param;
                        vMUserDetail.ModuleId = moduleid;
                        //Session["LoggedUserInfo"] = vMUserDetail;
                        HttpContext.Session.SetString("loginSession", JsonConvert.SerializeObject(vMUserDetail));
                        //var valuesession = HttpContext.Session.GetString("loginSession");

                        var str=this.GetUserDetail("usertype");

                        return RedirectToAction("Home", "Account");
                    }
                    else
                        ViewBag.ReturnMessage = Rstatus; // "Some error occured.please contact to admin.";
                }
                else
                {
                    ViewBag.ReturnMessage = "You enter wrong Username or email";
                }
                //return RedirectToAction("Home", "Account");

            }
            else
            {
                // return RedirectToAction("Module", "Account",
                return RedirectToAction("Login", "Account");
            }

            return View();

        }





    }
}
