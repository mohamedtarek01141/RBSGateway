namespace RBSGateway.DTO.Resource
{
    public class CreateResourceDto
    {
      
        public int? ParentID { get; set; }
        public int? DepartmentID { get; set; }
        public int? SiteID { get; set; }
        public int? SectorID { get; set; }
        public int? SectionID { get; set; }
        public string ResourceName { get; set; }
        public string Language { get;set; }

    }
}
