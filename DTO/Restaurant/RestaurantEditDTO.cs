using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.Restaurant
{
    public class RestaurantEditDTO
    {
          [Required]
          [StringLength(50)]
        public string RestaurantName { get; set; }
    }
}
