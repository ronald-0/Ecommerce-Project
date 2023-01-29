using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Create
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Field Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string Description { get; set; }

    }
}
