// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Roles;

public abstract class RoleBase
{
    public List<Permission> Permissions { get; protected set; } = new List<Permission>();

    protected RoleBase()
    {
    }

    public bool HasPermission(Permission permissionToCheck)
        => Permissions.Contains(permissionToCheck);


}
