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
            return await _context.Resources.Include(r => r.ResourceName).AsNoTracking().ToListAsync();
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

        public async Task<bool> DeleteResourceAsync(int resourceId, int companyId, int tenantId)
        {
            var resource = await _context.Resources.FindAsync(resourceId, companyId, tenantId);
            if (resource == null)
            {
                return false;
            }
            _context.Resources.Remove(resource);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
