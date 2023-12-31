using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.DTOs.Comments.Validators;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Domain.Errors;
using MediatR;

namespace Galacticos.Application.Features.Comments.Handler.Command
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, ErrorOr<CommentResponesDTO>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateCommentHandler(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<CommentResponesDTO>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CommentCommandValidator();

            var obj = new CreateCommentRequestDTO
            {
                Content = request.Content,
            };
            var validationResult = validator.Validate(obj);

            if (!validationResult.IsValid)
            {
                return Errors.Comment.InvalidComment;
            }

            var user = await _userRepository.Exists(request.UserId);

            if (!user)
            {
                return Errors.User.UserNotFound;
            }

            var post = await _postRepository.GetById(request.PostId);
            if (post == null)
            {
                return Errors.Post.PostNotFound;
            }

            var comment = _mapper.Map<Comment>(request);
            var result = await _commentRepository.CreateComment(comment);
            if(result == null)
            {
                return Errors.Comment.CommentCreationFailed;
            }
            var response = _mapper.Map<CommentResponesDTO>(result);
            return response;
        }
    }
}