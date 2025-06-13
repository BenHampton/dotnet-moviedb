using movie_api.Authorization.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace movie_api.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class NeedsPermissionAttribute(Permission permission) : AuthorizeAttribute,
    IAuthorizationRequirement, IAuthorizationRequirementData
{
    public Permission Permission { get; set; } = permission;

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}
