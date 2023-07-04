using API_Project.DTO.User;

namespace API_Project.Interfaces
{
     public interface IUser
     {
          public Task<bool> AddUser(UserAddDTO user);

     }
}
