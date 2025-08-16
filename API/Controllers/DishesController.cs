using Application.DTOs;
using Application.Features.Dishes.Commands.Create;
using Application.Features.Dishes.Queries.GetAll;
using Application.Features.Dishes.Queries.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantId, CreateDishCommand createDishCommand)
        {
            createDishCommand.RestaurantId = restaurantId;
            await mediator.Send(createDishCommand);
            return Created();
        }

        [HttpGet]
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
    }
}