using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Galacticos.Domain.Errors
{
    public static partial class Errors
    {
        public static class Like
        {
            public static Error DidNotLiked =>
            Error.Failure(code: "DidNotLiked", description: "You did not liked this post");

            public static Error LikeCreationFailed =>
            Error.Failure(code:"LikeCreationFailed", description: "Like not Created");
        }
    }
}