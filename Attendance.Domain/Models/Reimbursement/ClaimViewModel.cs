using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Reimbursement
{
    public class ClaimViewModel
    {
        public int ClaimId { get; set; }
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter the reimbursement amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please upload the bill image.")]
        [DataType(DataType.Upload)]
        public IFormFile BillImage { get; set; }
        public string? BillImagePath { get; set; }
    }
    public class ClaimStatusViewModel
    {
        public int ClaimId { get; set; }
        public string Status { get; set; }
        public string ManagerApprovalStatus { get; set; }
        public string RegionalManagerApprovalStatus { get; set; }
        public string AccountPersonApprovalStatus { get; set; }


        public int ID { get; set; }
        public int PayoutTypeID { get; set; }
        public string? PayoutType { get; set; }
        public int Month { get; set; }
        public int year { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public int ClaimType { get; set; }
        public int ClaimTypeSub { get; set; }
        public int PayoutSubTypeID { get; set; }
        public string? PayoutSubType { get; set; }
        public int ClaimAmount { get; set; }
        public string? KMIN { get; set; }
        public string? ClaimDate { get; set; }
        public string? KMOUT { get; set; }
        public string? REMARKS { get; set; }
        public bool ClaimStatus { get; set; }
        public string? ClaimStatuss { get; set; }
        public int Claim { get; set; }
        public string? Images { get; set; }
        public string? FileName { get; set; }
        public string? ClaimDetail { get; set; }
    }

}
