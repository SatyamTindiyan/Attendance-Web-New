using Attendance.Domain.Models.Account;
using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.Account
{
    public interface IAccountRepository
    {
        UserLoginMaster AuthenticateUser(string username, string password);
        UserDetail checkUserExist(string userName, string userPassword, out string statustxt);
        List<UserDetail> GetAllCountry(int? regionId, string userType, string userSubType, int userId, int companyId);
       
    }
}
