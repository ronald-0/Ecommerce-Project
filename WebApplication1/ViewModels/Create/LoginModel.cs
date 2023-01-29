using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Create
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Field Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string Password { get; set; }
    }
}
