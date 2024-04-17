using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.DropDownModel
{
    public class Countries
    {
        public int CountryID { get; set; }

        public string Country { get; set; }
        public string EncryptcountryID { get; set; }
    }
    public class DropDown
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public enum StatusCode1
    {
        Success = 101,
        Error = 102,
        AlreadyExist = 103,
        NotExist = 104,
        NotAccess = 105,
        Blank = 106,
        Expired = 107,
        Punchintimeexist=108,
        Punchouttimeexist=109,
        IntimeisMissing= 110,
        OuttimealreadyMarked=111,
        DataUpdated = 112

    }

}
