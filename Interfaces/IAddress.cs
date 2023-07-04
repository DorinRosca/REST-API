using API_Project.DTO.Address;
using API_Project.Models;

namespace API_Project.Interfaces
{
     public interface IAddress
     {
          public Task<bool> EditAddress(int id, AddressEditDTO address);
          public Task<AddressModel> GetAddress(int id);

     }
}
