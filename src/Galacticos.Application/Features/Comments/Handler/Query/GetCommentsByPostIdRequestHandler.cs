using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.Features.Comments.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using Galacticos.Domain.Errors;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Features.Comments.Handler.Query
{
    public class GetCommentsByPostIdRequestHandler : IRequestHandler<GetCommentsByPostIdRequest, ErrorOr<List<CommentResponesDTO>>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public GetCommentsByPostIdRequestHandler(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<List<CommentResponesDTO>>> Handle(GetCommentsByPostIdRequest request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.PostId);
            if (post == null)
            {
                return Errors.Post.PostNotFound;
            }

            List<Comment> comments = await _commentRepository.GetCommentsByPostId(request.PostId);
            var commentResponses = _mapper.Map<List<CommentResponesDTO>>(comments);

            return commentResponses;
        }
    }
}