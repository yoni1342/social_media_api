using Xunit;
using Moq;
using AutoMapper;
using Galacticos.Application.Features.Likes.Handler.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.UnitTests.Mocks;
using Galacticos.Application.Profiles;
using Galacticos.Application.Features.Likes.Command.Queries;
using Galacticos.Application.DTOs.Likes;
using ErrorOr;

namespace Galacticos.Application.UnitTests.Likes.Queries
{
    public class LikesRequestHandlerTest
    {
        private readonly Mock<ILikeRepository> _likeRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly IMapper _mapper;

        public LikesRequestHandlerTest()
        {
            _likeRepository = MockRepositories.LikeRepository();
            _userRepository = MockRepositories.UserRepository();
            _postRepository = MockRepositories.PostRepository();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LikeMappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateLikesRequestHandler_Success()
        {
            var handler = new LikePostRequestHandler(_likeRepository.Object, _mapper, _userRepository.Object, _postRepository.Object);

            var postId = new Guid("11111111-0000-0000-0000-000000000000");
            var userId = new Guid("00000000-0000-0000-0000-000000000000");

            var request = new LikePostRequest()
            {
                PostId = postId,
                UserId = userId
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorOr<LikeResponseDto>>(result);
            var like = _likeRepository.Object.GetAllLikes();
            Assert.Equal(2, like.Result.Count);
            
        }

        [Fact]
        public async Task CreateLikeRequestHandler_Failure()
        {
            var handler = new LikePostRequestHandler(_likeRepository.Object, _mapper, _userRepository.Object, _postRepository.Object);

            var postId = new Guid("11111111-0000-0000-0000-000000000000");
            var userId = new Guid("00000000-0000-0000-1234-000000000000");

            var request = new LikePostRequest()
            {
                PostId = postId,
                UserId = userId
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorOr<LikeResponseDto>>(result);
            var like = _likeRepository.Object.GetAllLikes();
            Assert.Equal(1, like.Result.Count);
        }


        [Fact]
        public async Task LikeAndUnlikePostRequestHandler_Success()
        {
            var handler = new LikePostRequestHandler(_likeRepository.Object, _mapper, _userRepository.Object, _postRepository.Object);

            var postId = new Guid("00000000-0000-0000-0000-000000000000");
            var userId = new Guid("00000000-0000-0000-0000-000000000000");

            var request = new LikePostRequest()
            {
                PostId = postId,
                UserId = userId
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorOr<LikeResponseDto>>(result);
            var like = _likeRepository.Object.GetAllLikes();
            Assert.Equal(2, like.Result.Count);

            // Act
            var handler2 = new DislikePostRequestHandler(_likeRepository.Object, _mapper);
            var request2 = new DislikePostRequest()
            {
                PostId = postId,
                UserId = userId
            };

            var result2 = await handler2.Handle(request2, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorOr<bool>>(result2);
            var like2 = _likeRepository.Object.GetAllLikes();
            Assert.Equal(1, like2.Result.Count);
        }
    }
}