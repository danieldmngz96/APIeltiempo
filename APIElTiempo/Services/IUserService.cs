using APIElTiempo.Models;

namespace APIElTiempo.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(int userId, UserDto userDto);
        Task DeleteUserAsync(int userId);

    }
}
