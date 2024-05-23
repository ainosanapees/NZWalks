using AutoMapper;

namespace NZWalks.Api.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile()
        {
            // Create map between domain model and DTO for Walk
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>().ReverseMap();

            // Create map between domain model and DTO for WalkDifficulty
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();
        }
    }
}
