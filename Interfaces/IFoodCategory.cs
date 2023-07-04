using API_Project.DTO.FoodCategory;
using API_Project.Models;

namespace API_Project.Interfaces
{
     public interface IFoodCategory
     {

          public Task<IEnumerable<FoodCategoryModel>> GetFoodCategories(int id);

          public Task<FoodCategoryModel> AddFoodCategory(int restaurantId, FoodCategoryAddDTO foodCategory);

          public Task<bool> EditFoodCategory(int id, FoodCategoryEditDTO foodCategory);
     }
}
