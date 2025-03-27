
namespace RBSGateway.Entities
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string CreatedBy { get; set; } = string.Empty;
        public DateOnly? LastUpdateDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string LastUpdateUser { get; set; } = string.Empty;
    }
}
