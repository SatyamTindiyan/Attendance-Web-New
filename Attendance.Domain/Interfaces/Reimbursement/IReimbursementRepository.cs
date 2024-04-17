using Attendance.Domain.Models.Reimbursement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.Reimbursement
{
    public interface IReimbursementRepository
    {
        List<ReimbursementNew> GetAcReimbursement(int month, int year, string? empId, int userId, string? userType, string? userSubType);
        List<ClaimStatus> GetClaimStatus(int id);
        List<ClaimType> GetClaimType(int id);
        List<ReimbursementNew> GetPaidReimbursement(int month, int year, string empId);
        List<ClaimStatusViewModel> GetReimbursement(int month, int year, string empId);
        List<ReimbursementNew> GetReimbursementDtl(int month, int year, string empId);
        List<SubClaimType> GetSubClaimType(int id , int claimType);
        int InsertReimbursementClaim(int employeeId, decimal amount, string billImagePath);
        int SaveacReimbursementApproval(string claimDetail, int month, int year, string empId, List<int> claimId);
        int SaveHeadReimbursementApproval(int approvalId, int status);
        int SaveHeadReimbursementApprovalDtl(string ClaimDetail, int Month, int year, string EmpId, string ARDATE);
        //int SaveReimbursement(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName);
        int SaveReimbursement(string ClaimDetail, int Month, int year, string EmpId);
        int SaveReimbursementApproval(string claimDetail, int month, int year, string empId, List<int> claimId);
        int SaveReimbursementMobile(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName);
        void UpdateClaimStatus(int claimId, string approvalLevel, string status);
    }
}
