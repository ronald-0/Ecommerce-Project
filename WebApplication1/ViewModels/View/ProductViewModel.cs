using WebApplication1.Entities;

namespace WebApplication1.ViewModels.View
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
