using RBSGateway.DTO.Resource;
using RBSGateway.Entities;

namespace RBSGateway.Services.ResourceServices
{
    public interface IResourceService
    {
        Task<GetResourceDto> GetByIdAsync(int resourceId, int companyId, int tenantId);
        Task<List<GetResourceDto>> GetAllAsync();
        Task<Resource> CreateAsync(CreateResourceDto dto);
        Task<bool> UpdateAsync(UpdateResourceDto dto);
        Task<bool> DeleteAsync(int resourceId, int companyId, int tenantId);
    }
}
