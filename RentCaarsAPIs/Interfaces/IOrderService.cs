using RentCaarsAPIs.Dtos.OrderDtos;

namespace RentCaarsAPIs.Interfaces
{
    public interface IOrderService
    {
        Task<OrderGetDTO> GetOrderAsync(int orderId);
        Task<List<OrderGetDTO>> GetListOfOrderAsync();
        Task<int> CreateOrderAsync(OrderCreateDTO orderDto);
        Task<int> DeleteOrderAsync(int orderId);
        Task<List<OrderGetDTO>> GetUserOrdersAsync(int userId);
        Task<int> DeleteUserOrdersAsync(int userId , int orderId);
        
    }
}
