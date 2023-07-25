using System.ComponentModel;

namespace WebBDS.Emuns;

public enum SortNewsEnum
{
    [Description("Mới nhất")]
    Latest = 1,
    [Description("Cũ nhất")]
    Oldest = 2
}