using API_Project.DTO.Restaurant;
using API_Project.Interfaces;
using API_Project.Models;
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

          /// <summary>
          /// Get a list of restaurants.
          /// </summary>
          /// <remarks>
          /// This endpoint retrieves a list of restaurants.
          /// </remarks>
          /// <returns>Returns a 200 OK response with a list of restaurants if any are found, or a 404 Not Found response if no restaurants are available.</returns>
          /// <response code="200">Returns a list of restaurants if any are found.</response>
          /// <response code="404">If no restaurants are available.</response>
          [HttpGet]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantModel>))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]

          public async Task<IActionResult> GetRestaurants()
          {

               var restaurants = await _restaurantRepository.GetRestaurants();
               if (restaurants.IsNullOrEmpty())
               {
                    return NotFound();
               }
               return Ok(restaurants);
          }

          /// <summary>
          /// Create a new restaurant.
          /// </summary>
          /// <param name="restaurant">The data for the new restaurant.</param>
          /// <remarks>
          /// This endpoint allows you to create a new restaurant.
          /// </remarks>
          /// <returns>Returns a 201 Created response with a location header and the newly created restaurant if successful.</returns>
          /// <response code="201">Returns a 201 Created response with a location header and the newly created restaurant if successful.</response>
          /// <response code="400">If the request data is invalid or incomplete.</response>
          [HttpPost("add")]
          [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RestaurantModel))]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantAddDTO restaurant)
          {
               var createdRestaurant = await _restaurantRepository.CreateRestaurant(restaurant);

               return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.Id }, createdRestaurant);
          }

          /// <summary>
          /// Get a restaurant by ID.
          /// </summary>
          /// <param name="id">The ID of the restaurant to retrieve.</param>
          /// <remarks>
          /// This endpoint allows you to retrieve a restaurant's information by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the restaurant data if found, or a 404 Not Found response if the restaurant with the specified ID is not found.</returns>
          /// <response code="200">Returns the restaurant data if found.</response>
          /// <response code="404">If the restaurant with the specified ID is not found.</response>
          [HttpGet("{id}", Name = "RestaurantById")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantModel))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<IActionResult> GetRestaurant(int id)
          {
               
               var restaurant = (await _restaurantRepository.GetRestaurant(id));

               return Ok(restaurant);
          }

          /// <summary>
          /// Edit an existing restaurant by ID.
          /// </summary>
          /// <param name="id">The ID of the restaurant to edit.</param>
          /// <param name="restaurant">The updated restaurant data.</param>
          /// <remarks>
          /// This endpoint allows you to edit an existing restaurant's information by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response if the edit is successful, or a 400 Bad Request response if the edit fails.</returns>
          /// <response code="200">Returns a 200 OK response if the edit is successful.</response>
          /// <response code="400">If the edit fails.</response>
          [HttpPut("edit/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          public async Task<bool> EditRestaurant(int id, [FromBody]RestaurantEditDTO restaurant)
          {
               return await _restaurantRepository.EditRestaurant(id, restaurant);
          }

          /// <summary>
          /// Get the menu of a restaurant by ID.
          /// </summary>
          /// <param name="id">The ID of the restaurant to retrieve the menu for.</param>
          /// <remarks>
          /// This endpoint allows you to retrieve the menu of a restaurant by specifying its ID.
          /// </remarks>
          /// <returns>Returns a 200 OK response with the restaurant's menu if found, or a 404 Not Found response if the restaurant with the specified ID is not found.</returns>
          /// <response code="200">Returns the restaurant's menu if found.</response>
          /// <response code="404">If the restaurant with the specified ID is not found.</response>
          [HttpGet("menu/{id}")]
          [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantModel))]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
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
