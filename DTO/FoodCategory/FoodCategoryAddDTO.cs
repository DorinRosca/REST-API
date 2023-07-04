using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.FoodCategory
{
     public class FoodCategoryAddDTO
     {
          [Required]
          [StringLength(50)]
          public string FoodCategoryName { get; set; }
     }
}
