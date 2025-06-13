using System.Security.Claims;
using movie_api.Authorization.Attributes;
using movie_api.Authorization.Permissions;
using movie_api.Authorization.Roles;
using Microsoft.AspNetCore.Authorization;
using movie_api.Authorization;
using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Handlers;

public class PermissionHandler : AuthorizationHandler<NeedsPermissionAttribute>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NeedsPermissionAttribute requirement)
    {
        var claimRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role);

        if (claimRole is null)
        {
            return Task.CompletedTask;
        }

        var userRole = RoleUtility.GetRole(claimRole.Value);

        if (userRole.HasPermission(Permission.ADMINISTRATOR))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var requiredPermission = requirement.Permission;

        if (userRole.HasPermission(requiredPermission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
