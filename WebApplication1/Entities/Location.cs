using WebApplication1.ViewModels.View;

namespace WebApplication1.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
