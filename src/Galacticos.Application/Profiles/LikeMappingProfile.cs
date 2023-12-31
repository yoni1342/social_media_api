using AutoMapper;
using Galacticos.Domain.Entities;
using Galacticos.Application.DTOs.Likes;
using Galacticos.Application.Features.Likes.Command.Queries;

namespace Galacticos.Application.Profiles
{
    public class LikeMappingProfile : Profile
    {
        public LikeMappingProfile()
        {
            CreateMap<LikePostRequest, Like>().ReverseMap();
            CreateMap<Like, LikeDto>().ReverseMap();  
            CreateMap<Like, LikeResponseDto>().ReverseMap();          
        }
    }
}