using Attendance.Domain.Models.Reimbursement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Interfaces.Reimbursement
{
    public interface IReimbursementService
    {
        List<ReimbursementNew> GetAcReimbursement(int month, int year, string? empId, int userId, string? userType, string? userSubType);
        List<ClaimStatus> GetClaimSatus();
        object GetClaimStatusById(int newClaimId);
        List<ClaimStatusViewModel> GetClaimStatuses(int month, int year, string empid);
        List<ClaimType> GetClaimType();
        List<ReimbursementNew> GetPaidReimbursement(int month, int year, string? empId);
        List<ReimbursementNew> GetReimbursementDtl(int month, int year, string empId);
        List<SubClaimType> GetSubClaimType(int claimType);
        List<SubClaimType> GetSubClaimTypes(int claimType);
        int InsertReimbursementClaim(int employeeId, decimal amount, string billImagePath);
        int SaveacReimbursementApproval(string claimDetail, int month, int year, string empId, string aRDATE, List<int> claimId);
        int SaveHeadReimbursementApproval(int approvalId, int status);
        int SaveHeadReimbursementApprovalDtl(string ClaimDetail, int Month, int year, string EmpId, string ARDATE);
        //int SaveReimbursement(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName);
        int SaveReimbursement(string ClaimDetail, int Month, int year, string EmpId);
        int SaveReimbursementApproval(string claimDetail, int month, int year, string empId, List<int> claimId);
        int SaveReimbursementMobile(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName);
        void UpdateClaimStatus(int claimId, string approvalLevel, string status);
    }
}
