using RBSGateway.Entities;
using System.Threading.Tasks;

namespace RBSGateway.Interface
{
    public interface IResourceNameRepository
    {
        Task<ResourceName> GetResourceNameByIdAsync(int id);
        Task<ResourceName> GetResourceByNameAsync(string name);
        Task<IEnumerable<ResourceName>> GetAllResourceNamesAsync();
        Task<ResourceName> AddResourceNameAsync(ResourceName resourceName);
        Task<bool> UpdateResourceNameAsync(ResourceName resourceName);
        Task<bool> DeleteResourceNameAsync(int id);
    }
}
