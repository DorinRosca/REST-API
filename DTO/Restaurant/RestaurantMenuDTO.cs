using System.ComponentModel.DataAnnotations;
using API_Project.Models;

namespace API_Project.DTO.Restaurant
{
    public class RestaurantMenuDTO
    {
         [Required]
         [StringLength(50)]
        public string RestaurantName { get; set; }
         
        [Required]
        [Range(0,Int32.MaxValue)]
        public int AddressId { get; set; }
        [Required]
        public AddressModel Address { get; set; }
        [Required]
        public ICollection<FoodOrderModel> FoodOrders { get; set; }
        [Required]
        public ICollection<FoodCategoryModel> FoodCategories { get; set; }

    }
}
