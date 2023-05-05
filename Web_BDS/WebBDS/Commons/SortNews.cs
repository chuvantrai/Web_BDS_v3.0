using System.ComponentModel;

namespace WebBDS.Commons;

public enum SortNews
{
    [Description("Mới nhất")]
    Latest = 1,
    [Description("Cũ nhất")]
    Oldest = 2
}