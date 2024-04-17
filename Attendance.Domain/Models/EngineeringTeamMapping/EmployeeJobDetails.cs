using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.EngineeringTeamMapping
{
    public class EmployeeJobDetails
    {
        public string? EMPIDName { get; set; }
        public int ID { get; set; }
        public int EMPLOYEEID { get; set; }

        public int EMPLOYEEID_2 { get; set; }
        public DateTime APPLICATIONDATE { get; set; }
        public DateTime INTERVIEWDATE { get; set; }
        public DateTime HIREDATE { get; set; }
        public DateTime AppLetterIssueDate { get; set; }
        public DateTime OfferLetterIssueDate { get; set; }
        public DateTime JOINING_DATE { get; set; }
        public string? JOBSTATUS { get; set; }
        public DateTime RESIGNEDATE { get; set; }
        public DateTime LASTWORKINGDATE { get; set; }
        public string? LEAVINGTYPE { get; set; }

        public int LeavingTypeID { get; set; }

        public int PROJECTID { get; set; }
        public int LOCATIONID { get; set; }
        public int DesignationID { get; set; }
        public int DepartmentID { get; set; }

        public string? Department { get; set; }
        public string? ResourceType { get; set; }
        public int ResourceTypeID { get; set; }
        public string? OldDesignation { get; set; }
        public string? EXISTINGORG { get; set; }

        public string? ResorceType { get; set; }



        public int EXISTINGORG1 { get; set; }



        public int EmployeeClientID { get; set; }
        public string? ReportingCordID { get; set; }
        public string? ReportingManagerID { get; set; }
        public string? ProjectHeadID { get; set; }
        public int STATUS { get; set; }
        public string? REMARKS { get; set; }
        public int SpaceID { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public int CompanyID { get; set; }
        public int VendorID { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public string? EmployeeName { get; set; }

        public double CasualLeave { get; set; }
        public double PrivilegedLeave { get; set; }
        public double MedicalLeave { get; set; }
        public string? Password { get; set; }
        public string? ApprovalLevel { get; set; }
        public int ApprovalLevelID { get; set; }
        public string? EMP_ID { get; set; }
        public int EMPID { get; set; }
    }
}
