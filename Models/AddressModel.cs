using System.ComponentModel.DataAnnotations;
using API_Project.Interfaces;

namespace API_Project.Models
{
     public class AddressModel
     {
          public int Id { get; set; }
          
          public string HouseNumber { get; set; }

          public string StreetName { get; set; }
          
          public string City { get; set; }
          public string PostalCode { get; set; }

          public ICollection<RestaurantModel> Restaurants { get; set; }
          public ICollection<FoodOrderModel> FoodOrders { get; set; }
          public ICollection<CustomerAddressModel> CustomerAddress { get; set; }
          public ICollection<CustomerModel> Customers { get; set; }
     }
}
