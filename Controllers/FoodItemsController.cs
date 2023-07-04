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

          [HttpGet("{id}")]
          public async Task<IActionResult> GetAllItems(int id)
          {
               var items = await _foodItem.GetFoodItems(id);
               if (items.IsNullOrEmpty())
               {
                    return NotFound();
               }
               return Ok(items);
          }

          [HttpGet("search/{query}")]
          public async Task<ActionResult<IEnumerable<FoodItemModel>>> SearchItems(string query)
          {
               var items = await _foodItem.SearchFoodItems(query);
               if (items.IsNullOrEmpty())
               {
                    return NotFound();
               }
               return Ok(items);
          }

          [HttpPut("edit/{id}")]
          public async Task<IActionResult>EditFoodItem(int id, [FromBody] FoodItemEditDTO foodItem)
          {
               var item = await _foodItem.EditFoodItems(id,foodItem);
               return Ok(item);
          }
     }
}
