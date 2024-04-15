using RentCaarsAPIs.Dtos.OrderDtos;

namespace RentCaarsAPIs.Interfaces
{
    public interface IOrderService
    {
        Task<OrderGetDTO> GetOrderAsync(int orderId);
        Task<List<OrderGetDTO>> GetListOfOrderAsync();
        Task CreateOrderAsync(OrderCreateDTO order);
        Task DeleteOrderAsync(int orderId);
    }
}
