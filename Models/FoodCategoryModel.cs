namespace API_Project.Models
{
     public class FoodCategoryModel
     {
          public int Id { get; set; }
          public string FoodCategoryName { get; set; }
          public int RestaurantId { get; set; }

          public  RestaurantModel Restaurant { get; set; }
          public ICollection<FoodItemModel> FoodItems { get; set; }

          public FoodCategoryModel()
          {
               FoodItems = new List<FoodItemModel>();
          }
     }
}
