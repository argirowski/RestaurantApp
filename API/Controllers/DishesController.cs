using Application.DTOs;
using Application.Features.Dishes.Commands.Create;
using Application.Features.Dishes.Commands.Delete;
using Application.Features.Dishes.Queries.GetAll;
using Application.Features.Dishes.Queries.GetSingle;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantId, CreateDishCommand createDishCommand)
        {
            createDishCommand.RestaurantId = restaurantId;
            var dishId = await mediator.Send(createDishCommand);
            return CreatedAtAction(nameof(GetDishForRestaurantById), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.IsAdult)]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllDishesForRestaurant([FromRoute] Guid restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDTO>> GetDishForRestaurantById([FromRoute] Guid restaurantId, [FromRoute] Guid dishId)
        {
            var singleDish = await mediator.Send(new GetSingleDishForRestaurantQuery(restaurantId, dishId));
            return Ok(singleDish);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] Guid restaurantId)
        {
            await mediator.Send(new DeleteDishForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}