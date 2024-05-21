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
        public async Task<List<UserGetDTO>> GetUsersAsync()
        {
            var users = await _context.Users
                .Select(u => new UserGetDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    IsAdmin = u.IsAdmin,
                    Cars = u.Cars.ToList(),
                    Orders = u.Orders.ToList()
                })
                .ToListAsync();

            return users;
        }


        public async Task<int> CreateUserAsync(UserRegisterDto userDto)
        {
            var existingUser =  await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
            if (existingUser != null)
            {
                return 0;
            }

            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                IsAdmin = userDto.IsAdmin
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UpdateUserAsync(UserUpdateDTO userDto , int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return 0;
            }

            user.Password = userDto.Password;
            user.Username = userDto.Username;

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
                .FirstOrDefaultAsync(u => u.Username == userDto.Username && userDto.Password== u.Password);

            if (user == null)
            {
                return 0;
            }

            return 1;
        }
    }
}
