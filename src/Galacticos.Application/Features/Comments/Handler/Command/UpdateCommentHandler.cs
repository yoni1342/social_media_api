using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using Galacticos.Domain.Errors;
using AutoMapper;
using Galacticos.Application.DTOs.Comments.Validators;

namespace Galacticos.Application.Features.Comments.Handler.Command
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, ErrorOr<CommentResponesDTO>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public UpdateCommentHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<CommentResponesDTO>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetCommentById(request.CommentId)!;
            if(comment == null)
            {
                return Errors.Comment.CommentNotFound;
            }
            if(comment.UserId != request.UserId)
            {
                return Errors.Comment.CommentIsNotYours;
            }

            var validator = new CommentCommandValidator();
            var obj = new CreateCommentRequestDTO()
            {
                Content = request.Content
            };

            var result = validator.Validate(obj);

            if (!result.IsValid)
            {
                return Errors.Comment.InvalidComment;
            }

            var commentToUpdate = _mapper.Map(request, comment);
            var updatedComment = await _commentRepository.UpdateComment(commentToUpdate);
            return _mapper.Map<CommentResponesDTO>(updatedComment);   
        }
    }
}