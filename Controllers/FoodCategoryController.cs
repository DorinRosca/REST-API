using API_Project.DTO.FoodCategory;
using API_Project.Interfaces;
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

          [HttpGet("{id}")]
          public async Task<IActionResult> GetCategories(int id)
          {
               var categories = await _foodCategory.GetFoodCategories(id);

               if (categories.IsNullOrEmpty())
               {
                    return Problem();
               }
               return Ok(categories);

          }
          [Authorize]
          [HttpPost("add/{id}")]
          public async Task<IActionResult> AddCategory(int id, [FromBody] FoodCategoryAddDTO foodCategory)
          {
               var newCategory = await _foodCategory.AddFoodCategory(id,foodCategory);
               return Ok(newCategory);
          }

          [HttpPut("edit/{id}")]
          public async Task<IActionResult> EditCategory(int id, [FromBody] FoodCategoryEditDTO foodCategory)
          {
               var result =  await _foodCategory.EditFoodCategory(id, foodCategory);
               return Ok(result);
          }
     }
}
