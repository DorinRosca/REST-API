using System.Data;
using System.Net;
using API_Project.Context;
using API_Project.Interfaces;
using API_Project.Models;
using Dapper;

namespace API_Project.Repository
{
     public class LoginRepository:ILogin
     {
          private readonly DapperContext _context;

          public LoginRepository(DapperContext context)
          {
               this._context = context;
          }

          public async Task<UserModel> Login(UserLogin user)
          {
               var query = @"SELECT u.Id, u.FirstName, u.LastName, u.Email, u.[Password], R.Id, r.RoleName FROM [User] AS u
                           JOIN UserRole AS ur ON ur.UserId = u.Id
                           JOIN [Role] AS r ON r.Id = ur.RoleId
                           WHERE Email = @Email AND [Password] = @Password COLLATE Latin1_General_CS_AS ";
               var parameters = new DynamicParameters();
               parameters.Add("Email", user.Username, DbType.String);
               parameters.Add("Password", user.Password, DbType.String);
               var userDict = new Dictionary<int, UserModel>();
               using (var connection = _context.CreateConnection())
               {
                    var result = await connection.QueryAsync<UserModel, RoleModel, UserModel>(query, (userModel, roleModel) =>
                    {
                         if (!userDict.TryGetValue(userModel.Id, out var currentUser))
                         {
                              currentUser = userModel;
                              currentUser.Roles.Add(roleModel);
                              userDict.Add(currentUser.Id, currentUser);
                         }
                         currentUser.Roles.Add(roleModel);
                         return currentUser;
                    }, parameters);
                    return result.FirstOrDefault();
               }


          }
     }
}
