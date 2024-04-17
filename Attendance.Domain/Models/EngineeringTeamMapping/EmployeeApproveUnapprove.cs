using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.EngineeringTeamMapping
{
    public class EmployeeApproveUnapprove
    {
        public string? IndCMFA { get; set; }
        public string? OtherFFRARRRF { get; set; }
        public string? UserType { get; set; }
        public string? Project { get; set; }
        public int CompanyID { get; set; }

        public int VendorID { get; set; }

        public int ProjectID { get; set; }

        //                       

        public int ID { get; set; }
        public string? EMP_ID { get; set; }

        public string? EmployeeName { get; set; }
        public string? CONTACTNO { get; set; }
        public string? PERSONALEMAIL { get; set; }
        public int DesignationID { get; set; }
        public string? Designation { get; set; }
        public string? Experience { get; set; }

        public string? Induction { get; set; }

        public string? Certified { get; set; }
        public string? CertifiedElectricalMSC { get; set; }
        public string? FirstAid { get; set; }
        public string? InductionDoc { get; set; }
        public string? CertifiedDoc { get; set; }
        public string? CertifiedElectricalMSCDoc { get; set; }
        public string? FirstAidDoc { get; set; }

        public string Result { get; set; }



        public int AppRejStatus { get; set; }

        public List<EmployeeApproveUnapproveChk> ChkSelectAll { get; set; }
        //public List<string> ChkSelectAll { get; set; }


    }

    public class EmployeeApproveUnapproveChk
    {
        //public int Id { get; set; }
        public string Emp_Id { get; set; }
    }
}

