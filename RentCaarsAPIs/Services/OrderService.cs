namespace RentCaarsAPIs.Services
{
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Models;
    using RentCaarsAPIs.Dtos.OrderDtos;
    using Microsoft.EntityFrameworkCore;
    using System;
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

        public async Task<int> CreateOrderAsync(OrderCreateDTO orderDto)
        {

            var order = new Order
            {
                UserId = orderDto.UserId,
                CarId = orderDto.CarId,
                RentalDate = orderDto.RentalDate,
                ReturnDate = orderDto.ReturnDate
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<int> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<List<OrderGetDTO>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
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

        public async Task<int> DeleteUserOrdersAsync(int userId, int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

     
    }
}
