using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Holiday
{
    public class HolidayExcel
    {
        public int id { get; set; }
        public string Acountry { get; set; }
        public string AHOLIDAY_NAME { get; set; }
        public string AHOLIDAY_DESC { get; set; }
        public int Acountryid { get; set; }


    }
    public class Holidaymasterexcel
    {
        public int id { get; set; }
        public string Acountry { get; set; }
        public string AHOLIDAY_NAME { get; set; }
        public string AHOLIDAY_DESC { get; set; }
        public int Acountryid { get; set; }
        public string AState { get; set; }
        public string AHOLIDAY_DATE { get;set; }
        public string AREMARKS { get; set; }    

    }
}
