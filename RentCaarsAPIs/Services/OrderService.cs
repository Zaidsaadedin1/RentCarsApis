namespace RentCaarsAPIs.Services
{
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Models;
    using RentCaarsAPIs.Dtos.OrderDtos;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RentCaarsAPIs.Data;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderGetDTO> GetOrderAsync(int orderId)
        {
            return await _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderGetDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    CarId = o.CarId,
                    RentalDate = o.RentalDate,
                    ReturnDate = o.ReturnDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrderGetDTO>> GetListOfOrderAsync()
        {
            return await _context.Orders
                .Select(o => new OrderGetDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    CarId = o.CarId,
                    RentalDate = o.RentalDate,
                    ReturnDate = o.ReturnDate
                })
                .ToListAsync();
        }

        public async Task CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.User.UserId,
                CarId = orderDto.Car.CarId,
                RentalDate = orderDto.RentalDate,
                ReturnDate = orderDto.ReturnDate
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }

}
