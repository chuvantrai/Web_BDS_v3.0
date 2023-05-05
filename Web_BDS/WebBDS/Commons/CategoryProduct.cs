using System.ComponentModel;

namespace WebBDS.Commons;

public enum CategoryProduct
{
    [Description("Căn hộ")]
    CanHo = 1,
    [Description("Đất nền")]
    DatNen = 2,
    [Description("Nhà Phố")]
    NhaPho = 3,
    [Description("Biệt Thự")]
    BietThu = 4,
}