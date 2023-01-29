using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Create
{
    public class CreateLocationViewModel
    {
        [Required(ErrorMessage = "Field Required")]
        public string Name { get; set; }

    }
}
