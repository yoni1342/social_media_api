using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galacticos.Application.Common.Interface.Authentication
{
    public interface IJwtTokenValidation
    {
        bool ValidateToken(string token);
        Guid ExtractUserIdFromToken(string token);
        string ExtractEmailFromToken(string token);
    }
}