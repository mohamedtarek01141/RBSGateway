using Microsoft.EntityFrameworkCore;
using RBSGateway.Entities;

namespace RBSGateway.Interface
{
    public interface IResourceRepository
    {
        Task<Resource> GetResourceByIdAsync(int id);
        Task<IEnumerable<Resource>> GetAllResourcesAsync();
        Task<Resource> AddResourceAsync(Resource resource);
        Task<bool> UpdateResourceAsync(Resource resource);
        Task<bool> DeleteResourceAsync(int id);
    }
}
