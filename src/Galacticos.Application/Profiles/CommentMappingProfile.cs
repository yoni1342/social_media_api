using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Profiles
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap <CreateCommentRequestDTO, CreateCommentCommand>();
            CreateMap<CreateCommentCommand, Comment>();
            CreateMap<Comment, CommentResponesDTO>();
            CreateMap<UpdateCommentRequestDTO, UpdateCommentCommand>();
            CreateMap<UpdateCommentCommand, Comment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>{
                if (srcMember == null)
                {
                    return false;
                }
                if (srcMember.GetType() == typeof(string))
                {
                    return !string.IsNullOrEmpty((string)srcMember);
                }
                return true;
            }));
        }

    }
}