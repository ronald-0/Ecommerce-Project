using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Repository
{
    public class CategoryRepo : ICategory
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public (Category category, bool successful, string message) AddCategory(CreateCategoryViewModel model)
        {
            Category catExists = _context.Categories.Where(x => x.Name== model.Name).FirstOrDefault();
            if (catExists != null)
            {
                return (null, false, "Already exists");
            }
            Category cat = new Category()
            {
                Name= model.Name,
                Description= model.Description,
            };
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return (cat, true, "Category Created");
        }

        public ( bool successful, string message) DeleteCategoryById(int id)
        {
            Category catExists = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (catExists == null)
            {
                return ( false, $"Category with id : {id} does not exist");
            }
            _context.Categories.Remove(catExists);
            _context.SaveChanges();

            return (true, "Category Successfully deleted");
        }

        public (List<Category> categories, bool successful, string message) GetAllCategories()
        {
            List<Category> list = _context.Categories.ToList();
            return (list, true, "Categories returned successfully");
        }

        public (List<Category> categories, bool successful, string message) GetAllCategoriesInDateRange(DateTime StartDate, DateTime EndDate)
        {
            var categories = _context.Categories.Where(x => x.DateCreated >= StartDate && x.DateCreated <= EndDate.AddHours(24)).ToList();

            return (categories, true, "Categories returned successfully");
        }

        public (CategoryViewModel category, bool successful, string message) GetCategoryById(int id)
        {
            Category catExists = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (catExists == null)
            {
                return (null, false, $"Category with id : {id} does not exist");
            }

            CategoryViewModel cat = new CategoryViewModel()
            {
                Id = catExists.Id,
                Name = catExists.Name,
                Description = catExists.Description,
            };
            return (cat, true, "Category Found");
        }

        public (Category category, bool successful, string message) UpdateCategory(CreateCategoryViewModel model, int id)
        {
            Category catExists = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (catExists == null)
            {
                return (null, false, $"Category with id : {id} does not exist");
            }
            catExists.Name= model.Name;
            catExists.Description= model.Description;
            _context.Categories.Update(catExists);
            _context.SaveChanges();

            return (catExists, true, "Category Updated");
        }
    }
}
