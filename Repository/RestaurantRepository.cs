using System.Data;
using API_Project.Context;
using API_Project.DTO.Restaurant;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace API_Project.Repository
{
    public class RestaurantRepository : IRestaurant
     {
          public readonly DapperContext _context;

          public RestaurantRepository(DapperContext context)
          {
               this._context = context;
          }
          public async Task<IEnumerable<RestaurantModel>> GetRestaurants()
          {
               var query = "SELECT Id,RestaurantName,AddressId FROM Restaurant r ";
               using (var connection = _context.CreateConnection())
               {
                    return await connection.QueryAsync<RestaurantModel>(query);
               }
          }

          public async Task<RestaurantModel> CreateRestaurant(RestaurantAddDTO restaurant)
          {
               var query = @"INSERT INTO Restaurant (RestaurantName, AddressId) 
                         VALUES (@RestaurantName, @AddressId) 
                         SELECT Id, RestaurantName, AddressId  FROM Restaurant WHERE Id = (SELECT SCOPE_IDENTITY() AS LastCreatedId)";

               var parameters = new DynamicParameters();
               parameters.Add("RestaurantName", restaurant.RestaurantName);
               parameters.Add("AddressId", restaurant.AddressId);

               using (var connection = _context.CreateConnection())
               {
                    var id = await connection.QuerySingleAsync<int>(query, parameters);
                    var createdRestaurant = new RestaurantModel
                    {
                         Id = id,
                         RestaurantName = restaurant.RestaurantName,
                         AddressId = restaurant.AddressId
                    };
                    return createdRestaurant;
               }
          }

          public async Task<RestaurantModel> GetRestaurant(int id)
          {
               var query = @"SELECT * FROM Restaurant r
                             LEFT JOIN FoodCategory c ON c.RestaurantId = r.id 
                             LEFT JOIN FoodItem i ON i.FoodCategoryId = c.Id 
                             LEFT JOIN Address idr on r.AddressId = idr.Id WHERE r.id = @Id";
               var restaurantDict = new Dictionary<int, RestaurantModel>();

               var parameters = new DynamicParameters();
               parameters.Add("Id", id, DbType.Int32);
               using (var connection = _context.CreateConnection())
               {
                    var restaurants = await connection
                         .QueryAsync<RestaurantModel, FoodCategoryModel, FoodItemModel, AddressModel, RestaurantModel>(
                              query, (restaurant, foodCategory, foodItem, address) =>
                              {
                                   if (!restaurantDict.TryGetValue(restaurant.Id, out var currentRestaurant))
                                   {
                                        currentRestaurant = restaurant;
                                        currentRestaurant.Address = address;
                                        restaurantDict.Add(currentRestaurant.Id, currentRestaurant);
                                   }

                                   var existingCategory =
                                        currentRestaurant.FoodCategories.FirstOrDefault(fc => fc.Id == foodCategory.Id);
                                   if (existingCategory == null)
                                   {
                                        existingCategory = foodCategory;
                                        currentRestaurant.FoodCategories.Add(existingCategory);
                                   }

                                   existingCategory.FoodItems.Add(foodItem);

                                   return currentRestaurant;
                              },parameters
                         );

                    return restaurants.FirstOrDefault();
               }
          }

          public async Task<bool> EditRestaurant(int id, RestaurantEditDTO restaurant)
          {
               var query = "Update Restaurant SET RestaurantName = @RestaurantName WHERE Id = @Id ";
               var parameters = new DynamicParameters();
               parameters.Add("Id", id, DbType.Int32);
               parameters.Add("RestaurantName", restaurant.RestaurantName, DbType.AnsiString);
               using (var connection = _context.CreateConnection())
               {
                    var rowAffected = await connection.ExecuteAsync(query, parameters);
                    return rowAffected > 0;
               }
          }

          public async Task<RestaurantModel> GetRestaurantMenu(int id)
          {
               var query = @"SELECT * FROM Restaurant r 
                           JOIN FoodCategory c ON c.RestaurantId = r.Id 
                           JOIN FoodItem i ON i.FoodCategoryId = c.Id WHERE c.Id = @Id";

               using (var connection = _context.CreateConnection())
               {
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id, DbType.Int32);
                    var restaurantDict = new Dictionary<int, RestaurantModel>();
                    var restaurants = await connection.QueryAsync<RestaurantModel, FoodCategoryModel, FoodItemModel, RestaurantModel>(
                         query, (restaurant, foodCategory, foodItem) =>
                         {
                              if (!restaurantDict.TryGetValue(restaurant.Id, out var currentRestaurant))
                              {
                                   currentRestaurant = restaurant;
                                   restaurantDict.Add(currentRestaurant.Id, currentRestaurant);
                              }

                              var existingCategory = currentRestaurant.FoodCategories.FirstOrDefault(fc => fc.Id == foodCategory.Id);
                              if (existingCategory == null)
                              {
                                   existingCategory = foodCategory;
                                   currentRestaurant.FoodCategories.Add(existingCategory);
                              }

                              existingCategory.FoodItems.Add(foodItem);

                              return currentRestaurant;
                         },
                         parameters
                    );

                    return restaurants.FirstOrDefault();
               }
          }
     }
}
