using API_Project.DTO.FoodOrder;
using API_Project.Models;

namespace API_Project.Interfaces
{
     public interface IFoodOrder
     {
          public Task<FoodOrderModel> AddFoodOrder(FoodOrderAddDTO foodOrder);
          public Task<bool> CancelOrder(int id);
          public Task<bool> SetDriver(int id, int driverId);

     }
}
