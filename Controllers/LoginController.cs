using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace API_Project.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class LoginController : ControllerBase
     {
          private readonly IConfiguration _configuration;
          private readonly ILogin _login;

          public LoginController(ILogin login, IConfiguration configuration)
          {
               _login = login;
               this._configuration=configuration;
          }
          /// <summary>
          /// Authenticate a user and generate an authentication token.
          /// </summary>
          /// <param name="userLogin">The user's login credentials.</param>
          /// <remarks>
          /// This endpoint allows users to log in by providing their credentials and returns an authentication token if successful.
          /// </remarks>
          /// <returns>Returns a 200 OK response with an authentication token if authentication is successful, or a 404 Not Found response with the message "UserNotFound" if the user is not found or authentication fails.</returns>
          /// <response code="200">Returns an authentication token if authentication is successful.</response>
          /// <response code="404">If the user is not found or authentication fails, returns a 404 Not Found response with the message "UserNotFound".</response>
          [AllowAnonymous]
          [HttpPost]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
          [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]

          public IActionResult Login([FromBody] UserLogin userLogin)
          {
               var user = Authenticate(userLogin);
               if (user != null)
               {
                    var token = Generate(user);
                    return Ok(token);
               }

               return NotFound("UserNotFound");
          }

          private object? Generate(UserModel user)
          {
               var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

               var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
               
               var claims = new List<Claim>()
               {
                    new Claim(ClaimTypes.NameIdentifier, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email)
               };
               foreach (var role in user.Roles)
               {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
               }

               var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                    expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
               return new JwtSecurityTokenHandler().WriteToken(token);
          }

          private  UserModel Authenticate(UserLogin userLogin)
          {
               var currentUser = _login.Login(userLogin);
               return currentUser.Result;
          }
     }
}
