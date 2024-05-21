using RentCaarsAPIs.Dtos.UserDtos;

namespace RentCaarsAPIs.Interfaces
{
    public interface IUserService
    {
        Task<UserGetDTO> GetUserAsync(int userId);
        int CreateUserAsync(UserRegisterDto user);
        Task<int> LoginUserAsync(UserLoginDto user);

        Task<int> UpdateUserAsync(UserUpdateDTO user);
        Task<int> DeleteUserAsync(int userId);
    }
}
