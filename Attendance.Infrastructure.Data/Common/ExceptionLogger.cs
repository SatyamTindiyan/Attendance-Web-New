using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data.Common
{
    public class ExceptionLogger
    {
        public int ExceptionId { get; set; }
        public string ErrorFrom { get; set; } = string.Empty;
        public int ErrorLine { get; set; }
        public string ExceptionMessage { get; set; } = string.Empty;
        public int ErrorNumber { get; set; }
        public string ErrorProcedure { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public string ExceptionStackTrack { get; set; } = string.Empty;
        public DateTime ExceptionLogTime { get; set; }
    }
}
