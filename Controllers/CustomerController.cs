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

          /// <summary>
          /// Get a list of customers.
          /// </summary>
          /// <remarks>
          /// This endpoint retrieves a list of customers.
          /// </remarks>
          /// <returns>Returns a 200 OK response with a list of customers if any are found, or a 404 Not Found response if no customers are available.</returns>
          /// <response code="200">Returns a list of customers if any are found.</response>
          /// <response code="404">If no customers are available.</response>
          [HttpGet]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerModel>))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
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
          /// Add a new customer.
          /// </summary>
          /// <param name="customer">The data for the new customer.</param>
          /// <remarks>
          /// This endpoint allows you to add a new customer.
          /// </remarks>
          /// <param name="customer">The customer data.</param>
          /// <returns>Returns a 201 Created response with a location header and the newly created customer if successful.</returns>
          /// <response code="201">Returns a 201 Created response with a location header and the newly created customer if successful.</response>
          /// <response code="400">If the request data is invalid or incomplete.</response>
          [HttpPost]
          [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerModel))]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
          [HttpPut("{id}")]
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
