using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Profile;
using MediatR;

namespace Galacticos.Application.Features.Profile.Request.Commands
{
    public class UpdatePasswordRequest : IRequest<ErrorOr<string>>
    {
        

        public Guid UserId { get; set; }
        public UpdatePasswordRequestDTO UpdatePasswordRequestDTO { get; set; } = null!;
    }
}