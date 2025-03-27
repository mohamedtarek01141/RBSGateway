namespace RBSGateway.DTO.Resource
{
    public class GetResourceDto
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public int TenantID { get; set; }
        public int CompanyID { get; set; }
        public int? ParentID { get; set; }
        public int? DepartmentID { get; set; }
        public int? SiteID { get; set; }
        public int? SectorID { get; set; }
        public int? SectionID { get; set; }
        public string Language { get; set; } 
        public int CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateOnly LastUpdatedDate { get; set; }
        public List<GetResourceDto> Items { get; set; }
    }
}
