﻿using System.ComponentModel.DataAnnotations;

namespace API_Project.DTO.Customer
{
    public class CustomerEditDTO
    {

         [Required]
         [StringLength(50)]
         public string FirstName { get; set; }

         [Required]
         [StringLength(50)] 
         public string LastName { get; set; }
    }
}
