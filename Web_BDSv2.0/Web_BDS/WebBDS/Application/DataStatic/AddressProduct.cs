using WebBDS.Emuns;
using WebBDS.Extensions;
using WebBDS.Models;

namespace WebBDS.Application.DataStatic;

public class AddressProduct
{
    public static List<Regional> AllAddressProduct { get; set; } = new List<Regional>()
    {
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.CamLam,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.CamLam)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.DiemKhanh,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.DiemKhanh)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.KhanhVinh,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.KhanhVinh)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.NhaTrang,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.NhaTrang)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.CamRang,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.CamRang)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.NinhHoa,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.NinhHoa)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.VanNinh,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.VanNinh)
        },
        new Regional
        {
            RegionalId = (int)RegionalProductEnum.Khac,
            RegionalName = ExpressionLogic.GetEnumDescription(RegionalProductEnum.Khac)
        }
    };

    public static Regional GetAddressProduct(int regionalId)
    {
        return AllAddressProduct.FirstOrDefault(x => x.RegionalId == regionalId)!;
    }
}