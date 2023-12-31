using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Application.Features.Profile.Request.Commands;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Profiles
{
    public class UserProfileMappingAutomapper : Profile
    {
        public UserProfileMappingAutomapper()
        {
            CreateMap<User, ProfileResponseDTO>();
            
            CreateMap<EditProfileRequestDTO, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, srcMember) => {
                    if (srcMember == null)
                    {
                        return false;
                    }
                    if (srcMember.GetType()==typeof(string) && string.IsNullOrEmpty((string)srcMember))
                    {
                        return false;
                    }
                    return true;
                }
            ));
        }
    }
}