using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Common;

namespace Galacticos.Application.DTOs.Profile
{
    public class UpdatePasswordTokenRequestDTO
    {
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}