using Attendance.Domain.Extensions;
using Attendance.Domain.Helper;
using Attendance.Domain.Interfaces.Account;
using Attendance.Domain.Models.Account;
using Attendance.Domain.Models.Attendance;
using Attendance.Domain.Models.UserDetail;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Attendance.Infrastructure.Data.Repositories.Account
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private SqlConnection _conn;

        private string errorMethodRoute = "ANTS.Infrastructure.Data.Repositories.Account.AccountRepository";

        public AccountRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public UserLoginMaster AuthenticateUser(string userName, string userPassword)
        {
            UserLoginMaster userDetail = (UserLoginMaster)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("UserName", (object)userName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Password", (object)userPassword, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = GetConnection();
                OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                SqlMapper.GridReader gridReader = SqlMapper.QueryMultiple((IDbConnection)conn, StoredProcedures.AuthenticateUser.ToDescription(), (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3);
                string? str = gridReader.Read<string>(true).FirstOrDefault<string>();
                if (str == "Success")
                    userDetail = gridReader.Read<UserLoginMaster>(true).FirstOrDefault<UserLoginMaster>();
                CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, this.errorMethodRoute, nameof(AuthenticateUser));
            }
            return userDetail;
        }

        public UserDetail checkUserExist(string userName, string userPassword, out string Statustxt)
        {
            Statustxt = "";
            UserDetail userDetail = null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("UserName",(object)userName,new DbType?(),new ParameterDirection?(),new int?(),new byte?(),new byte?());
                dynamicParameters1.Add("Password",(object)userPassword,new DbType?(), new ParameterDirection?(),new int?(), new byte?(),new byte?());
                _conn = GetConnection();
                OpenConnection(this._conn);
                SqlMapper.GridReader gridReader = SqlMapper.QueryMultiple((IDbConnection)_conn, "OMMS_CheckUserExistBasedOnUserNamePassword", (object)dynamicParameters1, commandType: CommandType.StoredProcedure);
                string str = gridReader.Read<string>(true).FirstOrDefault<string>();
                Statustxt = str;
                if (str == "Success")
                    userDetail = gridReader.Read<UserDetail>(true).FirstOrDefault<UserDetail>();
                CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, nameof(checkUserExist));
            }
            return userDetail;
        }

        public List<UserDetail> GetAllCountry(int? regionId, string UserType, string UserSubType, int UserId, int CompanyId = 0)
        {
            List<UserDetail> allCountry = (List<UserDetail>)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("RegionId", (object)regionId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CountryID", (object)CompanyId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                _conn = GetConnection();
                OpenConnection(_conn);
                allCountry = _conn.Query<UserDetail>(StoredProcedures.Sp_EmpInfo.ToDescription(), dynamicParameters1, commandType: CommandType.StoredProcedure).ToList();
                CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex, errorMethodRoute, nameof(GetAllCountry));
            }
            return allCountry;
        }
        
    }
}
