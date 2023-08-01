using System.ComponentModel;

namespace WebBDS.Emuns;

public enum RegionalProductEnum
{
    [Description("Cam lâm")]
    CamLam = 1,
    [Description("Diêm Khánh")]
    DiemKhanh = 2,
    [Description("Khánh Vĩnh")]
    KhanhVinh = 3,
    [Description("Nha Trang")]
    NhaTrang = 4,
    [Description("Cam Rang")]
    CamRang = 5,
    [Description("Ninh Hòa")]
    NinhHoa = 6,
    [Description("Vạn Ninh")]
    VanNinh = 7,
    [Description("Khu Vực Khác")]
    Khac = 8
}