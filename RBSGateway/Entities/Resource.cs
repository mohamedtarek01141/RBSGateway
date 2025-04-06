using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBSGateway.Entities
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceID { get; set; }
        public ResourceName ResourceName { get; set; }
        public int ResourceNameId { get; set; }
        public Resource Parent { get; set; }

        public int? ParentID { get; set; }
        public Tenant Tenant { get; set; }
        public int TenantID { get;set; }
        public Company Company { get; set; }
        public int CompanyID { get; set; }
        public int? DepartmentID { get; set; }
        public int? SiteID { get; set; }
        public int? SectorID { get; set; }
        public int? SectionID { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string LastUpdatedBy { get; set; } 
        public DateOnly LastUpdatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public ICollection<Resource> Items { get; set; } 

    }
}
