using API_Project.DTO.Address;
using API_Project.Interfaces;
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

          [HttpPut("edit{id}")]
          public async Task<IActionResult> EditAddress(int id, [FromBody] AddressEditDTO address)
          {
               var editedAddress = await _addressRepository.EditAddress(id, address);


               if (editedAddress)
               {
                    return Ok(editedAddress);
               }
               return NotFound();
          }
          [HttpGet("{id}")]
          public async Task<IActionResult> GetAddress(int id)
          {
               var address = await _addressRepository.GetAddress(id);

               return Ok(address);
          }
     }
}
