using AutoMapper;
using Galacticos.Application.DTOs.Auth;
using Galacticos.Application.Contract.Authentication;
using Galacticos.Application.DTOs.Users;
using Galacticos.Application.Features.Auth.Requests.Commands;
using Galacticos.Application.Features.Auth.Requests.Queries;
using Galacticos.Domain.Entities;
using Galacticos.Application.Features.Profile.Request.Commands;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Application.DTOs.Relations;

namespace Galacticos.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<RegisterCommand, User>();
        CreateMap<LoginRequest, LoginQuery>();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UpdatePasswordTokenRequestDTO, User>().ReverseMap();
        CreateMap<UpdatePasswordRequestDTO, User>().ReverseMap();
        CreateMap<UpdatePasswordTokenRequest, UpdatePasswordTokenRequestDTO>().ReverseMap();
        CreateMap<UpdatePasswordRequest, UpdatePasswordRequestDTO>().ReverseMap();
        CreateMap<UpdatePasswordTokenRequest, User>();
        CreateMap<UpdatePasswordRequest, User>();
        CreateMap<User, GetFollowersDTO>().ReverseMap();
    }
}