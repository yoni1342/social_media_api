using Xunit;
using Moq;
using AutoMapper;
using Galacticos.Application.Profiles;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.UnitTests.Mocks;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Application.Features.Comments.Handler.Command;
using Galacticos.Application.DTOs.Comments;
using ErrorOr;
using Galacticos.Application.Features.Comments.Handler.Query;
using Galacticos.Application.Features.Comments.Request.Queries;

namespace Galacticos.Application.UnitTests.Comments.Queries
{
    public class CommentHandlerTest
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly IMapper _mapper;

        public CommentHandlerTest()
        {
            _commentRepository = MockRepositories.CommentRepository();
            _postRepository = MockRepositories.PostRepository();
            _userRepository = MockRepositories.UserRepository();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CommentMappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetCommentByIdHandler_Success()
        {
            var handler = new GetCommentByIdRequestHandler(_commentRepository.Object, _mapper);

            var command = new GetCommentByIdRequest
            {
                Id = new Guid("00000000-0000-0000-0000-000000000000"),
            };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<ErrorOr<CommentResponesDTO>>(result);
        }


        [Fact]
        public async Task CreateCommentHandler_Success()
        {
            var handler = new CreateCommentHandler(_commentRepository.Object, _mapper, _postRepository.Object, _userRepository.Object);

            var command = new CreateCommentCommand
            {
                Content = "This is a comment",
                PostId = new Guid("00000000-0000-0000-0000-000000000000"),
                UserId = new Guid("00000000-0000-0000-0000-000000000000"),
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<ErrorOr<CommentResponesDTO>>(result);
        }


        [Fact]
        public async Task UpdateCommentHandler_Success()
        {
            var handler = new UpdateCommentHandler(_commentRepository.Object, _mapper);

            var command = new UpdateCommentCommand
            {
                CommentId = new Guid("00000000-0000-0000-0000-000000000000"),
                UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                Content = "This is a new comment",
            };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<ErrorOr<CommentResponesDTO>>(result);
            Assert.Equal("This is a new comment", result.Value.Content);
        }


        [Fact]
        public async Task DeleteCommentHandler_Success()
        {
            var handler = new DeleteCommentHandler(_commentRepository.Object, _mapper);

            var command = new DeleteCommentRequest
            {
                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                UserId = new Guid("00000000-0000-0000-0000-000000000000"),
            };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<ErrorOr<bool>>(result);
        }


        [Fact]
        public async Task GetCommentsByPostIdHandler_Success()
        {
            var handler = new GetCommentsByPostIdRequestHandler(_commentRepository.Object, _mapper, _postRepository.Object);

            var command = new GetCommentsByPostIdRequest
            {
                PostId = new Guid("00000000-0000-0000-0000-000000000000"),
            };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<ErrorOr<List<CommentResponesDTO>>>(result);
        }
    }
}