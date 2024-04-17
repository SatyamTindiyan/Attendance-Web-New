using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Helper
{
    public enum StoredProcedures
    {
        [Description("OMMS_CheckUserExistBasedOnUserNamePassword")]
        AuthenticateUser,

        [Description("Sp_EmpInfo")]
        Sp_EmpInfo,
    }
}
