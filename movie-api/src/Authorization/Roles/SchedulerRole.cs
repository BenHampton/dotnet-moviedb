using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Roles;

public class SchedulerRole : RoleBase
{
    private readonly List<Permission> _permissions = new List<Permission>()
    {
        Permission.SCHEDULE_BUILD,
        Permission.SCHEDULE_CREATE,
        Permission.SCHEDULE_DELETE,
        Permission.SCHEDULE_POST,
        Permission.SCHEDULE_READ,
        Permission.SCHEDULE_UPDATE,
        Permission.LINE_CREATE_CHAIN,
        Permission.LINE_CREATE_LINK,
        Permission.LINE_SPEED_UP,
        Permission.LINE_SHUT_DOWN,
        Permission.LINE_SLOW_DOWN,
        Permission.SCHEDULE_NOTE_CREATE,
        Permission.SEQUENCE_MODIFY_TANKAGE_PARTY,
        Permission.CONFLICTS_VIEW,
        Permission.CONFLICTS_MANAGE,
    };

    public SchedulerRole() : base()
    {
        Permissions.AddRange(_permissions);
    }
}
