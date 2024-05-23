using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{
    [ApiController]
   [Route("[controller]")]

    public class WalkController:Controller

    {
        private readonly IWalkRepository walkRepository;

        private readonly IMapper Mapper;    

        public WalkController(IWalkRepository walkRepository,IMapper Mapper)
        {
            this.walkRepository = walkRepository;
            this.Mapper = Mapper;   
        }

        [HttpGet]
        [Route("Walks")]
        public async Task<IActionResult>  GetAllWalkAsync()
            // fetch the data from the database 
        {
           var walkDomain = await walkRepository.GetAllAsync();

            //convert domain to dto
            var walkDTO = Mapper.Map <List<Models.DTO.Walk>>(walkDomain);
            return Ok(walkDTO); 

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(" GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id )
        {
            // domain object from database 

            var walkDomain = await walkRepository.GetAsync(id);
            // domain to dto 
            var walkDTO = Mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            ////convert dto to domain object
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name    = addWalkRequest.Name,  
                RegionId = addWalkRequest.RegionId, 
                WalkDifficultyId = addWalkRequest.WalkDifficultyId, 


            };
            // domain object to repository 
            walkDomain = await walkRepository.AddAsync(walkDomain);
            // domain to dto 

            var walkDTO = new Models.DTO.Walk
            {

                Id = walkDomain.Id, 
                Name =addWalkRequest.Name,
                Length=addWalkRequest.Length,
                RegionId =addWalkRequest.RegionId,
                WalkDifficultyId=addWalkRequest.WalkDifficultyId,   
            };
            //send dto to client 
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,

            };
            //pass details to repository 
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            {
                if(walkDomain ==null)
                {
                    return NotFound();
                }
                var walkDTO = new Models.DTO.Walk
                {
                    Id = walkDomain.Id,
                    Name = updateWalkRequest.Name,
                    Length = updateWalkRequest.Length,
                    RegionId = updateWalkRequest.RegionId,
                    WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
                };
                return Ok (walkDTO);

            }

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id )
        {
            var walkDomain = await walkRepository.DeleteAsync(id);  
            if(walkDomain == null)
            {
                return NotFound();  

            }

            var walkDTO = Mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);
        }

    }

   
}
