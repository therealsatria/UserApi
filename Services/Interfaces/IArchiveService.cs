using UserApi.DTOs;

namespace UserApi.Services.Interfaces;

public interface IArchiveService
{
    Task<IEnumerable<ArchiveReadDto>> GetAllArchives();
    Task<ArchiveReadDto?> GetArchiveById(Guid id);
    Task<ArchiveReadDto> CreateArchive(ArchiveCreateDto archiveDto);
    Task UpdateArchive(Guid id, ArchiveCreateDto archiveDto);
    Task DeleteArchive(Guid id);
}