using System.Data;
using API_Project.Context;
using API_Project.DTO.Customer;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;

namespace API_Project.Repository
{
    public class CustomerRepository :ICustomer
     {
          private readonly DapperContext _context;

          public CustomerRepository(DapperContext context)
          {
               this._context = context;
          }
          public async Task<IEnumerable<CustomerModel>> GetCustomers()
          {
               var query = "SELECT Id, FirstName, LastName FROM Customer";
               using var connection = _context.CreateConnection();
               var customers = await connection.QueryAsync<CustomerModel>(query);
               return customers;
          }
          public async Task<CustomerModel> AddCustomer(CustomerAddDTO customer)
          {
               var query = @"INSERT INTO Customer (FirstName, LastName) VALUES (@FirstName, @LastName )
                           SELECT Id, FirstName, LastName FROM Customer WHERE Id = (SELECT SCOPE_IDENTITY() AS LastInsertedID);";


               var parameters = new DynamicParameters();
               parameters.Add("FirstName", customer.FirstName);
               parameters.Add("LastName", customer.LastName);

               using (var connection = _context.CreateConnection())
               {
                    var newCustomer = await connection.QueryFirstOrDefaultAsync<CustomerModel>(query, parameters);
                    return newCustomer;
               }
          }

          public async Task<bool> EditCustomer(int id, CustomerEditDTO customer)
          {
               var query = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName WHERE id= @Id ";
               var parameters = new DynamicParameters();
               parameters.Add("Id",id,DbType.Int32);
               parameters.Add("FirstName",customer.FirstName,DbType.String);
               parameters.Add("LastName",customer.LastName, DbType.String);

               using (var connection = _context.CreateConnection())
               {
                    var rewAffected = await connection.ExecuteAsync(query,parameters);
                    return rewAffected > 0;
               }

          }

          public async Task<CustomerModel> GetCustomer(int id)
          {
               var query = "SELECT Id, FirstName, LastName FROM Customer WHERE id = @Id";
               var parameters = new DynamicParameters();
               parameters.Add("Id",id, DbType.Int32);

               using(var connection = _context.CreateConnection())
               {
                    var customer = await connection.QueryFirstOrDefaultAsync<CustomerModel>(query, parameters);
                    return customer;
               }
          }
     }
}
