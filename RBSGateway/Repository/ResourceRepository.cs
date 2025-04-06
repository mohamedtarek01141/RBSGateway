using Microsoft.EntityFrameworkCore;
using RBSGateway.Data;
using RBSGateway.Entities;
using RBSGateway.Interface;

namespace RBSGateway.Repository
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;
        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;

        }


        public async Task<IEnumerable<Resource>> GetAllResourcesAsync()
        {
            var allResources = await _context.Resources.Include(r=>r.ResourceName)
           .AsNoTracking()
           .ToListAsync();
            var resourcesDict = allResources.ToDictionary(r => r.ResourceID);
            foreach (var resource in allResources.Where(r => r.ParentID != null))
            {
                if (resourcesDict.TryGetValue(resource.ParentID.Value, out var parent))
                {
                    parent.Items ??= new List<Resource>();
                    parent.Items.Add(resource);
                }
            }
            return allResources.Where(r => r.ParentID == null).ToList();


        }


        
        public async Task<IEnumerable<Resource>> GetTopLeVelResources()
        {
            return await _context.Resources
        .Where(r => r.ParentID == null)
        .ToListAsync();
        }

        public async Task<Resource> GetResourceByIdAsync(int resourceId, int companyId, int tenantId)
        {
            return await _context.Resources.Include(r => r.ResourceName).Include(it=>it.Items).AsNoTracking()
                                           .FirstOrDefaultAsync(r => r.ResourceID == resourceId &&
                                                                     r.TenantID==tenantId       &&
                                                                     r.CompanyID==companyId);
        }

        public async Task<Resource> AddResourceAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<bool> UpdateResourceAsync(Resource resource)
        {
            _context.Resources.Update(resource);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteResourceAsync(Resource resource)
        {
            _context.Resources.Remove(resource);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
