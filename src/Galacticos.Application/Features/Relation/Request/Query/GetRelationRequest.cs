using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Relations;
using MediatR;

namespace Galacticos.Application.Features.Relation.Request.Query
{
    public class GetRelationRequest : IRequest<GetFollowersDTO>
    {
        public RelationDTO RelationDTO { get; set; }
    }
}