using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Reimbursement
{
    public class ReimbursementNew
    {
        public int ID { get; set; }
        public int PayoutTypeID { get; set; }
        public string? PayoutType { get; set; }
        public int Month { get; set; }
        public int year { get; set; }
        public string? EmpId { get; set; }
        public string? EMP_ID { get; set; }
        public string? EmpName { get; set; }
        public int ClaimType { get; set; }
        public string? ClaimTypeName { get; set; }
        public int ClaimTypeSub { get; set; }
        public string? ClaimTypeSubName { get; set; }
        public int PayoutSubTypeID { get; set; }
        public string? PayoutSubType { get; set; }
        public int ClaimAmount { get; set; }
        public string? KMIN { get; set; }
        public string? ClaimDate { get; set; }
        public string? KMOUT { get; set; }
        public string? REMARKS { get; set; }
        public string? ManagerRemarks { get; set; }
        public int RevisedAmount { get; set; }
        public string? verificationDate { get; set; }
        public bool ClaimStatus { get; set; }
        public string? ClaimStatuss { get; set; }
        public string? ManagerApproval { get; set; }
        public int ReportingManager { get; set; }
        public string? RM_Approval { get; set; }
        public int ProjectHeadID { get; set; }
        public int? Finance_Approval { get; set; }
        public int Claim { get; set; }
        public string? Images { get; set; }
        public string? FileName { get; set; }
        public string? ClaimDetail { get; set; }
        public string? InvoiceDate { get;set; }
        public string? TRNNumber { get; set; }
        public int tax { get; set; }
        public int TotalAmount { get; set; }
        public string? creditTax { get; set; }
        public string? InoiceNumber { get; set; }
        public int ApprovalLevel { get; set; }
        public string DisableOption { get; set; }
    }
    public class RootObject
    {
        public List<ReimbursementNew> reimbursementNews { get; set; }
        public Metadata metadata { get; set; }
    }
}
