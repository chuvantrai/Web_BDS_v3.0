using System.ComponentModel;

namespace WebBDS.Commons;

public enum UserRoleEnum
{
    [Description("admin")]
    Admin = 1,
    [Description("Khách hàng")]
    Customer
}