namespace RentCaarsAPIs.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Dtos.CarDtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarGetDTO>> GetCar(int id)
        {
            var car = await _carService.GetCarAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarGetDTO>>> GetCars()
        {
            var cars = await _carService.GetListOfCarAsync();
            return cars;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDto car)
        {
            await _carService.CreateCarAsync(car);
            return CreatedAtAction(nameof(GetCar), new { id = car.UserId }, car);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromBody] CarUpdateDTO car)
        {
            await _carService.UpdateCarAsync(car);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }
    }

}
