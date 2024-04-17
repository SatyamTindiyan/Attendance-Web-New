using Attendance.ApplicationCore.Interfaces.Account;
using Attendance.Domain.Models.Account;
using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.UserDetail;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Services.Account
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public UserLoginMaster AuthenticateUser(string username, string password)
        {
            return _accountRepository.AuthenticateUser(username, password);
        }

        public UserDetail checkUserExist( string userName, string userPassword, out string Rtxt)
        {
            string Statustxt = "";
            Rtxt = "";
            VMUserDetail vMUserDetail = null;
            UserDetail userDetail1 = null;
            try
            {
                userDetail1 = new UserDetail();
                userDetail1 = _accountRepository.checkUserExist(userName, userPassword, out Statustxt);
                Rtxt = Statustxt;
                if(userDetail1 != null)
                {
                    vMUserDetail = new VMUserDetail();
                    vMUserDetail.Id= userDetail1.Id;
                    vMUserDetail.UserName=userDetail1.UserName;
                    vMUserDetail.UserType=userDetail1.UserType;
                    vMUserDetail.UserSubType=userDetail1.UserSubType;
                    vMUserDetail.CountryId=userDetail1.CountryId;
                    vMUserDetail.Country=userDetail1.Country;
                    vMUserDetail.CompanyId=userDetail1.CompanyId;
                    vMUserDetail.RegionId=userDetail1.RegionId;
                    vMUserDetail.Approvallevel = userDetail1.Approvallevel;
                    vMUserDetail.EMP_ID = userDetail1.EMP_ID;

                    
                }
                
            }
            
            catch
            {

            }

            return userDetail1;
        }

        public List<UserDetail> GetCountryList(int? regionId, string UserType, string UserSubType, int UserId, int CompanyId)
        {
            return _accountRepository.GetAllCountry(regionId, UserType, UserSubType, UserId, CompanyId);
        }

       
    }
}
