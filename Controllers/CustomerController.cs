using API_Project.DTO.Customer;
using API_Project.Interfaces;
using API_Project.Models;
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
          
          /// <summary>
          /// Retrieves a list of customers.
          /// </summary>
          /// <remarks>
          /// This endpoint returns a list of customers from the repository.
          /// </remarks>
          /// <returns>Returns a list of customers if any are found, or a 404 Not Found response if no customers are available.</returns>
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerModel>))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          [HttpPost("add")]
          public async Task<IActionResult> AddCustomer([FromBody] CustomerAddDTO customer)
          {
               var createdCustomer = await _customerRepository.AddCustomer(customer);
               return CreatedAtRoute("RestaurantById", new { id = createdCustomer.Id }, createdCustomer);
          }
          
          /// <summary>
          /// Edit an existing customer by ID.
          /// </summary>
          /// <param name="id">The ID of the customer to edit.</param>
          /// <param name="customer">The updated customer data.</param>
          /// <remarks>
          /// This endpoint allows you to edit an existing customer's information.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the edited customer data if successful, or a 404 Not Found response if the customer with the specified ID is not found.</returns>
          /// <response code="200">Returns the edited customer data if the update is successful.</response>
          /// <response code="404">If the customer with the specified ID is not found.</response>
          [HttpPut("edit/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> EditCustomer(int id, [FromBody] CustomerEditDTO customer)
          {
               var editedCustomer = await _customerRepository.EditCustomer(id, customer);

               if (editedCustomer)
               {
                    return Ok(editedCustomer);

               }
               return NotFound();
          }

          /// <summary>
          /// Get a customer by ID.
          /// </summary>
          /// <param name="id">The ID of the customer to retrieve.</param>
          /// <remarks>
          /// This endpoint allows you to retrieve a customer's information by specifying their ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the customer data if found, or a 404 Not Found response if the customer with the specified ID is not found.</returns>
          /// <response code="200">Returns the customer data if found.</response>
          /// <response code="404">If the customer with the specified ID is not found.</response>
          [HttpGet("{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> GetCustomer(int id)
          {
               var customer = await _customerRepository.GetCustomer(id);
               return Ok(customer);
          }
     }
}
