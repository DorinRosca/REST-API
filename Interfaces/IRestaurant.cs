using API_Project.DTO.Restaurant;
using API_Project.Models;

namespace API_Project.Interfaces
{
    public interface IRestaurant
     {
         public Task<IEnumerable<RestaurantModel>> GetRestaurants();
         public Task<RestaurantModel> GetRestaurant(int id);
         public Task <RestaurantModel>CreateRestaurant(RestaurantAddDTO restaurant);

         public Task <bool> EditRestaurant(int id, RestaurantEditDTO restaurant);

         public Task<RestaurantModel> GetRestaurantMenu(int id);
     }
}
