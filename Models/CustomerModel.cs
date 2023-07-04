namespace API_Project.Models
{
     public class CustomerModel
     {
          public int Id { get; set; }

          public string FirstName { get; set; }
          public string LastName { get; set; }

          public ICollection<FoodOrderModel> FoodOrders { get; set; }
          public ICollection<AddressModel> Addresses { get; set; }
     }
}
