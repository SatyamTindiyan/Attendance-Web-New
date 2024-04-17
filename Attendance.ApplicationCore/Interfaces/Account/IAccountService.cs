using Attendance.Domain.Models.Account;
using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Interfaces.Account
{
    public interface IAccountService
    {
        //UserLoginMaster AuthenticateUser(string username, string password);
        //VMUserDetail checkUserExist(string _Id, string userName, string encryptPassword, out string rstatus);
        UserDetail checkUserExist(string userName, string encryptPassword, out string rstatus);
        List<UserDetail> GetCountryList(int? regionId, string userType, string userSubType, int userId, int companyId);

       
    }
}
