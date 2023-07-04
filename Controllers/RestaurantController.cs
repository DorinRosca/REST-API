using API_Project.DTO.Restaurant;
using API_Project.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Project.Controllers
{
    [Route("api/restaurants")]
     [ApiController]
     public class RestaurantController : ControllerBase
     {
          private readonly IRestaurant _restaurantRepository;

          public RestaurantController(IRestaurant restaurant)
          {
               this._restaurantRepository = restaurant;
          }

          [HttpGet]
          public async Task<IActionResult> GetRestaurants()
          {

               var restaurants = await _restaurantRepository.GetRestaurants();
               if (restaurants.IsNullOrEmpty())
               {
                    return NotFound();
               }
               return Ok(restaurants);
          }

          [HttpPost("add")]
          public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantAddDTO restaurant)
          {
               var createdRestaurant = await _restaurantRepository.CreateRestaurant(restaurant);

               return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.Id }, createdRestaurant);
          }

          [HttpGet("{id}", Name = "RestaurantById")]
          public async Task<IActionResult> GetRestaurant(int id)
          {
               
               var restaurant = (await _restaurantRepository.GetRestaurant(id));

               return Ok(restaurant);
          }

          [HttpPut("edit/{id}")]
          public async Task<bool> EditRestaurant(int id, [FromBody]RestaurantEditDTO restaurant)
          {
               return await _restaurantRepository.EditRestaurant(id, restaurant);
          }

          [HttpGet("menu/{id}")]
          public async Task<IActionResult> GetRestaurantMenu(int id)
          {
               var restaurants = await _restaurantRepository.GetRestaurantMenu(id);
               if (restaurants is null)
               {
                    return NotFound();
               }

               return Ok(restaurants);
          }
     }
}
