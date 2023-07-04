using API_Project.DTO.FoodOrder;
using API_Project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class FoodOrderController : ControllerBase
     {
          private readonly IFoodOrder _foodOrderRepository;

          public FoodOrderController(IFoodOrder foodOrder)
          {
               _foodOrderRepository = foodOrder;
          }

          [HttpPost("add")]
          public async Task<IActionResult> AddOrder([FromBody] FoodOrderAddDTO order)
          {
               var newOrder = await _foodOrderRepository.AddFoodOrder(order);
               return Ok(newOrder);
          }

          [HttpPut("cancel/{id}")]
          public async Task<IActionResult> CancelOrder(int id)
          {
               var order = await _foodOrderRepository.CancelOrder(id);
               if (order)
               {
                    return Ok(order);
               }
               return NotFound();
          }

          [HttpPut("setdriver{id}")]
          [Authorize(Roles = "Admin,Driver")]
          public async Task<IActionResult> SetDriver(int id,int driverId)
          {
               var order = await _foodOrderRepository.SetDriver(id, driverId);
               if (order)
               {
                    return Ok(order);
               }
               return NotFound();
          }
     }
}
