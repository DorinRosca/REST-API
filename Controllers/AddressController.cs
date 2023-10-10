using API_Project.DTO.Address;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class AddressController : ControllerBase
     {
          private readonly IAddress _addressRepository;

          public AddressController(IAddress address)
          {
               this._addressRepository = address;
          }
          /// <summary>
          /// Edit an existing Address
          /// </summary>
          /// <param name="id">The unique identifier of the address to be edited.</param>
          /// <param name="address">The updated address information to be applied.</param>
          /// <returns>Returns an IActionResult indicating the result of the edit operation</returns>
          /// <response code="200">the product was successfully added in DBase </response>
          /// <response code="400">Unable to add product due to validation error or an product with such name already exist</response>
          [HttpPut("edit{id}")]
          [ProducesResponseType(typeof(bool), 200)]
          [ProducesResponseType(400)]
          public async Task<IActionResult> EditAddress(int id, [FromBody] AddressEditDTO address)
          {
               var editedAddress = await _addressRepository.EditAddress(id, address);


               if (editedAddress)
               {
                    return Ok(editedAddress);
               }
               return NotFound();
          }
          /// <summary>
          /// Get an address by its unique identifier.
          /// </summary>
          /// <param name="id">The unique identifier of the address to retrieve.</param>
          /// <returns>Returns an IActionResult containing the address information if found, or a NotFound response if not found.</returns>
          /// <response code="200">Returns an IActionResult with a 200 status code and the address information if found.</response>
          /// <response code="404">Returns an IActionResult with a 404 status code if the address with the specified ID is not found.</response>
          [HttpGet("{id}")]
          [ProducesResponseType(typeof(AddressModel), 200)]
          [ProducesResponseType(404)]
          public async Task<IActionResult> GetAddress(int id)
          {
               var address = await _addressRepository.GetAddress(id);

               if (IsAddressEmpty(address))
               {
                    // Return a not found response if the address was not found
                    return NotFound();
               }
               else
               {
                    // Return a successful response with the retrieved address
                    return Ok(address);
               }
          }

          private bool IsAddressEmpty(AddressModel address)
          {
               return address.Id == 0;
          }
     }
}
