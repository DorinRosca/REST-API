using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.Address
{

     public class AddressEditDTO
     {
          [Required]
          [StringLength(20)]
          public string HouseNumber { get; set; }

          [Required]
          [StringLength(50)]
          public string StreetName { get; set; }
          [Required]
          [StringLength(50)]
          public string City { get; set; }

          [Required]
          [StringLength(5)]
          public string PostalCode { get; set; }
     }
}
