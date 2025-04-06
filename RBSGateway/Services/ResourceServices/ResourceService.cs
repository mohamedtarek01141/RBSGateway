using AutoMapper;
using RBSGateway.DTO.Resource;
using RBSGateway.Entities;
using RBSGateway.Interface;
using System.ComponentModel.Design;
using System.Transactions;

namespace RBSGateway.Services.ResourceServices
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IResourceNameRepository _resourceNameRepository;
        private readonly IMapper _mapper;

        public ResourceService(
            IResourceRepository resourceRepository,
            IResourceNameRepository resourceNameRepository,
            IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _resourceNameRepository = resourceNameRepository;
            _mapper = mapper;
        }

        // Get a single resource by ID
        public async Task<GetResourceDto> GetByIdAsync(int resourceId, int companyId, int tenantId)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(resourceId, companyId, tenantId);
            if (resource == null) return null;

            return _mapper.Map<GetResourceDto>(resource); 
        }

        // Get all resources
        public async Task<IEnumerable<GetResourceDto>> GetAllAsync()
        {
            var resources = await _resourceRepository.GetAllResourcesAsync();
            return  _mapper.Map<IEnumerable<GetResourceDto>>(resources);
        }

        public async Task<Resource> CreateAsync(CreateResourceDto dto)
        {


            var resourceName = new ResourceName { Name = dto.ResourceName, Language = dto.Language };
            await _resourceNameRepository.AddResourceNameAsync(resourceName);

            var resource = _mapper.Map<Resource>(dto);
            resource.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            resource.LastUpdatedDate = DateOnly.FromDateTime(DateTime.Now);
            resource.CreatedBy = 1;
            resource.LastUpdatedBy = "Mohamed";
            resource.ResourceNameId = resourceName.ResourceNameID;
            resource.TenantID = 1;
            resource.CompanyID = 1;
            var result = await _resourceRepository.AddResourceAsync(resource);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateResourceDto dto)
        {
            var existingResource = await _resourceRepository.GetResourceByIdAsync(dto.ResourceID, dto.TenantID, dto.CompanyID);
            if (existingResource == null) return false;

            var resource = _mapper.Map<Resource>(dto);
            resource.LastUpdatedDate = DateOnly.FromDateTime(DateTime.Now);
            resource.LastUpdatedBy = "Aly";
            resource.TenantID = 1;
            resource.CompanyID = 1;
            if (!string.IsNullOrWhiteSpace(existingResource.ResourceName.Name))
            {
                var existingResourceName = await _resourceNameRepository.GetResourceByNameAsync(dto.ResourceName);
                if (existingResourceName == null)
                {
                    // If not found, create a new one
                    var newResourceName = new ResourceName
                    {
                        Name = dto.ResourceName,
                        Language = dto.Language
                    };
                    await _resourceNameRepository.AddResourceNameAsync(newResourceName);
                    resource.ResourceNameId = newResourceName.ResourceNameID; // Use the generated ID
                }
                else
                {
                    resource.ResourceNameId = existingResourceName.ResourceNameID;
                }
            }

            return await _resourceRepository.UpdateResourceAsync(resource);
        }

        // Soft delete a resource
        public async Task<bool> DeleteAsync(Resource resource)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
           
                await DeleteChildrenRecursive(resource);

                await _resourceRepository.DeleteResourceAsync(resource);

                transaction.Complete();
                return true;
            }
            catch
            {
                throw;
            }
        }
        private async Task DeleteChildrenRecursive(Resource resource)
        {
            if (resource?.Items == null || !resource.Items.Any())
                return;

            var deleteTasks = new List<Task>();

            foreach (var item in resource.Items)
            {
                deleteTasks.Add(DeleteChildrenRecursive(item));
                deleteTasks.Add(_resourceRepository.DeleteResourceAsync(item));
            }

            await Task.WhenAll(deleteTasks);
        }


    }
}