using AutoMapper;
using NKWalks.API.Models.Domain;
using NKWalks.API.Models.DTO;

namespace NKWalks.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();

            CreateMap<AddRegionDTO, Region>().ReverseMap();

            CreateMap<UpdateRegionDTO,Region>().ReverseMap();

            CreateMap<AddWalksDTO,Walk>().ReverseMap();

            CreateMap<Walk,WalkDTO>().ReverseMap();

            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();

            CreateMap<UpdateWalkDTO,Walk>().ReverseMap(); 

        }
    }
}
