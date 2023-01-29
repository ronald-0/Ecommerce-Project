using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Repository
{
    public class AuthenticationRepo : IAuthentication
    {
        private readonly UserManager<Customer> _userManager;
        private readonly AppDbContext _context;

        public AuthenticationRepo(UserManager<Customer> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<(Customer customer, bool successful, string message)> AddCustomer(CreateCustomerViewModel model)
        {
            Customer cusExists = await _userManager.FindByEmailAsync(model.Email);
            if (cusExists != null)
            {
                return (null, false, "Already exists");
            }
            Customer cust = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                LocationId = model.LocationId,
                UserName= model.Email,
                DateOfBirth= model.DateOfBirth,
            };
            var result = await _userManager.CreateAsync(cust, model.Password);
            if(result.Succeeded)
            {
                return (cust, true, "Customer Created");
            }
            else
            {
                return (null, false, "Not Created");
            }
        }

        public (bool successful, string message) DeleteCustomerById(string id)
        {
            Customer cusExists = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (cusExists == null)
            {
                return (false, $"Customer with id : {id} does not exist");
            }
            _context.Users.Remove(cusExists);
            _context.SaveChanges();

            return (true, "Customer Successfully Deleted");
        }

        public (List<Customer> customers, bool successful, string message) GetAllCustomers()
        {
            List<Customer> list = _context.Users.Include(x => x.Location).ToList();
            return (list, true, "Customers returned successfully");
        }

        public (List<Customer> customers, bool successful, string message) GetAllCustomersBornInDateRange(DateTime StartDate, DateTime EndDate)
        {
            var customers = _context.Users.Where(x => x.DateOfBirth >= StartDate && x.DateOfBirth <= EndDate.AddHours(24)).ToList();

            return (customers, true, "Categories returned successfully");
        }

        public (CustomerVIewModel customer, bool successful, string message) GetCustomerById(string id)
        {
            Customer cusExists = _context.Users.Where(x => x.Id == id).Include(x => x.Location).FirstOrDefault();
            if (cusExists == null)
            {
                return (null, false, $"Customer with id : {id} does not exist");
            }
            CustomerVIewModel cust = new CustomerVIewModel()
            {
                 Id= cusExists.Id,
                 FirstName= cusExists.FirstName,
                 LastName= cusExists.LastName,
                 MiddleName= cusExists.MiddleName,
                 Email= cusExists.Email,
                 Phone= cusExists.PhoneNumber,
                 Address= cusExists.Address,
                 Location= new LocationViewModel()
                 {
                     Id = cusExists.Location.Id,
                     Name = cusExists.Location.Name,
                     DateCreated= cusExists.Location.DateCreated
                 },
                 LocationId= cusExists.Location.Id,
                 DateOfBirth= cusExists.DateOfBirth
            };
            return (cust, true, "Customer Found");
        }

        public async Task<(bool succesful, string message)> Login(string username, string password)
        {
            Customer cusExists = _context.Users.Where(x => x.Email== username).FirstOrDefault();
            if (cusExists == null)
            {
                return (false, "Invalid UserName or Password");
            }
            else
            {
                var result = await _userManager.CheckPasswordAsync(cusExists, password);
                if (result)
                {
                    return (true, "Login Succesful");
                }
                else
                {
                    return (false, "Invalid UserName or Password");
                }
            }
        }

        public (Customer customer, bool successful, string message) UpdateCustomer(CreateCustomerViewModel model, string id)
        {
            Customer cusExists = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (cusExists == null)
            {
                return (null, false, $"Customer with id : {id} does not exist");
            }
            cusExists.FirstName = model.FirstName;
            cusExists.LastName = model.LastName;
            cusExists.MiddleName = model.MiddleName;
            cusExists.PhoneNumber = model.PhoneNumber;
            cusExists.Address = model.Address;
            cusExists.LocationId = model.LocationId;
            cusExists.DateOfBirth = model.DateOfBirth;
            _context.Users.Update(cusExists);
            _context.SaveChanges();

            return (cusExists, true, "Customer Updated");
        }
    }
}
