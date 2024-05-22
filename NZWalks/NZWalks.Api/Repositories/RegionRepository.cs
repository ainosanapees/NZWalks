using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nzdbContext;
        public RegionRepository(NZWalksDbContext nzdbContext) { 
            this.nzdbContext =nzdbContext;   
        }

        public async  Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nzdbContext.AddAsync(region);
            await nzdbContext.SaveChangesAsync();
            return region;
        }

        public async  Task<Region> DeleteAsync(Guid id)
        {
            var region = await nzdbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }

            nzdbContext.Regions.Remove(region);
            await nzdbContext.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await  nzdbContext.Regions.ToListAsync();
        }

        
        public async Task<Region> GetAsync(Guid id)
        {
           return await  nzdbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);  
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nzdbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;  
            existingRegion.Area= region.Area;
            existingRegion.Latitude = region.Latitude;
            existingRegion .Longitude =region.Longitude;
            existingRegion.Population =region.Population;

            await nzdbContext.SaveChangesAsync();   
            return existingRegion;

        }
    }
}
