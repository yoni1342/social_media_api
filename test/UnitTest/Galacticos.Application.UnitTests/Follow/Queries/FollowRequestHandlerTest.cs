using Moq;
using Xunit;
using AutoMapper;
using Galacticos.Application.Features.Relation.Handler.Command;
using Galacticos.Application.Features.Relation.Request.Command;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Profiles;
using Galacticos.Application.DTOs.Relations;
using Galacticos.Application.UnitTests.Mocks;
using MediatR;

namespace Galacticos.Application.UnitTests.Follows.Queries
{
    public class FollowRequestHandlerTest
    {
        private readonly Mock<IRelationRepository> _relationRepository;
        private readonly IMapper _mapper;

        public FollowRequestHandlerTest()
        {
            _relationRepository = MockRepositories.RelationRepository();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RelationMappingProfile());
            });

            _mapper = mockMapper.CreateMapper();
        }


        [Fact]
        public async void FollowRequestHandler_Fail()
        {
            // Arrange
            var handler = new FollowCommandHandler(_mapper, _relationRepository.Object);
            var RelationDTO = new RelationDTO
            {
                FollowerId = new Guid("11111111-1111-1111-1111-111111111111"),
                FollowedUserId = new Guid("11111111-1111-1111-1111-111111111111"),
            };

            var command = new FollowCommand { RelationDTO = RelationDTO };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));
            
        }


        [Fact]
        public async void UnFollowRequestHandler_Fail()
        {
            // Arrange
            var handler = new UnFollowCommandHandler(_mapper, _relationRepository.Object);
            var RelationDTO = new RelationDTO
            {
                FollowerId = new Guid("11111111-1111-1111-1111-111111111111"),
                FollowedUserId = new Guid("11111111-1111-1111-1111-111111111111"),
            };

            var command = new UnFollowCommand { RelationDTO = RelationDTO };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));

        }


        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        // [Fact]
        // public async void FollowRequestHandler_Success()
        // {
        //     // Arrange
        //     var handler = new FollowCommandHandler(_mapper, _relationRepository.Object);
        //     var RelationDTO = new RelationDTO
        //     {
        //         FollowerId = new Guid("33333333-3333-3333-3333-333333333333"),
        //         FollowedUserId = new Guid("00000000-0000-0000-0000-000000000000"),
        //     };
            
        //     var command = new FollowCommand { RelationDTO = RelationDTO };

        //     // Act
        //     var result = await handler.Handle(command, CancellationToken.None);

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.IsType<GetFollowersDTO>(result);
        // }


        // [Fact]
        // public async void UnFollowRequestHandler_Success()
        // {
        //     // Arrange
        //     var handler = new UnFollowCommandHandler(_mapper, _relationRepository.Object);
        //     var RelationDTO = new RelationDTO
        //     {
        //         FollowerId = new Guid("11111111-1111-1111-1111-111111111111"),
        //         FollowedUserId = new Guid("22222222-2222-2222-2222-222222222222"),
        //     };

        //     var command = new UnFollowCommand { RelationDTO = RelationDTO };

        //     // Act
        //     var result = await handler.Handle(command, CancellationToken.None);

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.IsType<GetFollowersDTO>(result);

        // }
    }
}