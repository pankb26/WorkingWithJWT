//using System;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;
//using JWTHLAPI.DataLayer.Interfaces.Auth;
//using JWTHLAPI.ModelLayer.Common;

//namespace JWTHLAPI.Helpers
//{
//    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
//    public class HasPermissionAttribute : Attribute, IAsyncAuthorizationFilter
//    {
//        private readonly string _formName;
//        private readonly PermissionType _permission;

//        public HasPermissionAttribute(string formName, PermissionType permission)
//        {
//            _formName = formName;
//            _permission = permission;
//        }

//        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//        {
//            var user = context.HttpContext.User;

//            if (user?.Identity == null || !user.Identity.IsAuthenticated)
//            {
//                context.Result = new UnauthorizedResult();
//                return;
//            }

//            var roleIdClaim = user.FindFirst("roleId");

//            if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int roleId))
//            {
//                context.Result = new ForbidResult();
//                return;
//            }

//            var permissionRepository =
//                context.HttpContext.RequestServices
//                    .GetRequiredService<IPermissionRepository>();

//            bool hasPermission = await permissionRepository.HasPermission(
//                roleId,
//                _formName,
//                _permission
//            );

//            if (!hasPermission)
//            {
//                context.Result = new ForbidResult();
//            }
//        }
//    }
//}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.Common;

namespace JWTHLAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HasPermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _formName;
        private readonly PermissionType _permission;

        public HasPermissionAttribute(string formName, PermissionType permission)
        {
            _formName = formName;
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(
                    new ApiResponse(false, "User is not authenticated")
                );
                return;
            }

            var roleIdClaim = user.FindFirst("roleId");
            if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int roleId))
            {
                context.Result = new ObjectResult(
                    new ApiResponse(false, "Invalid role information")
                )
                {
                    StatusCode = 403
                };
                return;
            }

            var permissionRepo =
                context.HttpContext.RequestServices
                    .GetRequiredService<IPermissionRepository>();

            bool allowed = await permissionRepo.HasPermission(
                roleId,
                _formName,
                _permission
            );

            if (!allowed)
            {
                context.Result = new ObjectResult(
                    new ApiResponse(
                        false,
                        $"Access denied for {_formName} ({_permission})"
                    )
                )
                {
                    StatusCode = 403
                };
            }
        }
    }
}