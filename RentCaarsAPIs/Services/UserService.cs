namespace RentCaarsAPIs.Services
{
    using RentCaarsAPIs.Interfaces;
    using RentCaarsAPIs.Models;
    using RentCaarsAPIs.Dtos.UserDtos;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq;
    using RentCaarsAPIs.Data;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserGetDTO> GetUserAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserGetDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Password = u.Password,
                    IsAdmin = u.IsAdmin,
                    Cars = u.Cars.ToList(),
                    Orders = u.Orders.ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(UserRegisterDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password, // Note: Password should be hashed in a real application
                IsAdmin = userDto.IsAdmin
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserUpdateDTO userDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if (user != null)
            {
                user.Password = userDto.Password; // Update with hash in a real application
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task LoginUserAsync(UserLoginDto userDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userDto.Username && u.Password == userDto.Password);

            // Login logic here. In real applications, consider security implications.
        }
    }

}
