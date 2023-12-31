using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Galacticos.Domain.Errors
{
    public static partial class Errors
    {
        public static class Post
        {
            public static Error PostNotFound =>
            Error.Failure(code: "PostNotFound", description: "Post not found");

            public static Error PostNotCreated =>
            Error.Failure(code:"PostNotCreared", description: "Post not Created");

            public static Error PostIsNotYours =>
            Error.Validation(code: "PostIsNotYours", description: "Post is not yours");
            
            

            public static Error InvalidPost =>
            Error.Failure(code: "InvalidPost", description: "Invalid Post");

            public static Error InappropriateContent =>
            Error.Failure(code: "InappropriateContent", description: "Your post contains inappropriate content and has been removed. Please review our community guidelines and policies for more information.");
        }
    }
}