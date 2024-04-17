using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.loc
{
    public static class StatusMessageCode
    {
        public enum StatusCode
        {
            Success = 101,
            Error = 102,
            AlreadyExist = 103,
            NotExist = 104,
            NotAccess = 105,
            Blank = 106,
            Expired = 107,
            PreNotExist = 108
        }
    }
}
