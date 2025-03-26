using Microsoft.EntityFrameworkCore;
using RBSGateway.Data;
using RBSGateway.Entities;
using RBSGateway.Interface;

namespace RBSGateway.Repository
{
    public class ResourceNameRepository : IResourceNameRepository
    {


        private readonly ApplicationDbContext _context;

        public ResourceNameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResourceName> GetResourceNameByIdAsync(int id)
        {
            return await _context.ResourceNames.FindAsync(id);
        }

        public async Task<IEnumerable<ResourceName>> GetAllResourceNamesAsync()
        {
            return await _context.ResourceNames.ToListAsync();
        }

        public async Task<ResourceName> AddResourceNameAsync(ResourceName resourceName)
        {
            await _context.ResourceNames.AddAsync(resourceName);
            await _context.SaveChangesAsync();
            return resourceName;
        }

        public async Task<bool> UpdateResourceNameAsync(ResourceName resourceName)
        {
            _context.ResourceNames.Update(resourceName);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteResourceNameAsync(int id)
        {
            var resourceName = await _context.ResourceNames.FindAsync(id);
            if (resourceName == null)
            {
                return false;
            }

            _context.ResourceNames.Remove(resourceName);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
