using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.Api.Repositories
{
    public class WalkRepository:IWalkRepository 
    {

        private readonly NZWalksDbContext nzdbContext;
        private Walk walkDomian;

        public WalkRepository(NZWalksDbContext nzdbContext)
        {
            this.nzdbContext = nzdbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
           // Assign new id 
            walk.Id = Guid .NewGuid (); 
            await nzdbContext.Walks.AddAsync(walk); 
            await nzdbContext.SaveChangesAsync();   
            return walk;
           
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await nzdbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }


            nzdbContext.Walks.Remove(walkDomian);
            await nzdbContext.SaveChangesAsync();

            return existingWalk;

        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return  await 
                nzdbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id )
        {
            return await nzdbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async  Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nzdbContext.Walks.FindAsync(id);
            if(existingWalk!= null)
            {

                existingWalk.Length = walk.Length;
                existingWalk.Region = walk.Region;  
                existingWalk.WalkDifficultyId= walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await nzdbContext.SaveChangesAsync();
                return existingWalk;



            }
            return null;
        }
    }

}
