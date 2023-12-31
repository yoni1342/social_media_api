using MediatR;
using Galacticos.Application.DTOs.Relations;


namespace Galacticos.Application.Features.Relation.Request.Command
{
    public class FollowCommand : IRequest<GetFollowersDTO>
    {
        public RelationDTO RelationDTO { get; set; }
    }
}