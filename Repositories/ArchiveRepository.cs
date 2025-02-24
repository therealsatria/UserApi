using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Repositories.Interfaces;

public class ArchiveRepository : IArchiveRepository
{
    private readonly ApplicationDbContext _context;

    public ArchiveRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Archive>> GetAllAsync() => 
        await _context.Archives.Include(a => a.User).ToListAsync();

    public async Task<Archive?> GetByIdAsync(int id) => 
        await _context.Archives.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Archive> CreateAsync(Archive archive)
    {
        _context.Archives.Add(archive);
        await _context.SaveChangesAsync();
        return archive;
    }

    public async Task<Archive> UpdateAsync(Archive archive)
    {
        _context.Entry(archive).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return archive;
    }

    public async Task DeleteAsync(int id)
    {
        var archive = await GetByIdAsync(id);
        if (archive != null)
        {
            _context.Archives.Remove(archive);
            await _context.SaveChangesAsync();
        }
    }

    public  async Task<IEnumerable<Archive>> GetByUserIdAsync(int userId)
    {
        return await _context.Archives.Where(a => a.UserId == userId).Include(a => a.User).ToListAsync();
        // Include(a => a.User) is used to include related user information when retrieving archives by user id.
        // Without it, only the archive details will be returned.
        // If you don't need related user information, you can remove this line.
        // This can improve performance when retrieving archives by user id.
        // Note: Make sure to update the corresponding service method as well.
        // This method is used in the UserController.cs file.
        // Example:
        // public async Task<IActionResult> GetArchivesByUserId(int userId)
        // {
        //     var archives = await _archiveRepository.GetByUserIdAsync(userId);
        //     return Ok(archives);
        // }    
        // The rest of the code remains the same.
    }
}