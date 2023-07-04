using System.Data;
using API_Project.Context;
using API_Project.DTO.FoodItem;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;

namespace API_Project.Repository
{
     public class FoodItemRepository :IFoodItem
     {
          private readonly DapperContext _context;

          public FoodItemRepository(DapperContext context)
          {
               this._context = context;
          }
          public async Task<IEnumerable<FoodItemModel>> GetFoodItems(int categoryId)
          {
               var query = "SELECT Id, FoodItemName, FoodItemPrice, FoodCategoryId FROM FoodItem WHERE FoodCategoryId = @Id";

               var parameters = new DynamicParameters();
               parameters.Add("Id",categoryId, DbType.Int32);

               using (var connection = _context.CreateConnection())
               {
                    var foodItems = await connection.QueryAsync<FoodItemModel>(query, parameters);

                    return foodItems;
               }
          }

          public async Task<IEnumerable<FoodItemModel>> SearchFoodItems(string name)
          {
               var query = "SELECT Id, FoodItemName, FoodItemPrice, FoodCategoryId FROM FoodItem WHERE FoodItemName LIKE @Name";
               var parameters = new DynamicParameters();
               parameters.Add("Name", $"%{name}%",DbType.String);
               
               using (var connection = _context.CreateConnection())
               {
                    var resultItems = await connection.QueryAsync<FoodItemModel>(query, parameters);
                    return resultItems;
               }
          }

          public async Task<bool> EditFoodItems(int id , FoodItemEditDTO foodItem)
          {
               var query =
                    "UPDATE FoodItem SET FoodItemName = @Name, FoodItemPrice = @Price, FoodCategoryId = @CategoryId WHERE Id = @Id ";
               var parameters = new DynamicParameters();
               parameters.Add("Id",id,DbType.Int32);
               parameters.Add("Name",foodItem.FoodItemName,DbType.String);
               parameters.Add("Price",foodItem.FoodItemPrice,DbType.Int32);
               parameters.Add("CategoryId",foodItem.FoodItemCategoryId,DbType.Int32);

               using (var connection = _context.CreateConnection())
               {
                    var rowAffected = await connection.ExecuteAsync(query, parameters);
                    return rowAffected > 0;
               }
          }
     }
}
