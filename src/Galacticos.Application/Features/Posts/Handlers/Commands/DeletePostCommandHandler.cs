using AutoMapper;
using Galacticos.Application.Features.Posts.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using Galacticos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Domain.Errors;

namespace Galacticos.Application.Features.Posts.Handlers.Commands
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ErrorOr<bool>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostTagRepository _postTagRepository;

        public DeletePostCommandHandler(IPostRepository postRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _postTagRepository = postTagRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post postToDelete = await _postRepository.GetById(request.PostId);

            if (postToDelete == null)
            {
                return Errors.Post.PostNotFound;
            }

            if (postToDelete.UserId != request.UserId)
            {
                return Errors.Post.PostIsNotYours;
            }

            bool result = await _postRepository.Delete(request.PostId);

            var detetePostTags = await _postTagRepository.GetPostTagsByPostId(request.PostId);

            foreach (var postTag in detetePostTags)
            {
                await _postTagRepository.Delete(postTag);
            }
            
            return result;
        }
    }
}
