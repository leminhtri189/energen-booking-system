using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Commons
{
    public sealed record Error(string Code, string? Description = null)
    {
        public const string _OperationFailed = "OperationFailedError";
        public const string _ValidationFailed = "ValidationError";
        public const string _AlreadyExisted = "EntityExistedError";
        public const string _NotExisted = "EntityNotExistedError";
        public const string _Unauthorized = "InvalidPermissionError";

        public static readonly Error NoError = new(string.Empty);

        public static Error OperationFailed(string description) => new Error(_OperationFailed, description);
        public static Error ValidationFailed(string description) => new Error(_ValidationFailed, description);
        public static Error Existed(string description) => new Error(_AlreadyExisted, description);
        public static Error NotExisted(string description) => new Error(_NotExisted, description);
        public static Error Unauthorized(string description) => new Error(_Unauthorized, description);
    };
}
