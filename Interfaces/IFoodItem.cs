using API_Project.DTO.FoodItem;
using API_Project.Models;

namespace API_Project.Interfaces
{
     public interface IFoodItem
     {
          public Task<IEnumerable<FoodItemModel>> GetFoodItems(int id);
          public Task<IEnumerable<FoodItemModel>> SearchFoodItems(string name);

          public Task<bool> EditFoodItems(int id, FoodItemEditDTO foodItem);
     }
}
