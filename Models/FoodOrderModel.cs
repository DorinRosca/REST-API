using API_Project.Interfaces;
using System.Net;

namespace API_Project.Models
{
     public class FoodOrderModel
     {
          public int Id { get; set; }

          public int CustomerId { get; set; }
          public CustomerModel Customer { get; set; }
          public int DeliveryAddressId { get; set; }
          public AddressModel Address { get; set; }
          public int DriverId { get; set; }
          public UserModel Driver { get; set; }
          public int OrderStatusId { get; set; }
          public OrderStatusModel OrderStatus { get; set; }
          public int RestaurantId { get; set; }
          public RestaurantModel Restaurant { get; set; }
          public decimal DeliveryFee { get; set; }
          public decimal TotalAmount { get; set; }
          public DateTime OrderDateTime { get; set; }
          public DateTime RequestedDeliveryDateTime { get; set; }

          public ICollection<FoodItemModel> FoodOrders { get; set; }


     }
}
