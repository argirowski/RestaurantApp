using Application.Features.Commands.Create;
using Application.Features.Commands.Delete;
using Application.Features.Queries.GetAll;
using Application.Features.Queries.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
        {
            var restaurant = await mediator.Send(new GetSingleRestaurantQuery(id));
            if (restaurant is null)
            {
                return NotFound(new { Message = "Restaurant not found." });
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRestaurant = await mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = createdRestaurant.Id }, createdRestaurant);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] Guid id)
        {
            var result = await mediator.Send(new DeleteRestaurantCommand(id));
            if (result)
            {
                return NoContent();
            }
            return NotFound(new { Message = "Restaurant not found." });
        }
    }
}