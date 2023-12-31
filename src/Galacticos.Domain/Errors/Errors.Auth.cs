using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Galacticos.Domain.Errors
{
    public static partial class Errors
    {
        public static class Auth
        {
            public static Error WrongCreadital =>
            Error.Validation(code: "Auth.InvalidCredentials", description: "Invalid Credentials");

            public static Error UsernameEmailrequired =>
            Error.Validation(code: "Auth.UsernameEmailRequired", description: "Username or Email is required");

            public static Error InvalidToken =>
            Error.Validation(code: "Thetokenisinvalid", description: "You enterd wrong token");
            public static Error EmailAlreadyConfirmed =>
            Error.Validation(code: "EmailAlreadyConfirmed", description: "Email is already confirmed");
            public static Error EmailNotConfirmed =>
            Error.Validation(code: "EmailNotConfirmed", description: "Email is not confirmed");
        }
    }
}