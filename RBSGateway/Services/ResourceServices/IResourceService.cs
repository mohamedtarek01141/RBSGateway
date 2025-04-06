using RBSGateway.DTO.Resource;
using RBSGateway.Entities;

namespace RBSGateway.Services.ResourceServices
{
    public interface IResourceService
    {
        Task<GetResourceDto> GetByIdAsync(int resourceId, int companyId, int tenantId);
        Task<IEnumerable<GetResourceDto>> GetAllAsync();
        Task<Resource> CreateAsync(CreateResourceDto dto);
        Task<bool> UpdateAsync(UpdateResourceDto dto);
        Task<bool> DeleteAsync(Resource resource);
    }
}
