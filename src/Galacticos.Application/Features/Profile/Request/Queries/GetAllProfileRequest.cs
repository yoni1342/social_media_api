using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Profile;
using MediatR;

namespace Galacticos.Application.Features.Profile.Request.Queries
{
    public class GetAllProfileRequest : IRequest<List<ProfileResponseDTO>>
    {

    }
}