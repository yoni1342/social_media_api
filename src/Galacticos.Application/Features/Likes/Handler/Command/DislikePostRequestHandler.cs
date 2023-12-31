using AutoMapper;
using MediatR;
using Galacticos.Application.Features.Likes.Command.Queries;
using Galacticos.Application.Persistence.Contracts;
// using Galacticos.Application.DTOs.Likes.Validators;
using FluentValidation;
using ErrorOr;
using Galacticos.Domain.Errors;

namespace Galacticos.Application.Features.Likes.Handler.Queries
{
    public class DislikePostRequestHandler : IRequestHandler<DislikePostRequest, ErrorOr<bool>>
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;

        public DislikePostRequestHandler(ILikeRepository likeRepository, IMapper mapper)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<bool>> Handle(DislikePostRequest request, CancellationToken cancellationToken)
        {
            
            var like = await _likeRepository.GetLikeByPostIdAndUserId(request.PostId, request.UserId);

            if (like == null)
            {
                return Errors.Like.DidNotLiked;
            }

            bool res =await _likeRepository.UnlikePost(like.PostId, like.UserId);

            if (!res)
            {
                return Errors.Like.LikeCreationFailed;
            }

            return res; 
        }
    }
}