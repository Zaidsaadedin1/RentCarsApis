using Microsoft.AspNetCore.Mvc;
using RentCaarsAPIs.Dtos.UserDtos;
using RentCaarsAPIs.Interfaces;
using RentCaarsAPIs.Services;
using System;
using System.Threading.Tasks;

namespace RentCaarsAPIs.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
   
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] int userId)
        {
            try
            {
                var user = await _userService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("CreateUser")]
        public  IActionResult CreateUser([FromBody] UserRegisterDto userDto)
        {
            int result =  _userService.CreateUserAsync(userDto);
            if (result == 0)
            {
                return Conflict("Username already exists.");
            }
            return CreatedAtAction(nameof(GetUser), new { userId = result }, "User created successfully.");
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userDto)
        {
            int result = await _userService.UpdateUserAsync(userDto);
            if (result == 0)
            {
                return NotFound("User not found.");
            }
            return Ok("User updated successfully.");
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int userId)
        {
            int result = await _userService.DeleteUserAsync(userId);
            if (result == 0)
            {
                return NotFound("User not found.");
            }
            return Ok("User deleted successfully.");
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userDto)
        {
            int result = await _userService.LoginUserAsync(userDto);
            if (result == 0)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok("Login successful.");
        }
    }
}
