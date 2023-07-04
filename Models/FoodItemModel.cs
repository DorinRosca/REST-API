namespace API_Project.Models
{
     public class FoodItemModel
     {
           public int Id { get; set; }
          public string FoodItemName { get; set; }
          public decimal FoodItemPrice { get; set; }

          public int FoodCategoryId { get; set; }
          public FoodCategoryModel FoodCategory { get; set; }

          public ICollection<FoodOrderModel> FoodOrders { get; set; }
     }
}
