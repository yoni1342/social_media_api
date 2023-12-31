using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.Services.Authentication;
using MediatR;

namespace Galacticos.Application.Features.Auth.Requests.Commands
{
    public class VerifyEmailCommand : IRequest<ErrorOr<AuthenticationResult>>
    {
        public string Token { get; set; }
    }
}