namespace RentCaarsAPIs.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Dtos.CarDtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("/GetCar")]
        public async Task<ActionResult<CarGetDTO>> GetCar([FromQuery]  int id)
        {
            try
            {
                var car = await _carService.GetCarAsync(id);
                if (car == null)
                {
                    return NotFound();
                }
                return car;
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("/GetCars")]
        public async Task<ActionResult<List<CarGetDTO>>> GetCars()
        {
            try
            {
                var cars = await _carService.GetListOfCarAsync();
                return cars;
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("/CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDto car)
        {
            try
            {
                await _carService.CreateCarAsync(car);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("/UpdateCar")]
        public async Task<IActionResult> UpdateCar([FromQuery] int Id, [FromBody] CarUpdateDTO car)
        {
            try
            {
                await _carService.UpdateCarAsync(Id,car);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("/DeleteCar")]
        public async Task<IActionResult> DeleteCar([FromQuery] int id)
        {
            try
            {
                await _carService.DeleteCarAsync(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

}
