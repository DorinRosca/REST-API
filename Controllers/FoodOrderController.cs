using API_Project.DTO.FoodOrder;
using API_Project.Interfaces;
using API_Project.Models;
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

          /// <summary>
          /// Add a new food order.
          /// </summary>
          /// <param name="order">The data for the new food order.</param>
          /// <remarks>
          /// This endpoint allows you to add a new food order.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the newly created food order if successful.</returns>
          /// <response code="200">Returns the newly created food order if the operation is successful.</response>
          [HttpPost("add")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodOrderModel))]
          public async Task<IActionResult> AddOrder([FromBody] FoodOrderAddDTO order)
          {
               var newOrder = await _foodOrderRepository.AddFoodOrder(order);
               return Ok(newOrder);
          }
          /// <summary>
          /// Cancel a food order by ID.
          /// </summary>
          /// <param name="id">The ID of the food order to cancel.</param>
          /// <remarks>
          /// This endpoint allows you to cancel a food order by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response if the cancellation is successful, or a 404 Not Found response if the order with the specified ID is not found.</returns>
          /// <response code="200">Returns a 200 OK response if the cancellation is successful.</response>
          /// <response code="404">If the order with the specified ID is not found.</response>
          [HttpPut("cancel/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> CancelOrder(int id)
          {
               var order = await _foodOrderRepository.CancelOrder(id);
               if (order)
               {
                    return Ok(order);
               }
               return NotFound();
          }

          /// <summary>
          /// Set a driver for a food order by order ID.
          /// </summary>
          /// <param name="id">The ID of the food order to set the driver for.</param>
          /// <param name="driverId">The ID of the driver to assign to the order.</param>
          /// <remarks>
          /// This endpoint allows authorized users (Admin or Driver roles) to set a driver for a food order by specifying the order ID and driver ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response if the operation is successful, or a 404 Not Found response if the order with the specified ID is not found.</returns>
          /// <response code="200">Returns a 200 OK response if the operation is successful.</response>
          /// <response code="404">If the order with the specified ID is not found.</response>
          [HttpPut("setdriver/{id}")]
          [Authorize(Roles = "Admin,Driver")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
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
