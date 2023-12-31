using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Profile;
using MediatR;
using ErrorOr;

namespace Galacticos.Application.Features.Profile.Request.Queries
{
    public class GetProfileRequest : IRequest<ErrorOr<ProfileResponseDTO>>
    {
        public Guid UserId { get; set; }
    }
}