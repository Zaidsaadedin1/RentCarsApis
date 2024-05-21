using RentCaarsAPIs.Dtos.CarDtos;
using RentCaarsAPIs.Models;

namespace RentCaarsAPIs.Interfaces
{
    public interface ICarService
    {
        Task<CarGetDTO> GetCarAsync(int carId);
        Task<List<CarGetDTO>> GetListOfCarAsync();
        Task<int> CreateCarAsync(CarCreateDto car);
        Task<Car> UpdateCarAsync(int Id, CarUpdateDTO carDto);
        Task<int> DeleteCarAsync(int carId);
    }
}
