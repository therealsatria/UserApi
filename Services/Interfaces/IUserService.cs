using UserApi.DTOs;

namespace UserApi.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserReadDto>> GetAllUsers();
    Task<UserReadDto?> GetUserById(Guid id);
    Task<UserReadDto> CreateUser(UserCreateDto userDto);
    Task UpdateUser(Guid id, UserCreateDto userDto);
    Task DeleteUser(Guid id);
    Task<IEnumerable<ArchiveReadDto>> GetUserArchives(Guid userId);
}
