using Microsoft.EntityFrameworkCore;
using RentCaarsAPIs.Data;
using RentCaarsAPIs.Dtos.CarDtos;
using RentCaarsAPIs.Interfaces;
using RentCaarsAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCaarsAPIs.Services
{
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
                .Where(c => c.Id == carId)
                .Select(c => new CarGetDTO
                {
                    CarId = c.Id,
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
            var cars = await _context.Cars
                .Select(c => new CarGetDTO
                {
                    CarId = c.Id,
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

            return cars;
        }

        public async Task<int> CreateCarAsync(CarCreateDto carDto)
        {   var user = await _context.Users.FindAsync(carDto.UserId);
            if(user == null)
            {
                return 0;
            }
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
            return car.Id;
        }

        public async Task<Car> UpdateCarAsync(int Id, CarUpdateDTO carDto)
        {
            var car = await _context.Cars.FindAsync(Id);

            if (car == null)
            {
                return null;
            }

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
            return car;
        }

        public async Task<int> DeleteCarAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                return 0;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
