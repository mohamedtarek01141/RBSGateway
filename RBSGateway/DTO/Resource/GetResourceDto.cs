namespace RBSGateway.DTO.Resource
{
    public class GetResourceDto
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public int? ParentID { get; set; }
        public string Language { get; set; } 
        public List<GetResourceDto> Items { get; set; }
    }
}
