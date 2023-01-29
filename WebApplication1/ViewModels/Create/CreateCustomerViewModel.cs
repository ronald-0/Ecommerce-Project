using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.ViewModels.Create
{
    public class CreateCustomerViewModel
    {
        internal readonly string Id;

        [Required(ErrorMessage = "Field Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string Password { get; set; }

    }
}
