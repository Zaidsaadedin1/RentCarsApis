namespace RentCaarsAPIs.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Dtos.OrderDtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("/GetOrder")]
        public async Task<ActionResult<OrderGetDTO>> GetOrder([FromQuery] int id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("/GetOrders")]
        public async Task<ActionResult<List<OrderGetDTO>>> GetOrders()
        {
            var orders = await _orderService.GetListOfOrderAsync();
            return Ok(orders);
        }

        [HttpPost("/CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.UserId }, order); 
        }

        [HttpDelete("/DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromQuery] int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }

}
