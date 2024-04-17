using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Infra
{
    public class InfraType
    {
        public string EmpID { get; set; }
        public int RecordId { get; set; }
        public string InfraGrpType { get; set; }
        public int InfraGrpID { get; set; }

        public string InfraGrpCode { get; set; }

        public string Infraname { get; set; }
    }

    public class GetInfraEditdetails
    {
        public string? MODEL_NUMBER { get; set; }
        public string? Id { get; set; }
        public string? SERIAL_NUMBER { get; set; }
        public string? IMEI_NUMBER { get; set; }
        public string? INFRA_TYPE { get; set; }
        public string? REMARKS { get; set; }
        public string? INFRA_STATUS { get; set; }
    }

    public class GetImageInfra
    {
        public string? INFRA_IMAGE { get; set; }
    }

    public class GetInfraIssue
    {
        public string? EmpID { get; set; }
        public string? EmpName { get; set; }
        public string? Location { get; set; }
        public string? SerialNo { get; set; }
        public string? ServiceTag { get; set; }
        public int ModelNo { get; set; }
        public string? InfraGrp { get; set; }
    }

    public class GetPreviousDate
    {
        public int result { get; set; }
        public string? PreviousAttendanceDate { get; set; }
    }
}

