using RentCaarsAPIs.Dtos.CarDtos;

namespace RentCaarsAPIs.Interfaces
{
    public interface ICarService
    {
        Task<CarGetDTO> GetCarAsync(int carId);
        Task<List<CarGetDTO>> GetListOfCarAsync();
        Task CreateCarAsync(CarCreateDto car);
        Task UpdateCarAsync(int Id,CarUpdateDTO car);
        Task DeleteCarAsync(int carId);
    }
}
