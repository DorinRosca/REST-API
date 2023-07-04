using System.ComponentModel.DataAnnotations;
using API_Project.Models;

namespace API_Project.DTO.FoodOrder
{
     public class FoodOrderAddDTO
     {
          [Required]
          [Range(0, Int32.MaxValue)]
          public int CustomerId { get; set; }

          [Required]
          [Range(0, Int32.MaxValue)]
          public int AddressId { get; set; }

          [Required]
          [Range(0, Int32.MaxValue)]
          public int OrderStatusId { get; set; }

          [Required]
          [Range(0, Int32.MaxValue)]
          public int RestaurantId { get; set; }

          [Required]
          [Range(0, Double.PositiveInfinity)]
          public decimal DeliveryFee { get; set; }


          [Required]
          [Range(0, Double.PositiveInfinity)]
          public decimal TotalAmount { get; set; }
          public DateTime OrderDateTime { get; set; }
          public DateTime RequestedDeliveryDateTime { get; set; }

          public  ICollection<FoodOrderItemModel> OrderItems { get; set; }


          public FoodOrderAddDTO()
          {
               OrderItems = new List<FoodOrderItemModel>();
          }
     }
}
