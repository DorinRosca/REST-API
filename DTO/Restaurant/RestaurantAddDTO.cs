using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.Restaurant
{
    public class RestaurantAddDTO
    {
         [Required]
         [StringLength(50)]
         public string RestaurantName { get; set; }


         [Required]
         [Range(0,Int32.MaxValue)]
        public int AddressId { get; set; }
    }
}
