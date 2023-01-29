using WebApplication1.Entities;

namespace WebApplication1.ViewModels.View
{
    public class CustomerVIewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public LocationViewModel Location { get; set; }
        public int LocationId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
