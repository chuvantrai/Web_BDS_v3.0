using System.ComponentModel;

namespace WebBDS.Commons;

public enum RegionalProduct
{
    [Description("Khu Vực Khác")]
    Khac = 0,
    [Description("Nha Trang")]
    CanHo = 1,
    [Description("Cam Ranh")]
    DatNen = 2,
    [Description("Ninh Hòa")]
    NhaPho = 3,
    // [Description("Diên Khánh")]
    // BietThu = 4,
    // [Description("Khánh Sơn")]
    // BietThu = 4,
    // [Description("Khánh Vĩnh")]
    // BietThu = 4,
    // [Description("Cam Lâm")]
    // BietThu = 4,
    // [Description("Diên Khánh")]
    // BietThu = 4,
}