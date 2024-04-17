using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Attendance
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        //private readonly string _permission;

        //public AuthorizeActionFilter(string permission)
        //{
        //    _permission = permission;
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = false;
            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        //private bool CheckUserPermission(ClaimsPrincipal user, string permission)
        //{
        //    // Logic for checking the user permission goes here. 

        //    // Let's assume this user has only read permission.
        //    return permission == "Read";
        //}
    }
}
