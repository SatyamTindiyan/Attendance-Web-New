using Attendance.Domain.Interfaces.Account;
using Attendance.Domain.Interfaces.User;
using Attendance.Domain.Models.Account;
using Attendance.Domain.Models.UserDetail;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data.Repositories.User
{
    public class UserRepository : BaseRepository , IUserRepository
    {
        private SqlConnection _conn;
        //LogHandler log = null;
        string errorMethodRoute = "";

        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<UserProfile> GetUserDetail(string userName, string empid, int DesignationID)
        {
            List<UserProfile> users = new List<UserProfile>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("UserName", userName);
                _conn = GetConnection();
                OpenConnection(_conn);
                users = _conn.Query<UserProfile>("Sp_EmpInfo", param, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, nameof(GetUserDetail));

            }
            return users;
        }

        public UserProfile GetUserDetail(string userName)
        {

            UserProfile? userlist = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("UserName", userName);
                _conn = GetConnection();
                OpenConnection(_conn);
                userlist = _conn.Query<UserProfile>("Sp_EmpInfo", param, commandType: CommandType.StoredProcedure).FirstOrDefault();

                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
               
                WriteErrorLog(ex, errorMethodRoute, "GetUserDetail");
            }
            return userlist;
        }
    }
}
