namespace RentCaarsAPIs.Services
{
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Models;
    using RentCaarsAPIs.Dtos.CarDtos;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RentCaarsAPIs.Data;

    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarGetDTO> GetCarAsync(int carId)
        {
            var car = await _context.Cars
                .Where(c => c.CarId == carId)
                .Select(c => new CarGetDTO
                {
                    CarId = c.CarId,
                    Model = c.Model,
                    Brand = c.Brand,
                    LicenseNumber = c.LicenseNumber,
                    IsAvailable = c.IsAvailable,
                    Color = c.Color,
                    Year = c.Year,
                    PricePerDay = c.PricePerDay,
                    ImageUrl = c.ImageUrl,
                    Mileage = c.Mileage,
                    Description = c.Description,
                    UserId = c.UserId
                })
                .FirstOrDefaultAsync();

            return car;
        }

        public async Task<List<CarGetDTO>> GetListOfCarAsync()
        {
            return await _context.Cars
                .Select(c => new CarGetDTO
                {
                    CarId = c.CarId,
                    Model = c.Model,
                    Brand = c.Brand,
                    LicenseNumber = c.LicenseNumber,
                    IsAvailable = c.IsAvailable,
                    Color = c.Color,
                    Year = c.Year,
                    PricePerDay = c.PricePerDay,
                    ImageUrl = c.ImageUrl,
                    Mileage = c.Mileage,
                    Description = c.Description,
                    UserId = c.UserId
                })
                .ToListAsync();
        }

        public async Task CreateCarAsync(CarCreateDto carDto)
        {
            var car = new Car
            {
                Model = carDto.Model,
                Brand = carDto.Brand,
                LicenseNumber = carDto.LicenseNumber,
                IsAvailable = carDto.IsAvailable,
                Color = carDto.Color,
                Year = carDto.Year,
                PricePerDay = carDto.PricePerDay,
                ImageUrl = carDto.ImageUrl,
                Mileage = carDto.Mileage,
                Description = carDto.Description,
                UserId = carDto.UserId
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(CarUpdateDTO carDto)
        {
            var car = await _context.Cars.FindAsync(carDto.UserId);
            if (car != null)
            {
                // Map properties from DTO to existing car model
                car.Model = carDto.Model;
                car.Brand = carDto.Brand;
                car.LicenseNumber = carDto.LicenseNumber;
                car.IsAvailable = carDto.IsAvailable;
                car.Color = carDto.Color;
                car.Year = carDto.Year;
                car.PricePerDay = carDto.PricePerDay;
                car.ImageUrl = carDto.ImageUrl;
                car.Mileage = carDto.Mileage;
                car.Description = carDto.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCarAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }

}
