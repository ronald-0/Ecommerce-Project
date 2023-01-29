using WebApplication1.Entities;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Interface
{
    public interface IAuthentication
    {
        (CustomerVIewModel customer, bool successful, string message) GetCustomerById(string id);
        Task<(Customer customer, bool successful, string message)> AddCustomer(CreateCustomerViewModel model);
        (Customer customer, bool successful, string message) UpdateCustomer(CreateCustomerViewModel model, string id);
        (bool successful, string message) DeleteCustomerById(string id);
        (List<Customer> customers, bool successful, string message) GetAllCustomers();
        (List<Customer> customers, bool successful, string message) GetAllCustomersBornInDateRange(DateTime StartDate, DateTime EndDate);
        Task<(bool succesful, string message)> Login(string username, string password);
    }
}
