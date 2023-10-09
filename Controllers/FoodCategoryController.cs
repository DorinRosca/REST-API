using API_Project.DTO.FoodCategory;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Project.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class FoodCategoryController : ControllerBase
     {
          private readonly IFoodCategory _foodCategory;

          public FoodCategoryController(IFoodCategory foodCategory)
          {
               this._foodCategory = foodCategory;
          }
          /// <summary>
          /// Get food categories by ID.
          /// </summary>
          /// <param name="id">The ID of the food item to retrieve categories for.</param>
          /// <remarks>
          /// This endpoint allows you to retrieve food categories for a specific food item by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the list of food categories if found, or a 404 Not Found response if no categories are available.</returns>
          /// <response code="200">Returns the list of food categories if found.</response>
          /// <response code="404">If no categories are available for the specified food item ID.</response>
          [HttpGet("{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FoodCategoryModel>))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> GetCategories(int id)
          {
               var categories = await _foodCategory.GetFoodCategories(id);

               if (categories.IsNullOrEmpty())
               {
                    return Problem();
               }
               return Ok(categories);

          }
          /// <summary>
          /// Add a new food category for a food item by ID.
          /// </summary>
          /// <param name="id">The ID of the food item to which the category will be added.</param>
          /// <param name="foodCategory">The data for the new food category.</param>
          /// <remarks>
          /// This endpoint allows you to add a new food category to a specific food item by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the newly created food category if successful.</returns>
          /// <response code="200">Returns the newly created food category if the operation is successful.</response>
          [Authorize]
          [HttpPost("add/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodCategoryModel))]

          public async Task<IActionResult> AddCategory(int id, [FromBody] FoodCategoryAddDTO foodCategory)
          {
               var newCategory = await _foodCategory.AddFoodCategory(id,foodCategory);
               return Ok(newCategory);
          }

          /// <summary>
          /// Edit an existing food category by ID.
          /// </summary>
          /// <param name="id">The ID of the food category to edit.</param>
          /// <param name="foodCategory">The updated food category data.</param>
          /// <remarks>
          /// This endpoint allows you to edit an existing food category's information by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the updated food category data if successful, or a 404 Not Found response if the food category with the specified ID is not found.</returns>
          /// <response code="200">Returns the updated food category data if the edit is successful.</response>
          /// <response code="404">If the food category with the specified ID is not found.</response>
          [HttpPut("edit/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodCategoryModel))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> EditCategory(int id, [FromBody] FoodCategoryEditDTO foodCategory)
          {
               var result =  await _foodCategory.EditFoodCategory(id, foodCategory);
               return Ok(result);
          }
     }
}
