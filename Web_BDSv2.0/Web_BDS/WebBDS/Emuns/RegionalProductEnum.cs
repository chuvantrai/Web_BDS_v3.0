using System.ComponentModel;

namespace WebBDS.Emuns;

public enum RegionalProductEnum
{
    [Description("Khu Vực Khác")]
    Khac = 0,
    [Description("Nha Trang")]
    CanHo = 1,
    [Description("Cam Ranh")]
    DatNen = 2,
    [Description("Ninh Hòa")]
    NhaPho = 3
}