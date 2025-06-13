using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Roles;

public class ControllerRole : RoleBase
{
    private readonly List<Permission> _permissions = new List<Permission>()
    {
        Permission.SCHEDULE_READ,
        Permission.TRACKER_CREATE,
        Permission.TRACKER_DELETE,
        Permission.TRACKER_READ,
        Permission.TRACKER_UPDATE,
        Permission.CONTROLLER_SCHEDULE_BUILD,
        Permission.CONTROLLER_SCHEDULE_CREATE,
        Permission.CONTROLLER_SCHEDULE_DELETE,
        Permission.CONTROLLER_SCHEDULE_POST,
        Permission.CONTROLLER_SCHEDULE_READ,
        Permission.CONTROLLER_SCHEDULE_UPDATE,
        Permission.LINE_SPEED_UP,
        Permission.LINE_SHUT_DOWN,
        Permission.LINE_SLOW_DOWN,
        Permission.SCHEDULE_NOTE_CREATE,
        Permission.SEQUENCE_MODIFY_TANKAGE_PARTY,
    };

    public ControllerRole() : base()
    {
        Permissions.AddRange(_permissions);
    }
}
