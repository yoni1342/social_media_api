using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Application.Features.Profile.Request.Queries;
using Galacticos.Application.Features.Profile.Request.Commands;
using System.Security.Claims;
using Galacticos.Application.Common.Interface.Authentication;
using Galacticos.Domain.Errors;
using Galacticos.Application.Persistence.Contracts;

namespace Galacticos.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        
        public ProfileController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            GetProfileRequest request = new GetProfileRequest()
            {
                UserId = id
            };
            ErrorOr<ProfileResponseDTO> result = await _mediator.Send(request);

            return result.Match<ActionResult>(
                profileResponseDTO => Ok(profileResponseDTO),
                error => BadRequest(error)
            );
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromForm] EditProfileRequestDTO editProfileRequestDTO)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            EditProfileRequest request = new EditProfileRequest()
            {
                UserId = Guid.Parse(userIdClaim),
                EditProfileRequestDTO = editProfileRequestDTO
            };
            ErrorOr<ProfileResponseDTO> result = await _mediator.Send(request);

            return result.Match<ActionResult>(
                profileResponseDTO => Ok(profileResponseDTO),
                error => BadRequest(error)
            );
        }

        [HttpPut("update-password")]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDTO updatePasswordRequestDTO)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            UpdatePasswordRequest request = new UpdatePasswordRequest()
            {
                UpdatePasswordRequestDTO = updatePasswordRequestDTO,
                UserId = Guid.Parse(userIdClaim)
            };

            ErrorOr<string> result = await _mediator.Send(request);

            return result.Match<ActionResult>(
                message => Ok(message),
                error => BadRequest(error)
            );
        }
      

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            GetAllProfileRequest request = new GetAllProfileRequest();
            ErrorOr<List<ProfileResponseDTO>> result = await _mediator.Send(request);

            return result.Match<ActionResult>(
                profileResponseDTOs => Ok(profileResponseDTOs),
                error => BadRequest(error)
            );
        }
    }
}