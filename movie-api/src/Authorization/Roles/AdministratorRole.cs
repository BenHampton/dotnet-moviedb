using movie_api.Authorization.Permissions;

namespace movie_api.Authorization.Roles;

public class AdministratorRole : RoleBase
{
    public AdministratorRole()
    {
        Permissions.Add(Permission.ADMINISTRATOR);
    }
}
