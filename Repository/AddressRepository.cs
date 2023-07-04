using System.Data;
using API_Project.Context;
using API_Project.DTO.Address;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;

namespace API_Project.Repository
{
     public class AddressRepository : IAddress
     {
          private readonly DapperContext _context;

          public AddressRepository(DapperContext context)
          {
               this._context = context;
          }

          public async Task<bool> EditAddress(int id, AddressEditDTO address)
          {
               var query = @"UPDATE Address 
                             SET HouseNumber = @Number, StreetName = @Street, City = @City, PostalCode = @Code 
                              WHERE Id = @Id ";
               var parameters = new DynamicParameters();
               parameters.Add("Id", id, DbType.Int32);
               parameters.Add("Number", address.HouseNumber, DbType.String);
               parameters.Add("Street", address.StreetName, DbType.String);
               parameters.Add("City", address.City, DbType.String);
               parameters.Add("Code", address.PostalCode, DbType.String);

               using (var connection = _context.CreateConnection())
               {
                    var rowAffected = await connection.ExecuteAsync(query, parameters);
                    return rowAffected > 0;
               }
          }

          public async Task<AddressModel> GetAddress(int id)
          {
               var query = "SELECT Id, HouseNumber, StreetName, City, PostalCode FROM Address WHERE Id = @Id";
               var parameters = new DynamicParameters();
               parameters.Add("Id",id,DbType.Int32);

               using (var connection = _context.CreateConnection())
               {
                    var address = await connection.QuerySingleAsync<AddressModel>(query, parameters);
                    return address;
               }
          }
     }
}
