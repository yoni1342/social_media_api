using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Profile;
using MediatR;

namespace Galacticos.Application.Features.Profile.Request.Commands
{
    public class UpdatePasswordTokenRequest : IRequest<ErrorOr<string>>
    {
        public string Token { get; set; } = null!;
        public UpdatePasswordTokenRequestDTO updatePasswordTokenRequestDTO { get; set; } = null!;
        
    }
}