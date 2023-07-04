using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.FoodItem
{
     public class FoodItemEditDTO
     {
          [Required]
          [StringLength(50)]
          public string FoodItemName { get; set; }

          [Required]
          [Range(0,Int32.MaxValue)]
          public int FoodItemPrice { get; set; }
          
          [Required]
          [Range(0,Int32.MaxValue)]
          public int FoodItemCategoryId { get; set; }
     }
}
