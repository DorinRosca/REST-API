using System.Data;
using API_Project.Context;
using API_Project.DTO.FoodOrder;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace API_Project.Repository
{
     public class FoodOrderRepository :IFoodOrder
     {

          public readonly DapperContext _context;

          public FoodOrderRepository(DapperContext context)
          {
               this._context = context;
          }
          public async Task<FoodOrderModel> AddFoodOrder(FoodOrderAddDTO foodOrder)
          {
               foodOrder.OrderDateTime = DateTime.Now;
               int sum = 0;
               foreach (var item in foodOrder.OrderItems)
               {
                    foodOrder.TotalAmount += item.FoodItemPrice * item.Quantity;
               }

               foodOrder.TotalAmount += foodOrder.DeliveryFee;
               var query =
                         @"INSERT INTO FoodOrder(CustomerId, DeliveryAddressId, OrderStatusId, RestaurantId, DeliveryFee,  TotalAmount, OrderDateTime, RequestedDeliveryDateTime )
                         VALUES (@CustomerId, @DeliveryAddressId, @OrderStatusId, @RestaurantId, @DeliveryFee,  @TotalAmount, @OrderDateTime, @RequestedDeliveryDateTime )
                         SELECT Id, CustomerId, DeliveryAddressId, DriverId, OrderStatusId, RestaurantId, DeliveryFee,  TotalAmount, OrderDateTime, RequestedDeliveryDateTime FROM FoodOrder 
                         WHERE Id = (SELECT CAST(SCOPE_IDENTITY() AS int))";

               var parameters = new DynamicParameters();
               parameters.Add("CustomerId",foodOrder.CustomerId,DbType.Int32);
               parameters.Add("DeliveryAddressId",foodOrder.AddressId,DbType.Int32);
               parameters.Add("OrderStatusId", foodOrder.OrderStatusId,DbType.Int32);
               parameters.Add("RestaurantId", foodOrder.RestaurantId,DbType.Int32);
               parameters.Add("DeliveryFee", foodOrder.DeliveryFee, DbType.Decimal);
               parameters.Add("TotalAmount", foodOrder.TotalAmount, DbType.Decimal);
               parameters.Add("OrderDateTime", foodOrder.OrderDateTime, DbType.DateTime);
               parameters.Add("RequestedDeliveryDateTime", foodOrder.RequestedDeliveryDateTime, DbType.DateTime);

               using (var connection = _context.CreateConnection())
               {
                    var newFoodOrder = await connection.QueryFirstAsync<FoodOrderModel>(query,parameters);
                    return newFoodOrder;
               }
          }

          public async Task<bool> CancelOrder(int id)
          {
               const int cancelledId = 5;
               var query = "UPDATE FoodOrder SET OrderStatusId = @NewStatus WHERE Id = @Id ";
               var parameters = new DynamicParameters();
               parameters.Add("Id", id,DbType.Int32);
               parameters.Add("NewStatus", cancelledId,DbType.Int32);

               using (var connection = _context.CreateConnection())
               {
                    var rowAffected = await connection.ExecuteAsync(query, parameters);
                    return rowAffected > 0;
               }
          }
          [Authorize(Roles = "Admin,Driver")]
          public async Task<bool> SetDriver(int id, int driverId)
          {
               var query = "UPDATE FoodOrder SET DriverId = @DriverId WHERE Id = @Id ";
                           var parameters = new DynamicParameters();
               parameters.Add("Id", id, DbType.Int32);
               parameters.Add("DriverId", driverId, DbType.Int32);
               using (var connection = _context.CreateConnection())
               {
                    var rowAffected = await connection.ExecuteAsync(query, parameters);
                    return rowAffected > 0;
               }
          }
     }
}
