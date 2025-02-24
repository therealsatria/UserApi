using UserApi.DTOs;

namespace UserApi.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserReadDto>> GetAllUsers();
    Task<UserReadDto?> GetUserById(int id);
    Task<UserReadDto> CreateUser(UserCreateDto userDto);
    Task UpdateUser(int id, UserCreateDto userDto);
    Task DeleteUser(int id);
    Task<IEnumerable<ArchiveReadDto>> GetUserArchives(int userId);
}
