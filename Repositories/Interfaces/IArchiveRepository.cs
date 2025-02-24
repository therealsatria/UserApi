using UserApi.Models;

namespace UserApi.Repositories.Interfaces;

public interface IArchiveRepository
{
    Task<IEnumerable<Archive>> GetAllAsync();
    Task<Archive?> GetByIdAsync(Guid id);
    Task<Archive> CreateAsync(Archive archive);
    Task<Archive> UpdateAsync(Archive archive);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Archive>> GetByUserIdAsync(Guid userId);
}