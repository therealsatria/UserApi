using AutoMapper;
using UserApi.DTOs;
using UserApi.Models;
using UserApi.Repositories.Interfaces;

namespace UserApi.Services.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;
    public readonly IArchiveRepository _archiveRepo;

    public UserService(IUserRepository userRepo, IMapper mapper, IArchiveRepository archiveRepo)
    {
        _userRepo = userRepo;
        _mapper = mapper;
        _archiveRepo = archiveRepo;
    }

    public async Task<IEnumerable<UserReadDto>> GetAllUsers()
    {
        var users = await _userRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<UserReadDto>>(users);
    }

    public async Task<UserReadDto?> GetUserById(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> CreateUser(UserCreateDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var createdUser = await _userRepo.CreateAsync(user);
        return _mapper.Map<UserReadDto>(createdUser);
    }

    public async Task UpdateUser(Guid id, UserCreateDto userDto)
    {
        var existingUser = await _userRepo.GetByIdAsync(id);
        if (existingUser == null) throw new KeyNotFoundException("User not found");
        
        _mapper.Map(userDto, existingUser);
        await _userRepo.UpdateAsync(existingUser);
    }

    public async Task DeleteUser(Guid id)
    {
        await _userRepo.DeleteAsync(id);
    }

    public async Task<IEnumerable<ArchiveReadDto>> GetUserArchives(Guid userId)
    {
    var user = await _userRepo.GetByIdAsync(userId);
    if (user == null) throw new KeyNotFoundException("User not found");
    
    var archives = await _archiveRepo.GetByUserIdAsync(userId);
    return _mapper.Map<IEnumerable<ArchiveReadDto>>(archives);
    }
}