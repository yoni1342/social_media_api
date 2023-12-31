using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Galacticos.Domain.Errors
{
    public static partial class Errors
    {
        public static class Comment
        {
            public static Error CommentCreationFailed =>
            Error.Failure(code: "Comment.FailCreating", description: "Saving Comment to DB Error");

            public static Error CommentNotFound =>
            Error.Failure(code: "Comment.NotFound", description: "Comment not found");
            public static Error CommentIsNotYours =>
            Error.Validation(code: "PostIsNotYours", description: "Post is not yours");

            public static Error InvalidComment =>
            Error.Failure(code: "Comment.Invalid", description: "Invalid Comment");
        }
    }
}