using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.Features.Comments.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Domain.Errors;
using MediatR;

namespace Galacticos.Application.Features.Comments.Handler.Query
{
    public class GetCommentByIdRequestHandler : IRequestHandler<GetCommentByIdRequest, ErrorOr<CommentResponesDTO>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        
        public GetCommentByIdRequestHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<CommentResponesDTO>> Handle(GetCommentByIdRequest request, CancellationToken cancellationToken)
        {
            Comment res = await _commentRepository.GetCommentById(request.Id)!;
            
            if (res == null)
            {
                return Errors.Comment.CommentNotFound;
            }

            var commentResponse = _mapper.Map<CommentResponesDTO>(res);
            return commentResponse;
        }
    }
    
}