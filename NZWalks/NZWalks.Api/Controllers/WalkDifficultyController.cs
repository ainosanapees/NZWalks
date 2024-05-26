using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper Mapper;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper Mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.Mapper = Mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            return Ok(await walkDifficultyRepository.GetAllAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }


            var walkDifficultyDTO = Mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficulty addWalkDifficulty)
        {
            // request(DTO) to Domanin
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficulty.Code,


            };

            // pass the details to the repository 
            walkDifficulty = await walkDifficultyRepository.AddAsync(walkDifficulty);
            //convert back to dto 
            var walkDifficultyDTO = new Models.DTO.Region()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code,

            };
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO); ;
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id , [FromBody ] UpdateWalkDifficulty updateWalkDifficulty)
        {
            //DTO to domain model 
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficulty.Code,
            };
            walkDifficulty = await walkDifficultyRepository.UpdateAsync( walkDifficulty,id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code,
            };
            return Ok(walkDifficultyDTO);


        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult > DeleteWalkDifficultyAsync(Guid id )
        {
            var walkDifficulty = await walkDifficultyRepository.DeleteAsync(id);
            if(walkDifficulty ==null)
            {
                return NotFound()
;            }
            var walkDifficultyDTO = Mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }
    }
}