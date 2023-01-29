namespace WebApplication1.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }

    }
}
