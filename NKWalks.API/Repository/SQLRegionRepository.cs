using Microsoft.EntityFrameworkCore;
using NKWalks.API.Data;
using NKWalks.API.Models.Domain;

namespace NKWalks.API.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NKWalkDBContext dBContext;

        public SQLRegionRepository(NKWalkDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dBContext.Regions.AddAsync(region);
            await dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingRegion == null) 
            { 
                return null;
            }
            dBContext.Regions.Remove(existingRegion);
            await dBContext.SaveChangesAsync();

            return existingRegion;
            
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
           var existingRegion = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null) 
            { 
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dBContext.SaveChangesAsync();

            return existingRegion;

        }
    }
}
