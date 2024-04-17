using Attendance.ApplicationCore.Interfaces.Reimbursement;
using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.Infra;
using Attendance.Domain.Models.Reimbursement;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Services.Reimbursement
{
    public class ReimbursementService : BaseService, IReimbursementService
    {
        public ReimbursementService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public List<ClaimStatus> GetClaimSatus()
        {
            List<ClaimStatus> lstGetClaimStatus = null;
            try
            {
                //VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                //if (vMUserDetail != null)
                //{
                int Id = 0;
                lstGetClaimStatus = new List<ClaimStatus>();
                lstGetClaimStatus = _reimbursementRepository.GetClaimStatus(Id);
                //}

            }
            catch
            {

            }

            return lstGetClaimStatus;
        }

        public List<ClaimType> GetClaimType()
        {
            List<ClaimType> lstGetClaimType = null;
            try
            {
                //VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                //if (vMUserDetail != null)
                //{
                int Id = 0;
                lstGetClaimType = new List<ClaimType>();
                lstGetClaimType = _reimbursementRepository.GetClaimType(Id);
                //}

            }
            catch
            {

            }

            return lstGetClaimType;
        }

        public List<ReimbursementNew> GetReimbursementDtl(int month, int year, string empId)
        {
            List<ReimbursementNew> lstGetReimbursement = null;
            try
            {
                lstGetReimbursement = new List<ReimbursementNew>();
                lstGetReimbursement = _reimbursementRepository.GetReimbursementDtl(month, year, empId);

            }
            catch
            {

            }

            return lstGetReimbursement;
        }

        public List<SubClaimType> GetSubClaimType(int claimType)
        {
            List<SubClaimType> lstGetSubClaimType = null;
            try
            {
                int Id = 0;
                lstGetSubClaimType = new List<SubClaimType>();
                lstGetSubClaimType = _reimbursementRepository.GetSubClaimType(Id , claimType);
                //}

            }
            catch
            {

            }

            return lstGetSubClaimType;
        }

        public List<SubClaimType> GetSubClaimTypes(int claimType)
        {
            List<SubClaimType> lstGetSubClaimType = null;
            try
            {
                int Id = 0;
                lstGetSubClaimType = new List<SubClaimType>();
                lstGetSubClaimType = _reimbursementRepository.GetSubClaimType(claimType , Id);
                //}

            }
            catch
            {

            }

            return lstGetSubClaimType;
        }

        public int SaveHeadReimbursementApproval(int approvalId, int status)
        {
            int result = 0;
            try
            {

                result = _reimbursementRepository.SaveHeadReimbursementApproval(approvalId, status);


            }
            catch
            {

            }

            return result;
        }

        public int SaveHeadReimbursementApprovalDtl(string ClaimDetail, int Month, int year, string EmpId , string ARDATE)
        {
            int result = 0;
            try
            {
               
                result = _reimbursementRepository.SaveHeadReimbursementApprovalDtl(ClaimDetail,Month,year,EmpId,ARDATE);
               
            }
            catch
            {

            }

            return result;
        }

        //public int SaveReimbursement(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName)
        //{
        //    int result = (int)StatusCode1.Error;
        //    try
        //    {
        //        result = _reimbursementRepository.SaveReimbursement(month, year, empId, empName, claimType, clamSubType, claimDatee, claimAmount, kmIN, kmOUT, remarks, claimStatus, fileName);
        //    }
        //    catch
        //    {

        //    }
        //    return result;
        //}

        public int SaveReimbursementMobile(int month, int year, string empId, string empName, int claimType, int clamSubType, string claimDatee, int claimAmount, int kmIN, int kmOUT, string remarks, int claimStatus, string fileName)
        {
            int result = (int)StatusCode1.Error;
            try
            {
                result = _reimbursementRepository.SaveReimbursementMobile(month, year, empId, empName, claimType, clamSubType, claimDatee, claimAmount, kmIN, kmOUT, remarks, claimStatus, fileName);
            }
            catch
            {

            }
            return result;
        }

        public int SaveReimbursement(string ClaimDetail, int Month, int year, string EmpId)
        {
            return _reimbursementRepository.SaveReimbursement(ClaimDetail, Month, year, EmpId);

        }

        public int SaveReimbursementApproval(string claimDetail, int month, int year, string empId , List<int> claimId)
        {
            return _reimbursementRepository.SaveReimbursementApproval(claimDetail, month, year, empId , claimId);
        }

        public object GetClaimStatusById(int newClaimId)
        {
            throw new NotImplementedException();
        }

        public List<ClaimStatusViewModel> GetClaimStatuses(int month, int year, string empid)
        {
            List<ClaimStatusViewModel> lstGetReimbursement = null;
            
                //VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                //if (vMUserDetail != null)
                //{
                lstGetReimbursement = new List<ClaimStatusViewModel>();
                lstGetReimbursement = _reimbursementRepository.GetReimbursement(month, year, empid);
            //}



            return lstGetReimbursement;
        }

        public int InsertReimbursementClaim(int employeeId, decimal amount, string billImagePath)
        {
            return _reimbursementRepository.InsertReimbursementClaim(employeeId, amount, billImagePath);
        }

        public void UpdateClaimStatus(int claimId, string approvalLevel, string status)
        {
             _reimbursementRepository.UpdateClaimStatus(claimId, approvalLevel, status);
        }

        //Get Paid report //
        public List<ReimbursementNew> GetPaidReimbursement(int month, int year, string empId)
        {
            List<ReimbursementNew> lstGetReimbursement = null;
            try
            {
                
                lstGetReimbursement = new List<ReimbursementNew>();
                lstGetReimbursement = _reimbursementRepository.GetPaidReimbursement(month, year, empId);
                //}

            }
            catch
            {

            }

            return lstGetReimbursement;
        }

        public List<ReimbursementNew> GetAcReimbursement(int month, int year, string? empId, int userId, string? userType, string? userSubType)
        {
            List<ReimbursementNew> lstGetacReimbursement = null;
            try
            {

                lstGetacReimbursement = new List<ReimbursementNew>();
                lstGetacReimbursement = _reimbursementRepository.GetAcReimbursement(month, year, empId,userId,userType,userSubType);
                //}

            }
            catch
            {

            }

            return lstGetacReimbursement;

        }

        public int SaveacReimbursementApproval(string claimDetail, int month, int year, string empId, string aRDATE, List<int> claimId)
        {
            return _reimbursementRepository.SaveacReimbursementApproval(claimDetail, month, year, empId, claimId);
        }
    }
}
