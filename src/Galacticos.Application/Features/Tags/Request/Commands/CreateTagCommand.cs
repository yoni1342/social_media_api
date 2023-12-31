using Galacticos.Application.DTOs;
using MediatR;
using System;

namespace Galacticos.Application.Tags.Commands
{
    public class CreateTagCommand : IRequest<TagDto>
    {
        public string Name { get; set; }
    }
}
