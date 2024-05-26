using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nzdbContext;
        public WalkDifficultyRepository(NZWalksDbContext nzdbContext)
        { 
            this.nzdbContext = nzdbContext; 
        }

        public async  Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid .NewGuid();
             await nzdbContext.AddAsync(walkDifficulty);
             await  nzdbContext.SaveChangesAsync();
             return walkDifficulty;  
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await nzdbContext.WalksDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null)
            
            {
                return null;
            }
             nzdbContext.WalksDifficulty.Remove(walkDifficulty);
             await nzdbContext.SaveChangesAsync();  
            return walkDifficulty;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nzdbContext.WalksDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nzdbContext.WalksDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(WalkDifficulty walkDifficulty, Guid id)
        {
            var existingWalkDifficulty = await nzdbContext.WalksDifficulty.FindAsync(id);
            if (existingWalkDifficulty == null)
            {
                return null;

            }
            existingWalkDifficulty.Code = walkDifficulty.Code;
            await nzdbContext.SaveChangesAsync();

            return existingWalkDifficulty;  
        }
    }
}
