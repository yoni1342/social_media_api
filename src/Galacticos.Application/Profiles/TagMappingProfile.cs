using Galacticos.Application.DTOs;
using Galacticos.Domain.Entities;
using AutoMapper;

namespace Galacticos.Application.Profiles
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
        }    
    }
}