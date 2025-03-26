using RBSGateway.Entities;

namespace RBSGateway.Interface
{
    public interface IResourceNameRepository
    {
        Task<ResourceName> GetResourceNameByIdAsync(int id);
        Task<IEnumerable<ResourceName>> GetAllResourceNamesAsync();
        Task<ResourceName> AddResourceNameAsync(ResourceName resourceName);
        Task<bool> UpdateResourceNameAsync(ResourceName resourceName);
        Task<bool> DeleteResourceNameAsync(int id);
    }
}
