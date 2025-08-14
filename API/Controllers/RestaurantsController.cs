using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await restaurantsService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
        {
            var restaurant = await restaurantsService.GetRestaurantByIdAsync(id);
            if (restaurant is null)
            {
                return NotFound(new { Message = "Restaurant not found." });
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDTO createRestaurantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRestaurant = await restaurantsService.CreateRestaurantAsync(createRestaurantDTO);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = createdRestaurant.Id }, createdRestaurant);
        }
    }
}
