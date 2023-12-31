using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Domain.Entities;
using MediatR;

namespace Galacticos.Application.Features.Profile.Request.Commands
{
    public class EditProfileRequest : IRequest<ErrorOr<ProfileResponseDTO>>
    {
        public Guid UserId { get; set; }
        public required EditProfileRequestDTO EditProfileRequestDTO { get; set; }

    }
}