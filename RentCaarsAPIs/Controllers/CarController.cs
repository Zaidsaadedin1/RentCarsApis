namespace RentCaarsAPIs.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Dtos.CarDtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RentCaarsAPIs.Models;

    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("/GetCar")]
        public async Task<ActionResult<CarGetDTO>> GetCar([FromQuery] int id)
        {
            if (id <= 0)
            {
                return NotFound("car Not found");
            }
            var car = await _carService.GetCarAsync(id);
            if (car == null)
            {
                return NotFound("car Not found");
            }
            return Ok(car);

        }

        [HttpGet("/GetCars")]
        public async Task<ActionResult<List<CarGetDTO>>> GetCars()
        {   
            var cars = await _carService.GetListOfCarAsync();
            return cars;
        }

        [HttpPost("/CreateCar")]
        public async Task<ActionResult> CreateCar([FromBody] CarCreateDto car)
        {
            if (car == null)
            {
                return BadRequest("all required fields should be filled");
            }
            var newCar = await _carService.CreateCarAsync(car);
            if (newCar == 0)
            {
                return BadRequest("all required fields should be filled");
            }
            return Ok("car created successfully");
        }

        [HttpPut("/UpdateCar")]
        public async Task<ActionResult> UpdateCar([FromQuery] int Id, [FromBody] CarUpdateDTO carUpdateDTO)
        {
            if (carUpdateDTO == null)
            {
                return BadRequest("all required fields should be filled");
            }
            if (Id <=0)
            {
                return BadRequest("car Not found");
            }
            var car = await _carService.UpdateCarAsync(Id, carUpdateDTO);
            if (car == null )
            {
                return BadRequest("car Not found");
            }
            return Ok("car updated successfully");
        }


        [HttpDelete("/DeleteCar")]
        public async Task<ActionResult> DeleteCar([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Car not found");
            }
            var res = await _carService.DeleteCarAsync(id);
            if (res == 0)
            {
                return BadRequest("Car not found");
            }
            return Ok("Car Deleted successfully");
        }
    }

}
