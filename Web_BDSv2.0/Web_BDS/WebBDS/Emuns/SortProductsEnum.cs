using System.ComponentModel;

namespace WebBDS.Emuns;

public enum SortProductsEnum
{
    [Description("Mới nhất")]
    Latest = 1,
    [Description("Cũ nhất")]
    Oldest = 2,
    [Description("Giá cao")]
    HighPrice = 3,
    [Description("Giá thấp")]
    LowPrice = 4,
    [Description("Diện tích lớn")]
    LargeArea = 5,
    [Description("Diện tích bé")]
    SmallArea = 6,
    [Description("Chiều ngang lớn")]
    LargeWidth = 7,
    [Description("Chiều ngang bé")]
    SmallWidth = 8
}