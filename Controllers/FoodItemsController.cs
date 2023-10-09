using API_Project.DTO.FoodItem;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Project.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class FoodItemsController : ControllerBase
     {
          private readonly IFoodItem _foodItem;

          public FoodItemsController(IFoodItem foodItem)
          {
               _foodItem = foodItem;
          }

          /// <summary>
          /// Get all food items by category ID.
          /// </summary>
          /// <param name="id">The ID of the food category to retrieve items for.</param>
          /// <remarks>
          /// This endpoint allows you to retrieve all food items belonging to a specific food category by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the list of food items if found, or a 404 Not Found response if no items are available.</returns>
          /// <response code="200">Returns the list of food items if found.</response>
          /// <response code="404">If no items are available for the specified food category ID.</response>
          [HttpGet("{id}")]

          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FoodItemModel>))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> GetAllItems(int id)
          {
               var items = await _foodItem.GetFoodItems(id);
               if (items.IsNullOrEmpty())
               {
                    return NotFound();
               }
               return Ok(items);
          }

          /// <summary>
          /// Search for food items based on a query.
          /// </summary>
          /// <param name="query">The search query to find matching food items.</param>
          /// <remarks>
          /// This endpoint allows you to search for food items based on a query string.
          /// </remarks>
          /// <param name="query">The search query.</param>
          /// <returns>Returns a 200 OK response with a list of matching food items if found, or a 404 Not Found response if no items match the query.</returns>
          /// <response code="200">Returns a list of matching food items if found.</response>
          /// <response code="404">If no items match the provided query.</response>
          [HttpGet("search/{query}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FoodItemModel>))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<ActionResult<IEnumerable<FoodItemModel>>> SearchItems(string query)
          {
               var items = await _foodItem.SearchFoodItems(query);
               if (items.IsNullOrEmpty())
               {
                    return NotFound();
               }
               return Ok(items);
          }

          /// <summary>
          /// Edit an existing food item by ID.
          /// </summary>
          /// <param name="id">The ID of the food item to edit.</param>
          /// <param name="foodItem">The updated food item data.</param>
          /// <remarks>
          /// This endpoint allows you to edit an existing food item's information by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the updated food item data if successful, or a 404 Not Found response if the food item with the specified ID is not found.</returns>
          /// <response code="200">Returns the updated food item data if the edit is successful.</response>
          /// <response code="404">If the food item with the specified ID is not found.</response>
          [HttpPut("edit/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult>EditFoodItem(int id, [FromBody] FoodItemEditDTO foodItem)
          {
               var item = await _foodItem.EditFoodItems(id,foodItem);
               return Ok(item);
          }
     }
}
