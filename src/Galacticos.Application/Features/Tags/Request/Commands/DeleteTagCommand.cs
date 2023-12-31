using MediatR;
using System;

namespace Galacticos.Application.Tags.Commands
{
    public class DeleteTagCommand : IRequest
    {
        public Guid TagId { get; set; }
    }
}
