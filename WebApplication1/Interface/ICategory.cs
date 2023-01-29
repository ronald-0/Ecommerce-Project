using WebApplication1.Entities;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Interface
{
    public interface ICategory
    {
        (CategoryViewModel category, bool successful, string message) GetCategoryById(int id);
        (Category category, bool successful, string message) AddCategory(CreateCategoryViewModel model);
        (Category category, bool successful, string message) UpdateCategory(CreateCategoryViewModel model, int id);
        ( bool successful, string message) DeleteCategoryById(int id);
        (List<Category> categories, bool successful, string message) GetAllCategories();
        (List<Category> categories, bool successful, string message) GetAllCategoriesInDateRange(DateTime StartDate, DateTime EndDate);

    }
}
