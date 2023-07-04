using API_Project.DTO.Customer;
using API_Project.Models;

namespace API_Project.Interfaces
{
    public interface ICustomer
     {
          public Task<IEnumerable<CustomerModel>> GetCustomers();
          public Task<CustomerModel> AddCustomer(CustomerAddDTO customer);

          public Task<bool> EditCustomer(int id, CustomerEditDTO customer);

          public Task<CustomerModel> GetCustomer(int id);
     }
}
