using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.EngineeringTeamMapping
{
    public class ProjectMasterOMMS
    {
        //public int Id { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? EncryptId { get; set; }
        //[Required(ErrorMessage = "Project code is required.")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Project name is required.")]
        public string? Name { get; set; }
        //[Required(ErrorMessage = "Project ADP is required.")]
        public string? ADP { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Nature of project is required.")]
        public int NatureId { get; set; }
        public string? EncrpytNatureId { get; set; }
        public string? Nature { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }
        public string? EncrpytCountryId { get; set; }
        public string? Country { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        public int CustomerId { get; set; }
        public string? EncrpytCustomerId { get; set; }
        public string? Customer { get; set; }

        [Required(ErrorMessage = "Operator is required.")]
        public int OperatorId { get; set; }
        public string? EncrpytOperatorId { get; set; }
        public string? Operator { get; set; }

        // [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        //[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? strStartDate { get; set; }
        public string? strEndDate { get; set; }

        public string? Remarks { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public int SpaceId { get; set; }
        public int VersionNumber { get; set; }
        //public List<EmployeeCompliance> lstEmployeeCompliances { get; set; }
        //public List<VMProjectCircleMapping> lstVMProjectCircleMappings { get; set; }
        //public List<ProjectCircleMapping> lstProjectCircleMappings { get; set; }
        //public List<VendorMaster> lstvmVendorMaster { get; set; }
        //public List<vmCircleVendor> lstvmCircleVendor { get; set; }

        public string? VCId { get; set; }
        public string? CircleIds { get; set; }
        //public List<ProjectVendorCircleNature> lstProjectVendorCircleNature { get; set; }

        //public List<CircleMaster> lstCircle { get; set; }
        public bool IsMapped { get; set; }
        public int CircleId { get; set; }
        public string? Circle { get; set; }

        // this is use only on employee site allocation 
        public string? EncryptProjectId { get; set; }

        //public List<NatureType2> lstNature2 { get; set; }
        public int ProjectTypeId { get; set; }
        public string? ProjectType { get; set; }
        //public List<DriveTypes> lstProjectType { get; set; }
    }
}

