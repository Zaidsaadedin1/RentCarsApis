using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCaarsAPIs.Data;
using RentCaarsAPIs.Dtos.UserDtos;
using RentCaarsAPIs.Interfaces;
using RentCaarsAPIs.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RentCaarsAPIs.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserGetDTO> GetUserAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserGetDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    IsAdmin = u.IsAdmin,
                    Cars = u.Cars.ToList(),
                    Orders = u.Orders.ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public int CreateUserAsync(UserRegisterDto userDto)
        {
            var existingUser =  _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
            if (existingUser != null)
            {
                return 0;
            }

            var user = new User
            {
                Username = userDto.Username,
                Password = HashPassword(userDto.Password),
                IsAdmin = userDto.IsAdmin
            };

            _context.Users.Add(user);
            _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UpdateUserAsync(UserUpdateDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
            if (user == null)
            {
                return 0;
            }

            user.Password = HashPassword(userDto.Password);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return 0;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> LoginUserAsync(UserLoginDto userDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userDto.Username && VerifyPassword(userDto.Password, u.Password));

            if (user == null)
            {
                return 0;
            }

            return 1;
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
