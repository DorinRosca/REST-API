namespace API_Project.Models
{
     public class RestaurantModel
     {
          public int Id { get; set; }
          public string RestaurantName { get; set; }
          public int AddressId { get; set; }
          public AddressModel Address { get; set; }
          public ICollection<FoodOrderModel> FoodOrders { get; set; }
          public ICollection<FoodCategoryModel> FoodCategories { get; set; }

          public RestaurantModel()
          {
               FoodCategories = new List<FoodCategoryModel>();
          }

     }
}
