using System.ComponentModel;

namespace WebBDS.Emuns;

public enum SortProductEnum
{
    [Description("Mới nhất")]
    NewProduct = 1,
    [Description("Giá thấp đến cao")]
    PriceHigh = 2,
    [Description("Giá cao đến thấp")]
    PriceLow = 3,
    [Description("Giá trên m² thấp")]
    PriceMeterLow = 4,
    [Description("Giá trên m² cao")]
    PriceMeterHigh = 5,
    [Description("Diện tích bé đến lớn")]
    AcreageHigh = 6,
    [Description("Diện tích lớn đến bé")]
    AcreageLow = 7
}

