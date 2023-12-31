using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using Galacticos.Application.Features.Posts.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using MediatR;

namespace Galacticos.Application.Features.Posts.Handlers.Queries
{
    public class GetFilterdPostQueryHandler : IRequestHandler<GetFilterdPostQuery, List<PostResponesDTO>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;
        
        public GetFilterdPostQueryHandler(IPostRepository postRepository, IMapper mapper, ITagRepository tagRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
        }

        public async Task<List<PostResponesDTO>> Handle(GetFilterdPostQuery request, CancellationToken cancellationToken)
        {
            List<Post> posts = new();
            List<Tag> tags = new();
            List<PostTag> postTags= new();

            foreach (var tag in request.Tag)
            {
                var t = await _tagRepository.GetTagByName(tag);
                if(t != null) tags.Add(t);
            }
            foreach(Tag t in tags){
                var pt = await _postTagRepository.GetPostTagsByTagId(t.Id);
                postTags.AddRange(pt);
            }
            // remove duplicates from posttags
            postTags = postTags.Distinct().ToList();
            foreach(PostTag pt in postTags){
                var p = await _postRepository.GetById(pt.PostId);
                if(p != null) posts.Add(p);
            }
            // remove duplicates from posts
            posts = posts.Distinct().ToList();

            return _mapper.Map<List<PostResponesDTO>>(posts);
        }
    }
}