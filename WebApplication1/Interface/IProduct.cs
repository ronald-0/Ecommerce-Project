using WebApplication1.Entities;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Interface
{
    public interface IProduct
    {
        (ProductViewModel product, bool successful, string message) GetProductById(int id);
        (Product product, bool successful, string message) AddProduct(CreateProductViewModel model);
        (Product product, bool successful, string message) UpdateProduct(CreateProductViewModel model, int id);
        (bool successful, string message) DeleteProductById(int id);
        (List<Product> products, bool successful, string message) GetAllProducts();
        (List<Product> products, bool successful, string message) GetAllProductsInDateRange(DateTime StartDate, DateTime EndDate);
    }
}
