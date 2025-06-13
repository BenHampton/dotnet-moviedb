using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Roles;

public class PlanningUtilizationRole : RoleBase
{
    private readonly List<Permission> _permissions = new List<Permission>()
    {
        Permission.SCHEDULE_READ,
        Permission.LINE_MAINTENANCE_CREATE,
        Permission.LINE_MAINTENANCE_DELETE,
        Permission.LINE_MAINTENANCE_READ,
        Permission.LINE_MAINTENANCE_UPDATE,
        Permission.TRACKER_CREATE,
        Permission.TRACKER_DELETE,
        Permission.TRACKER_READ,
        Permission.TRACKER_UPDATE,
    };

    public PlanningUtilizationRole() : base()
    {
        Permissions.AddRange(_permissions);
    }
}
