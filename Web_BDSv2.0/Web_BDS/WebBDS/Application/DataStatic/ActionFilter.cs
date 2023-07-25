using WebBDS.Application.SubModels;
using WebBDS.Emuns;

namespace WebBDS.Application.DataStatic;

public static class ActionFilter
{
    public static List<ActionAPI> AllActionModel { get; set; } = new List<ActionAPI>()
    {
        new ActionAPI
        {
            Action = ActionFilterEnum.ProductDetailData,
            UserRole = new[] { UserRoleEnum.Admin, UserRoleEnum.Customer }
        }
    };
    public static ActionAPI GetAllActionModel(ActionFilterEnum action)
    {
        return AllActionModel.FirstOrDefault(x => x.Action == action)!;
    }
}