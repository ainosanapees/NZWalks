using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper Mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper Mapper)
        {
            this.regionRepository = regionRepository;
            this.Mapper = Mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Latitude = region.Latitude,
            //        Longitude = region.Longitude,
            //        Population = region.Population,

            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            var regionsDTO = Mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }



        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task <IActionResult> GetRegionAsync( Guid id )
        {
            var region= await regionRepository.GetAsync(id);// Ireposiotory
            if(region == null)
            {
                return  NotFound();
            }
            var regionDTO = Mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);


        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // request(DTO) to Domanin
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Latitude = addRegionRequest.Latitude,
                Longitude = addRegionRequest.Longitude,
                Population = addRegionRequest.Population

            };

            // pass the details to the repository 
            region = await regionRepository.AddAsync(region);
            //convert back to dto 
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code =region.Code,
                Name = region.Name,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new {id =regionDTO.Id}, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync( Guid id )
        {
            //Get region for the database 
            var region = await  regionRepository.DeleteAsync(id);
            if(region ==null)
            {
                return NotFound();
            }
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population

            }; 
            //Convert response back to the dto
            return Ok(region);
           
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to Domain

            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Latitude = updateRegionRequest.Latitude,
                Longitude = updateRegionRequest.Longitude,
                Population = updateRegionRequest.Population

            };
            // Update  Region to the repository

            region = await regionRepository.UpdateAsync(id, region);
            //If Null Not found
            if(region == null)
            {
                return NotFound();
            }

            // Convert Domain to Dto 
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population
            };
            return Ok(regionDTO);   

            // return ok 
        }
        


    }
}
