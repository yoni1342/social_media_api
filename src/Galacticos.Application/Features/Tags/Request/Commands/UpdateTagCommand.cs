using Galacticos.Application.DTOs;
using MediatR;
using System;

namespace Galacticos.Application.Tags.Commands
{
    public class UpdateTagCommand : IRequest<TagDto>
    {
        public Guid TagId { get; set; }
        public string NewName { get; set; }
    }
}
