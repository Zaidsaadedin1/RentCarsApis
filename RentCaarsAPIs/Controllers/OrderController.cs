using Microsoft.AspNetCore.Mvc;
using RentCaarsAPIs.Interfaces;
using RentCaarsAPIs.Dtos.OrderDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentCaarsAPIs.Dtos.CarDtos;

namespace RentCaarsAPIs.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("/GetOrder")]
        public async Task<ActionResult<OrderGetDTO>> GetOrder([FromQuery] int orderid)
        {
            if (orderid <= 0)
            {
                return BadRequest("Invalid order ID. Please provide a valid positive integer.");
            }
            var order = await _orderService.GetOrderAsync(orderid);
            if (order == null)
            {
                return NotFound($"Order with ID {orderid} not found.");
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
        public async Task<ActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            if (order == null)
            {
                return BadRequest("Order data is required.");
            }

            var newOrderId = await _orderService.CreateOrderAsync(order);
            if (newOrderId ==-1)
            {
                return BadRequest("this car is ordered already ");
            }
            if (newOrderId == -2)
            {
                return BadRequest("Wrong car or user ID  ");
            }
            return Ok("Order is created successfully");
        }

        [HttpDelete("/DeleteOrder")]
        public async Task<ActionResult> DeleteOrder([FromQuery] int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest("Invalid order ID. Please provide a valid positive integer.");
            }

            var result = await _orderService.DeleteOrderAsync(orderId);
            if (result == 0)
            {
                return NotFound($"Order with ID {orderId} not found or could not be deleted.");
            }
            return Ok("Order deleted successfully");
        }

        [HttpGet("/GetUserOrdersAsync")]
        public async Task<ActionResult<List<OrderGetDTO>>> GetUserOrdersAsync([FromQuery] int userId)
        {
            var result = await _orderService.GetUserOrdersAsync(userId);
            return result;
        }

        [HttpDelete("/DeleteUserOrdersAsync")]
        public async Task<ActionResult> DeleteUserOrdersAsync([FromQuery] int userId, [FromQuery] int orderId)
        {
            if (userId <= 0 || orderId <= 0)
            {
                return BadRequest("Invalid order ID or userId. Please provide a valid positive integer.");
            }

            var result = await _orderService.DeleteUserOrdersAsync(userId, orderId);
            if (result == 0)
            {
                return NotFound($"Order with orderId {orderId} and userId {userId} not found or could not be deleted.");
            }
            return Ok("You have deleted your order successfully");
        }
    }
}
