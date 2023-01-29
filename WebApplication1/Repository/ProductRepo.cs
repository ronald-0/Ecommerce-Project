using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Repository
{
    public class ProductRepo : IProduct
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public (Product product, bool successful, string message) AddProduct(CreateProductViewModel model)
        {
            Product proExists = _context.Products.Where(x => x.Name == model.Name).FirstOrDefault();
            if (proExists != null)
            {
                return (null, false, "Already exists");
            }
            Product pro = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId= model.CategoryId,
                Price= model.Price,
                LocationId= model.LocationId,
            };
            _context.Products.Add(pro);
            _context.SaveChanges();
            return (pro, true, "Product Created");
        }

        public (bool successful, string message) DeleteProductById(int id)
        {
            Product proExists = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (proExists == null)
            {
                return (false, $"Product with id : {id} does not exist");
            }
            _context.Products.Remove(proExists);
            _context.SaveChanges();

            return (true, "Product Successfully deleted");
        }

        public (List<Product> products, bool successful, string message) GetAllProducts()
        {
            List<Product> list = _context.Products.ToList();
            return (list, true, "Products returned successfully");
        }

        public (List<Product> products, bool successful, string message) GetAllProductsInDateRange(DateTime StartDate, DateTime EndDate)
        {
            var products = _context.Products.Where(x => x.DateCreated >= StartDate && x.DateCreated <= EndDate.AddHours(24)).ToList();

            return (products, true, "Products returned successfully");
        }

        public (ProductViewModel product, bool successful, string message) GetProductById(int id)
        {
            Product proExists = _context.Products.Where(x => x.Id == id).Include(x => x.Location).Include(x => x.Category).FirstOrDefault();
            if (proExists == null)
            {
                return (null, false, $"Product with id : {id} does not exist");
            }

            ProductViewModel prod = new ProductViewModel()
            {
                Id = proExists.Id,
                Name = proExists.Name,
                Description = proExists.Description,
                Category = new CategoryViewModel()
                {
                    Id = proExists.Category.Id,
                    Name = proExists.Category.Name,
                    Description = proExists.Description,
                    DateCreated = proExists.Category.DateCreated
                },
                CategoryId = proExists.CategoryId,
                Price = proExists.Price,
                Location = new LocationViewModel()
                {
                    Id = proExists.Location.Id,
                    Name = proExists.Location.Name,
                    DateCreated = proExists.Location.DateCreated
                },
                LocationId = proExists.LocationId,
                DateCreated= proExists.DateCreated,
            };

            return (prod, true, "Product Found");
        }

        public (Product product, bool successful, string message) UpdateProduct(CreateProductViewModel model, int id)
        {
            Product proExists = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (proExists == null)
            {
                return (null, false, $"Product with id : {id} does not exist");
            }
            proExists.Name = model.Name;
            proExists.Description = model.Description;
            _context.Products.Update(proExists);
            _context.SaveChanges();

            return (proExists, true, "Product Updated");
        }
    }
}
