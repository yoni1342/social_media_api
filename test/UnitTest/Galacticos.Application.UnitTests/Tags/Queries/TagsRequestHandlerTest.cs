using AutoMapper;
using Moq;
using Xunit;
using ErrorOr;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.UnitTests.Mocks;
using Galacticos.Application.Tags.Queries;
using Galacticos.Application.Profiles;
using Galacticos.Application.Features.Tags.Request.Queries;
using Galacticos.Application.DTOs;

namespace Galacticos.Application.UnitTests.Tags.Queries
{
    public class TagsRequestHandlerTest
    {
        private readonly Mock<ITagRepository> _mockTagRepository;
        private readonly IMapper _mockMapper;

        public TagsRequestHandlerTest()
        {
            _mockTagRepository = MockRepositories.TagRepository();
            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TagMappingProfile());
            }).CreateMapper();
        }

        [Fact]
        public async void GetAllTagsHandler_Success()
        {
            var handler = new GetAllTagsQueryHandler(_mockTagRepository.Object, _mockMapper);

            var command = new GetAllTagsQuery();

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<List<TagDto>>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count, 2);
        }

        [Fact]
        public async void GetAllTagsHandler_Failure()
        {
            var handler = new GetAllTagsQueryHandler(_mockTagRepository.Object, _mockMapper);

            var command = new GetAllTagsQuery();

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<List<TagDto>>(result);
            Assert.NotEqual(result.Count, 1);
        }

        [Fact]
        public async void GetTagByIdHandler_Success()
        {
            var handler = new GetTagByIdQueryHandler(_mockTagRepository.Object, _mockMapper);

            var command = new GetTagByIdQuery { TagId = new Guid("00000000-0000-0000-0000-000000000000") };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<TagDto>(result);
            Assert.Equal(result.Id, new Guid("00000000-0000-0000-0000-000000000000"));
        }

        [Fact]
        public async void GetTagByIdHandler_Failure()
        {
            var handler = new GetTagByIdQueryHandler(_mockTagRepository.Object, _mockMapper);

            var command = new GetTagByIdQuery { TagId = new Guid("d9b6c0b7-9b9a-4e5a-8b3a-3b9b6c0b7a9a") };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<TagDto>(result);
            Assert.NotEqual(result.Id, new Guid("d9b6c0b7-9b9a-4e5a-8b3a-3b9b6c0b7a9b"));
        }        
    }
}