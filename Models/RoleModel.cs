namespace API_Project.Models
{
     public class RoleModel
     {
          public int Id { get; set; }
          public string RoleName { get; set; }

          public ICollection<UserModel> Users { get; set; }
     }
}
