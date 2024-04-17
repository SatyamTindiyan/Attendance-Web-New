using Attendance.Domain.Models.UserDetail;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Mvc.Filters;
using System.Web.Mvc;

namespace Attendance
{
    public sealed class CustomAuthorizeAttribute : Attribute, IAuthenticationFilter
    {

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //if ((filterContext.HttpContext.Session["LoggedUserInfo"] != null))
            //{
            //    //dtuser = new DataTable();
            //    //dtuser = (DataTable)filterContext.HttpContext.Session["LoggedUserInfo"];
            //    //vMUserDetail = new VMUserDetail();
            //    var ss = (VMUserDetail)filterContext.HttpContext.Session["LoggedUserInfo"];
            //}
            //else
            //{
            //    if (this.IsAnonymousAction(filterContext))
            //    {
            //        return;
            //    }
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Redirecting the user to the Login View of Account Controller when fail  
                //filterContext.Result = new RedirectToRouteResult(
                //new RouteValueDictionary
                //{
                //     { "controller", "Account" },
                //     { "action", "Login" }
                //});
                //ExceptionLogger logger = new ExceptionLogger()
                //{
                //    ExceptionMessage = "Your session out. Please login again",
                //};
                ViewResult view = new ViewResult();
                view.ViewName = "../Home/Error";
                view.ViewData = new ViewDataDictionary();
                //view.ViewData.Model = logger;

                //filterContext.ExceptionHandled = true;
                view.ExecuteResult(filterContext);




            }
            else
            {
                //if (vMUserDetail != null)
                //{
                //    string strUsertype = vMUserDetail.UserType;
                //    if (strUsertype.ToLower() == "company")
                //    {
                //        filterContext.Result = new RedirectToRouteResult(
                //                               new RouteValueDictionary
                //                               {
                //                                    { "controller", "Company" },
                //                                    { "action", "ViewCompany" }
                //                               });
                //    }
                //}

                //if (dtuser != null)
                //{
                //    if (dtuser.Rows.Count > 0)
                //    {
                //        // here you can set redirection according to user role.
                //    }
                //}
            }

        }

        private bool IsAnonymousAction(AuthenticationContext filterContext)
        {
            return filterContext.ActionDescriptor
                                 .GetCustomAttributes(inherit: true)
                                 .OfType<AllowAnonymousAttribute>()
                                 //or any attr. you want
                                 .Any();
        }
        //public void OnAuthorization(AuthorizationFilterContext context)
        //{

        //    if (context != null)
        //    {
        //        // Auth logic



        //    }
        //}
    }
}
