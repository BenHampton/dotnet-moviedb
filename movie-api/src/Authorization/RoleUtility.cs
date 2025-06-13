// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using movie_api.Authorization.Permissions;
using movie_api.Authorization.Roles;
using movie_api.Exceptions;

namespace movie_api.Authorization;

public static class RoleUtility
{
    private static readonly string _schedulerRole = "Scheduler";
    private static readonly string _reporterRole = "Reporter";
    private static readonly string _controllerRole = "Controller";
    private static readonly string _planningUtilizationRole = "Planning and Utilization";
    private static readonly string _administratorRole = "Administrator";

    private static readonly Dictionary<string, RoleBase> _roles = new Dictionary<string, RoleBase>()
    {
        {_reporterRole, new ReporterRole()},
        {_controllerRole, new ControllerRole()},
        {_planningUtilizationRole, new PlanningUtilizationRole()},
        {_schedulerRole, new SchedulerRole()},
        {_administratorRole, new AdministratorRole()},
    };

    public static RoleBase GetRole(string roleName)
    {
        var roleFound = _roles.ContainsKey(roleName);

        if (!roleFound)
        {
            throw new RoleNotFoundException($"Role {roleName} was not found in list");
        }

        return _roles[roleName];
    }

    public static bool DoesRoleHavePermission(string roleName, Permission permission)
    {
        var role = GetRole(roleName);
        return role.HasPermission(permission);
    }
}
