using System.ComponentModel;

namespace WebBDS.Emuns;

public enum UserRoleEnum
{
    [Description("admin")]
    Admin = 1,
    [Description("Khách hàng")]
    Customer
}