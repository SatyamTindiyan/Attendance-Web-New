using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Master
{
    public class VendorMaster
    {
        public int VendorID { get; set; }
        public string? EncryptVendorId { get; set; }
        public string? VendorCode { get; set; }
        public string? Vendor { get; set; }
        public int CompanyID { get; set; }
        public bool Status { get; set; }
        public string? Remarks { get; set; }
        public int SpaceID { get; set; }
        public int VersionNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }

        //public List<CircleMaster> lstCircleMaster { get; set; }
        //public List<NatureType> lstNature { get; set; }
        //public List<NatureType2> lstNature2 { get; set; }
        public int CircleId { get; set; }
        public int NatureId { get; set; }

        public bool IsMapped { get; set; }

        public int CountryID { get; set; }
        public string? ExistEmailAddress { get; set; }

        // ADD 
        public string? GSTNo { get; set; }
        public string? PAN { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? RegisteredAddress { get; set; }
        public string? CorporateAddress { get; set; }
        public string? WebsiteURL { get; set; }
        public string? ContactPersonName { get; set; }
        public string? ContactPersonPhoneNo { get; set; }
        public string? ContactPersonEmail { get; set; }

    }

    public class vmVendorMaster
    {
        public int VendorID { get; set; }
        public string EncryptVendorID { get; set; }
        public string VendorCode { get; set; }
        public string Vendor { get; set; }
        public int CompanyID { get; set; }
        public string EncryptCompanyID { get; set; }
        public bool Status { get; set; }

    }

    //public class VendorSave
    //{
    //    public int CircleID { get; set; }
    //    public string Circle { get; set; }
    //    public string NatureList { get; set; }
    //    public List<vmCircleVertical> lstvmCircleVertical { get; set; }

    //    public string CircleList { get; set; }
    //}

    //public class vmCircleVertical
    //{

    //    public int CircleID { get; set; }
    //    public int VerticalID { get; set; }


    //}
}
