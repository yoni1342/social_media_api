using Galacticos.Application.DTOs.Likes;
using MediatR;
using Galacticos.Application.Features.Likes.Command.Queries;
using Galacticos.Application.Persistence.Contracts;
using AutoMapper;
using FluentValidation;
using Galacticos.Domain.Entities;
using ErrorOr;
using Galacticos.Domain.Errors;

namespace Galacticos.Application.Features.Likes.Handler.Queries
{
    public class LikePostRequestHandler : IRequestHandler<LikePostRequest, ErrorOr<LikeResponseDto>>
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        
        public LikePostRequestHandler(ILikeRepository likeRepository, IMapper mapper, IUserRepository userRepository, IPostRepository postRepository)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<LikeResponseDto>> Handle(LikePostRequest request, CancellationToken cancellationToken)
        {

            var user = _userRepository.GetUserById(request.UserId);

            if (user == null)
            {
                return Errors.User.UserNotFound;
            }

            var post = _postRepository.GetById(request.PostId);

            if (post == null)
            {
                return Errors.Post.PostNotFound;
            }

            var likes = _mapper.Map<Like>(request);
            var like = await _likeRepository.LikePost(likes.PostId, likes.UserId);

            if (like == null)
            {
                return Errors.Like.LikeCreationFailed;
            }

            LikeResponseDto response = _mapper.Map<LikeResponseDto>(like);
            return response;
        }
    }
}