namespace API_Project.Models
{
     public class FoodOrderItemModel
     {
          public int FoodOrderId { get; set; }
          public int FoodItemId { get; set; }

          public decimal FoodItemPrice { get; set; }

          public int Quantity { get; set; }
     }
}
