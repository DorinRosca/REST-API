using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.User
{
     public class UserAddDTO
     {
          [StringLength(50)]
          public string FirstName { get; set; }

          [StringLength(50)]
          public string LastName { get; set; }

          [StringLength(50)]
          public string Email { get; set; }

          [StringLength(50)]
          [MinLength(8)]
          public string Password { get; set; }
     }
}
