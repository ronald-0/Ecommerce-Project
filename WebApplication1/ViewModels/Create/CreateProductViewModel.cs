using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.ViewModels.Create
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Field Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public int LocationId { get; set; }

    }
}
