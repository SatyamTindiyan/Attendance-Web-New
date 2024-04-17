using Attendance.ApplicationCore.Interfaces.User;
using Attendance.Domain.Interfaces.User;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Repositories.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Services.User
{
    public class UserService : BaseService , IUserService
    {
       

        public UserService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }
        
        public List<UserProfile> GetUserDetail(string userName, string empid, int DesignationID)
        {
            return _userRepository.GetUserDetail(userName, empid, DesignationID);
        }

        public UserProfile GetUserDetail(string userName)
        {
            UserProfile? lstCor = null;

            try
            {
                lstCor = new UserProfile();
                lstCor = _userRepository.GetUserDetail(userName);

            }
            catch
            {

            }
            return lstCor;
        }
    }
}
