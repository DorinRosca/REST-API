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

     /// <summary>
     /// Add a new user with the specified role.
     /// </summary>
     /// <param name="user">The data for the new user.</param>
     /// <remarks>
     /// This endpoint allows authorized users with the "Admin" role to add a new user with the specified role.
     /// </remarks>
     /// <param name="user">The user data.</param>
     /// <returns>Returns a 200 OK response if the user is added successfully, or a 400 Bad Request response if the operation fails.</returns>
     /// <response code="200">Returns a 200 OK response if the user is added successfully.</response>
     /// <response code="400">If the operation fails.</response>
     [Authorize(Roles = "Admin")]
     [HttpPost("add")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public Task<bool> AddUser([FromBody] UserAddDTO user)
     {
          return  _userRepository.AddUser(user);
     }
}