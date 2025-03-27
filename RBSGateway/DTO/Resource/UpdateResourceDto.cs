namespace RBSGateway.DTO.Resource
{
    public class UpdateResourceDto
    {
        public int ResourceID { get; set; }
        public int TenantID { get; set; }
        public int CompanyID { get; set; }
        public int? ParentID { get; set; }
        public int? DepartmentID { get; set; }
        public int? SiteID { get; set; }
        public int? SectorID { get; set; }
        public int? SectionID { get; set; }

        public int ResourceNameId { get; set; }
        public string ResourceName { get; set; }
        public string Language { get; set; }

    }
}
