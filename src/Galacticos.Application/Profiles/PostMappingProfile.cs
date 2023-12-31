using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Galacticos.Application.DTOs.Posts;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Application.Features.Posts.Request.Commands;
using Galacticos.Domain.Entities;
namespace Galacticos.Application.Profiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<CreatePostCommand, Post>(); // CreateMap for CreatePostCommand to Post mapping
            CreateMap<Post, PostResponesDTO>() // CreateMap for Post to PostResponesDTO mapping
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));

            CreateMap<CreatePostCommand, Post>();
            CreateMap<UpdatePostRequestDTO, Post>()
                .ForAllMembers(x => x.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                    {
                        return false;
                    }
                    if (srcMember.GetType() == typeof(string) && string.IsNullOrEmpty((string)srcMember))
                    {
                        return false;
                    }
                    return true;
                }));

            CreateMap<Task<Post>, PostResponesDTO>();
            CreateMap<Post, PostResponesDTO>();
            CreateMap<CreatePostRequestDTO, CreatePostCommand>();
            CreateMap<PostDto, Post>().ReverseMap();
        }

    }
}