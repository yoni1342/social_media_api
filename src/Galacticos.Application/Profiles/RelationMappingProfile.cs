using AutoMapper;
using Galacticos.Domain.Entities;
using Galacticos.Application.DTOs.Relations;

namespace Galacticos.Application.Profiles
{
    public class RelationMappingProfile : Profile
    {
        public RelationMappingProfile()
        {
            CreateMap<Follow, RelationDTO>().ReverseMap();
            CreateMap<Follow, GetFollowersDTO>().ReverseMap();
        }
    }
}