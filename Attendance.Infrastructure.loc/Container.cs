using Attendance.ApplicationCore.Interfaces.Account;
using Attendance.ApplicationCore.Interfaces.Attendance;
using Attendance.ApplicationCore.Interfaces.User;
using Attendance.ApplicationCore.Services.Account;
using Attendance.ApplicationCore.Services.User;
using Attendance.Domain.Interfaces.Account;
using Attendance.Domain.Interfaces.Attendance;
using Attendance.Domain.Interfaces.User;
using Attendance.Infrastructure.Data.Repositories.Account;
using Attendance.Infrastructure.Data.Repositories.User;
using Attendance.Infrastructure.Data.Repositories.Attendance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attendance.ApplicationCore.Services.Attendance;
using Attendance.ApplicationCore.Interfaces.EngineeringTeamMapping;
using Attendance.ApplicationCore.Services.EngineeringTeamMapping;
using Attendance.Domain.Interfaces.EngineeringTeamMapping;
using Attendance.Infrastructure.Data.Repositories.EngineeringTeamMapping;
using Attendance.ApplicationCore.Interfaces.Holiday;

using Attendance.Domain.Interfaces.Holiday;
using Attendance.Infrastructure.Data.Repositories.Holiday;
using Attendance.ApplicationCore.Services.Holiday;
using Attendance.ApplicationCore.Interfaces.Infra;
using Attendance.ApplicationCore.Services.Infra;
using Attendance.Domain.Interfaces.Infra;
using Attendance.Infrastructure.Data.Repositories.Infra;
using Attendance.ApplicationCore.Interfaces.Leave;
using Attendance.ApplicationCore.Services.Leave;
using Attendance.Domain.Interfaces.Leave;
using Attendance.Infrastructure.Data.Repositories.Leave;
using Attendance.ApplicationCore.Interfaces.Reimbursement;
using Attendance.ApplicationCore.Services.Reimbursement;
using Attendance.Domain.Interfaces.Reimbursement;
using Attendance.Infrastructure.Data.Repositories.Reimbursement;

namespace Attendance.Infrastructure.loc
{
    public class Container
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //servicess
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IEngineeringTeamMappingService, EngineeringTeamMappingService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IInfraService, InfraService>();
            services.AddScoped<ILeaveService, LeaveService>();
            services.AddScoped<IReimbursementService, ReimbursementService>();



            //repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IEngineeringTeamMappingRepository, EngineeringTeamMappingRepository>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IInfraRepository, InfraRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<IReimbursementRepository, ReimbursementRepository>();

        }
    }
}
