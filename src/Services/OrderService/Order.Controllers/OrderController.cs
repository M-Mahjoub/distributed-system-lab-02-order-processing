using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.CreateOrder;
using System.Threading.Tasks;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(/*CreateOrderRequest createOrderRequest*/)
        {
            await _mediator.Send(new CreateOrderCommand());

            return Ok();

        }
    }
}
