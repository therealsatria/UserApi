using AutoMapper;
using UserApi.DTOs;
using UserApi.Models;
using UserApi.Repositories.Interfaces;

namespace UserApi.Services.Interfaces;

public class ArchiveService : IArchiveService
{
    private readonly IArchiveRepository _archiveRepo;
    private readonly IMapper _mapper;

    public ArchiveService(IArchiveRepository archiveRepo, IMapper mapper)
    {
        _archiveRepo = archiveRepo;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ArchiveReadDto>> GetAllArchives()
    {
        var archives = await _archiveRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<ArchiveReadDto>>(archives);
    }
    public async Task<ArchiveReadDto?> GetArchiveById(Guid id)
    {
        var archive = await _archiveRepo.GetByIdAsync(id);
        return archive == null? null : _mapper.Map<ArchiveReadDto>(archive);
    }
    public async Task<ArchiveReadDto> CreateArchive(ArchiveCreateDto archiveDto)
    {
        var archive = _mapper.Map<Archive>(archiveDto);
        var createdArchive = await _archiveRepo.CreateAsync(archive);
        return _mapper.Map<ArchiveReadDto>(createdArchive);
    }
    public async Task UpdateArchive(Guid id, ArchiveCreateDto archiveDto)
    {
        var existingArchive = await _archiveRepo.GetByIdAsync(id);
        if (existingArchive == null) throw new KeyNotFoundException("Archive not found");
        
        _mapper.Map(archiveDto, existingArchive);
        await _archiveRepo.UpdateAsync(existingArchive);
    }
    public async Task DeleteArchive(Guid id)
    {
        await _archiveRepo.DeleteAsync(id);
    }

}