using System.Data;
using API_Project.Context;
using API_Project.DTO.FoodCategory;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;

namespace API_Project.Repository
{
     public class FoodCategoryRepository : IFoodCategory
     {
          private readonly DapperContext _context;

          public FoodCategoryRepository(DapperContext context)
          {
               this._context = context;
          }
          public async Task<IEnumerable<FoodCategoryModel>> GetFoodCategories(int restaurantId)
          {
               
               var query = "SELECT Id, FoodCategoryName, RestaurantId FROM FoodCategory WHERE RestaurantId = @Id";

               var parameters = new DynamicParameters();
               parameters.Add("Id", restaurantId, DbType.Int32);

               using (var connection = _context.CreateConnection())
               {
                    var categories = await connection.QueryAsync<FoodCategoryModel>(query, parameters);

                    return categories;
               }
          }

          public async  Task<FoodCategoryModel> AddFoodCategory(int restaurantId, FoodCategoryAddDTO foodCategory)
          {
               var query = @"INSERT INTO FoodCategory(FoodCategoryName, RestaurantId) 
                         VALUES( @FoodName, @Id )SELECT Id, FoodCategoryName, RestaurantId FROM FoodCategory 
                         WHERE Id = (SELECT SCOPE_IDENTITY() AS LastInsertedID);";

               var parameters = new DynamicParameters();
               parameters.Add("FoodName",foodCategory.FoodCategoryName, DbType.String);
               parameters.Add("Id",restaurantId, DbType.Int32);

               using (var connection = _context.CreateConnection())
               {
                    var newFoodCategory = await connection.QueryFirstOrDefaultAsync<FoodCategoryModel>(query, parameters);
                    return newFoodCategory;
               }

          }

          public async Task<bool> EditFoodCategory(int id, FoodCategoryEditDTO foodCategory)
          {
               var query = "UPDATE FoodCategory SET FoodCategoryName = @Name WHERE Id =@Id ";
               var parameters = new DynamicParameters();
               parameters.Add("Id",id,DbType.Int32);
               parameters.Add("Name",foodCategory.FoodCategoryName,DbType.String);
               using (var connection = _context.CreateConnection())
               {
                    var rowAffected = await connection.ExecuteAsync(query, parameters);
                    return rowAffected > 0;
               }
          }
     }
}
