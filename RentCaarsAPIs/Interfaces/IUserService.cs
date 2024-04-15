using RentCaarsAPIs.Dtos.UserDtos;

namespace RentCaarsAPIs.Interfaces
{
    public interface IUserService
    {
        Task<UserGetDTO> GetUserAsync(int userId);
        Task CreateUserAsync(UserRegisterDto user);
        Task LoginUserAsync(UserLoginDto user);

        Task UpdateUserAsync(UserUpdateDTO user);
        Task DeleteUserAsync(int userId);
    }
}
