using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Entities
{
    public class Customer: IdentityUser
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
