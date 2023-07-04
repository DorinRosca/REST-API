using System.Data;
using API_Project.Context;
using API_Project.DTO.User;
using API_Project.Interfaces;
using Dapper;

namespace API_Project.Repository
{
     public class UserRepository :IUser
     {
          private readonly DapperContext _context;

          public UserRepository(DapperContext context)
          {
               this._context = context;
          }

          public async Task<bool> AddUser(UserAddDTO user)
          {
               var query = @"INSERT INTO [User](FirstName, LastName, Email, [Password])  
                                   VALUES (@FirstName, @LastName, @Email, @Password)";
               var parameters = new DynamicParameters();
               parameters.Add("FirstName", user.FirstName,DbType.String);
               parameters.Add("LastName", user.LastName, DbType.String);
               parameters.Add("Email", user.Email, DbType.String);
               parameters.Add("Password", user.Password, DbType.String);

               using (var connection = _context.CreateConnection())
               {
                    var result = await connection.ExecuteAsync(query, parameters);
                    return result > 0;
               }

          }
     }
}
