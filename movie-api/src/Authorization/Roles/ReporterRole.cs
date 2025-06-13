using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Roles;

public class ReporterRole : RoleBase
{
    private readonly List<Permission> _permissions = new List<Permission>()
    {
        Permission.SCHEDULE_READ
    };

    public ReporterRole()
    {
        Permissions.AddRange(_permissions);
    }
}
