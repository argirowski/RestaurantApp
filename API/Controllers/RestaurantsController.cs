using Application.DTOs;
using Application.Features.Restaurants.Commands.Create;
using Application.Features.Restaurants.Commands.Delete;
using Application.Features.Restaurants.Commands.Update;
using Application.Features.Restaurants.Queries.GetAll;
using Application.Features.Restaurants.Queries.GetSingle;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "CreatedAtLeastTwoRestaurants")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAllRestaurants([FromQuery] GetAllRestaurantsQuery getAllRestaurantsQuery)
        {
            var restaurants = await mediator.Send(getAllRestaurantsQuery);
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDTO?>> GetRestaurantById([FromRoute] Guid id)
        {
            var restaurant = await mediator.Send(new GetSingleRestaurantQuery(id));
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            var createdRestaurant = await mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = createdRestaurant.Id }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] Guid id)
        {
            var result = await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}