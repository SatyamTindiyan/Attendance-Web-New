using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Company
{
    public class CompanyDetail
    {
        public long Id { get; set; }
        public string? EncryptId { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyEncrpytId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? GstNo { get; set; }
        public string? TinNo { get; set; }
        [Required(ErrorMessage = "Company Phone is required.")]
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        [Required(ErrorMessage = "Registered Address is required.")]
        public string? RegisteredAddress { get; set; }
        [Required(ErrorMessage = "Corporate Address is required.")]
        public string? CorporateAddress { get; set; }
        public string? WebsiteUrl { get; set; }
        [Required(ErrorMessage = "Registration Date is required.")]
        // [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        [Required(ErrorMessage = "Contact Person Name is required.")]
        public string? ContactPersonName { get; set; }
        [Required(ErrorMessage = "Contact Person Phone no is required.")]
        public string? ContactPersonPhoneNo { get; set; }
        [Required(ErrorMessage = "Contact Person email is required.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        public string? ContactPersonEmail { get; set; }

        public string? Remark { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string? strRegistrationDate { get; set; }
    }
}
