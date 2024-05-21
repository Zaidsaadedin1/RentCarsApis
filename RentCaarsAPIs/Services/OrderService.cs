using RentCaarsAPIs.Interfaces;
using RentCaarsAPIs.Models;
using RentCaarsAPIs.Dtos.OrderDtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentCaarsAPIs.Data;

namespace RentCaarsAPIs.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderGetDTO> GetOrderAsync(int orderId)
        {
            if (orderId <= 0)
            {
                return null;
            }
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }

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
            var orders = await _context.Orders
                .Select(o => new OrderGetDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    CarId = o.CarId,
                    RentalDate = o.RentalDate,
                    ReturnDate = o.ReturnDate
                })
                .ToListAsync();

            return orders;
        }

        public async Task<int> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var isOrdered = await _context.Orders.FirstOrDefaultAsync(o => o.UserId  == orderDto.UserId && o.CarId == orderDto.CarId);
            if (isOrdered != null)
            {
                return -1;
            }
            var IsUserExist = await _context.Users.FindAsync(orderDto.UserId);
            var isCarExist = await _context.Cars.FindAsync(orderDto.CarId);
            if (IsUserExist == null || isCarExist == null)
            {
                return -2;
            }
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
            if (order == null)
            {
                return 0; // Not found
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return 1; // Successfully deleted
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

        public async Task<int> DeleteUserOrderAsync(int userId, int orderId)
        {
            if (userId <= 0 || orderId <= 0)
            {
                return 0;
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId);
            if (order == null)
            {
                return 0; // Not found
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return 1; // Successfully deleted
        }
    }
}
