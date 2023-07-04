using API_Project.DTO.Customer;
using API_Project.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
     [ApiController]
     public class CustomerController : ControllerBase
     {
          private readonly ICustomer _customerRepository;

          public CustomerController(ICustomer customer)
          {
               this._customerRepository = customer;
          }

          [HttpGet]
          public async Task<IActionResult> GetCustomers()
          {
               var customers = await _customerRepository.GetCustomers();
               if (customers.IsNullOrEmpty())
               {
                    return NotFound();
               }
               else
               {
                    return Ok(customers);
               }
          }

          
          [HttpPost("add")]
          public async Task<IActionResult> AddCustomer([FromBody] CustomerAddDTO customer)
          {
               var createdCustomer = await _customerRepository.AddCustomer(customer);
               return CreatedAtRoute("RestaurantById", new { id = createdCustomer.Id }, createdCustomer);
          }

          [HttpPut("edit/{id}")]
          public async Task<IActionResult> EditCustomer(int id, [FromBody] CustomerEditDTO customer)
          {
               var editedCustomer = await _customerRepository.EditCustomer(id, customer);

               if (editedCustomer)
               {
                    return Ok(editedCustomer);

               }
               return NotFound();
          }


          [HttpGet("{id}")]
          public async Task<IActionResult> GetCustomer(int id)
          {
               var customer = await _customerRepository.GetCustomer(id);
               return Ok(customer);
          }
     }
}
