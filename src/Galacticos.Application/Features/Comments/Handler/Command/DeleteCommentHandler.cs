using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Domain.Errors;
using MediatR;

namespace Galacticos.Application.Features.Comments.Handler.Command
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentRequest, ErrorOr<bool>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public DeleteCommentHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            Comment commentToDelete = await _commentRepository.GetCommentById(request.Id)!;

            if (commentToDelete == null)
            {
                return Errors.Comment.CommentNotFound;
            }

            if (commentToDelete.UserId != request.UserId)
            {
                return Errors.Comment.CommentIsNotYours;
            }

            bool result = await _commentRepository.DeleteComment(request.Id);
            return result;
        }
    }
}