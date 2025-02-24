using UserApi.Models;

namespace UserApi.Repositories.Interfaces;

public interface IArchiveRepository
{
    Task<IEnumerable<Archive>> GetAllAsync();
    Task<Archive?> GetByIdAsync(int id);
    Task<Archive> CreateAsync(Archive archive);
    Task<Archive> UpdateAsync(Archive archive);
    Task DeleteAsync(int id);
    Task<IEnumerable<Archive>> GetByUserIdAsync(int userId);
}