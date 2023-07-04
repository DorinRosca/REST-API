using API_Project.DTO.User;
using API_Project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
     private readonly IUser _userRepository;

     public UserController(IUser user)
     {
          this._userRepository = user;
     }

     [Authorize(Roles = "Admin")]
     [HttpPost("add")]
     public Task<bool> AddUser([FromBody] UserAddDTO user)
     {
          return  _userRepository.AddUser(user);
     }
}