using AutoMapper;
using RBSGateway.DTO.Resource;
using RBSGateway.Entities;
using RBSGateway.Interface;

namespace RBSGateway.Services.ResourceServices
{
    public class ResourceService:IResourceService
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
        public async Task<GetResourceDto> GetByIdAsync(int resourceId,int companyId, int tenantId)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(resourceId,companyId,tenantId);
            if (resource == null) return null;

            return  MapResource(resource); ;
        }

        // Get all resources
        public async Task<List<GetResourceDto>> GetAllAsync()
        {
            var resources = await _resourceRepository.GetAllResourcesAsync();
            return resources.Select(resource => MapResource(resource)).ToList();
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
            var result= await _resourceRepository.AddResourceAsync(resource);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateResourceDto dto)
        {
            var existingResource = await _resourceRepository.GetResourceByIdAsync(dto.ResourceID,dto.TenantID,dto.CompanyID);
            if (existingResource == null) return false;

            var resource= _mapper.Map<Resource>(dto);
            resource.LastUpdatedDate = DateOnly.FromDateTime(DateTime.Now);
            resource.LastUpdatedBy = "Aly";
            resource.TenantID = 1;
            resource.CompanyID = 1;
            if (!string.IsNullOrWhiteSpace(dto.ResourceName))
            {
                // Try to find an existing ResourceName with this name
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
        public async Task<bool> DeleteAsync(int resourceId, int companyID,int tenantId)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(resourceId,companyID, tenantId);
            if (resource == null) return false;

            return await _resourceRepository.DeleteResourceAsync(resource.ResourceID,resource.CompanyID,resource.TenantID);
        }
        private GetResourceDto MapResource(Resource resource)
        {
            if (resource == null)
                return null;

            var resourceDto = new GetResourceDto
            {
                ResourceID = resource.ResourceID,
                ResourceName = resource.ResourceName?.Name, // Get the name from the lookup entity
                ParentID = resource.ParentID,
                DepartmentID = resource.DepartmentID,
                SiteID = resource.SiteID,
                SectorID = resource.SectorID,
                SectionID = resource.SectionID,
                Language = resource.ResourceName?.Language,  // Language from the ResourceName entity
                CreatedBy = resource.CreatedBy,
                CreatedDate = resource.CreatedDate,
                LastUpdatedBy = resource.LastUpdatedBy,
                LastUpdatedDate = resource.LastUpdatedDate,
                Items = new List<GetResourceDto>()
            };

            // Recursively map child resources if any exist.
            if (resource.Items != null && resource.Items.Any())
            {
                resourceDto.Items = resource.Items.Select(child => MapResource(child)).ToList();
            }

            return resourceDto;
        }

    }
}
