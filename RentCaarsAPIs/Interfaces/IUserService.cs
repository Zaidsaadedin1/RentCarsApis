using RentCaarsAPIs.Dtos.UserDtos;

namespace RentCaarsAPIs.Interfaces
{
    public interface IUserService
    {
        Task<UserGetDTO> GetUserAsync(int userId);
        Task<List<UserGetDTO>> GetUsersAsync();
        Task<int> CreateUserAsync(UserRegisterDto user);
        Task<int> LoginUserAsync(UserLoginDto user);

        Task<int> UpdateUserAsync(UserUpdateDTO userDto, int id);
        Task<int> DeleteUserAsync(int userId);
    }
}
