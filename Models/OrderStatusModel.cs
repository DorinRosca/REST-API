namespace API_Project.Models
{
     public class OrderStatusModel
     {
          public int Id { get; set; }
          public string OrderStatusName { get; set; }

          public ICollection<FoodOrderModel> FoodOrders { get; set; }
     }
}
