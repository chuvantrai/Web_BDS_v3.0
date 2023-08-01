using WebBDS.Emuns;
using WebBDS.Extensions;
using WebBDS.Models;

namespace WebBDS.Application.DataStatic;

public class CategoryProduct
{
    public static List<Category> AllCategoryProduct { get; set; } = new List<Category>()
    {
        new Category
        {
            CategoryId = (int)CategoryProductEnum.CanHo,
            CategoryName = ExpressionLogic.GetEnumDescription(CategoryProductEnum.CanHo)
        },
        new Category
        {
            CategoryId = (int)CategoryProductEnum.BietThu,
            CategoryName = ExpressionLogic.GetEnumDescription(CategoryProductEnum.BietThu)
        },
        new Category
        {
            CategoryId = (int)CategoryProductEnum.DatNen,
            CategoryName = ExpressionLogic.GetEnumDescription(CategoryProductEnum.DatNen)
        },
        new Category
        {
            CategoryId = (int)CategoryProductEnum.NhaPho,
            CategoryName = ExpressionLogic.GetEnumDescription(CategoryProductEnum.NhaPho)
        }
    };
    public static Category GetCategoryById(int categoryId)
    {
        return AllCategoryProduct.FirstOrDefault(x => x.CategoryId == categoryId)!;
    }
}