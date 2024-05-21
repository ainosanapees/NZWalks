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
        public async Task<IEnumerable<Region>> GetAll()
        {
            return await  nzdbContext.Regions.ToListAsync();
        }
    }
}
