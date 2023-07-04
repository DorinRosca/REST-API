using API_Project.Models;

namespace API_Project.Interfaces
{
     public interface ILogin
     {
          public Task<UserModel> Login(UserLogin user);
     }
}
