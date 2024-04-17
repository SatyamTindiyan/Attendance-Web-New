using Attendance.Domain.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.User
{
    public interface IUserRepository
    {
        List<UserProfile> GetUserDetail(string userName, string empid, int DesignationID);
        UserProfile GetUserDetail(string userName);
        //UserProfile GetUserDetail(string userName);
    }
}
