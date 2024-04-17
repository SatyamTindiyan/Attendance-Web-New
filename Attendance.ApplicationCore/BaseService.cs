using Attendance.Domain.Interfaces.Account;
using Attendance.Domain.Interfaces.Attendance;
using Attendance.Domain.Interfaces.EngineeringTeamMapping;
using Attendance.Domain.Interfaces.Holiday;
using Attendance.Domain.Interfaces.Infra;
using Attendance.Domain.Interfaces.Leave;
using Attendance.Domain.Interfaces.Reimbursement;
using Attendance.Domain.Interfaces.User;
using Attendance.Infrastructure.Data.Repositories.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore
{
    public class BaseService 
    {
        protected readonly IAccountRepository _accountRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly IAttendanceRepository _attendanceRepository;
        protected readonly IEngineeringTeamMappingRepository _etmrepository;
        protected readonly IHolidayRepository _holidayRepository;
        protected readonly IInfraRepository _infraRepository;
        protected readonly ILeaveRepository _leaveRepository;
        protected readonly IReimbursementRepository _reimbursementRepository;
        private readonly IConfiguration _configuration;

        public BaseService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _accountRepository = serviceProvider.GetService<IAccountRepository>();
            _userRepository = serviceProvider.GetService<IUserRepository>();
            _attendanceRepository = serviceProvider.GetService<IAttendanceRepository>();
            _etmrepository = serviceProvider.GetService<IEngineeringTeamMappingRepository>();
            _holidayRepository = serviceProvider.GetService<IHolidayRepository>();
            _leaveRepository = serviceProvider.GetService<ILeaveRepository>();
            _infraRepository = serviceProvider.GetService<IInfraRepository>();
            _reimbursementRepository = serviceProvider.GetService<IReimbursementRepository>();
            _configuration = configuration;
        }
    }
}
