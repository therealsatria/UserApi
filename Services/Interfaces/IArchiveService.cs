using UserApi.DTOs;

namespace UserApi.Services.Interfaces;

public interface IArchiveService
{
    Task<IEnumerable<ArchiveReadDto>> GetAllArchives();
    Task<ArchiveReadDto?> GetArchiveById(int id);
    Task<ArchiveReadDto> CreateArchive(ArchiveCreateDto archiveDto);
    Task UpdateArchive(int id, ArchiveCreateDto archiveDto);
    Task DeleteArchive(int id);
}