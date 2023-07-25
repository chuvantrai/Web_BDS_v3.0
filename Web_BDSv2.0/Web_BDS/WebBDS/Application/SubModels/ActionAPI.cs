using WebBDS.Emuns;

namespace WebBDS.Application.SubModels;

public class ActionAPI
{
    public ActionFilterEnum Action { get; set; }
    public UserRoleEnum[]? UserRole { get; set; }
    public bool CheckAllRole { get; set; } = false; // false chi can thoa man 1 userRole
}