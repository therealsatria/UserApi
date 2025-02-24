using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserApi.DTOs;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArchivesController : ControllerBase
{
    private readonly IArchiveService _archiveService;
    private readonly IMapper _mapper;
    private readonly ILogger<ArchivesController> _logger;

    public ArchivesController(
        IArchiveService archiveService,
        IMapper mapper,
        ILogger<ArchivesController> logger)
    {
        _archiveService = archiveService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArchiveReadDto>>> GetAllArchives()
    {
        try
        {
            var archives = await _archiveService.GetAllArchives();
            return Ok(archives);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all archives");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetArchiveById")]
    public async Task<ActionResult<ArchiveReadDto>> GetArchiveById(int id)
    {
        try
        {
            var archive = await _archiveService.GetArchiveById(id);

            if (archive == null)
            {
                _logger.LogWarning("Archive with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(archive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving archive with id: {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPost]
    public async Task<ActionResult<ArchiveReadDto>> CreateArchive([FromBody] ArchiveCreateDto archiveCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for archive creation");
                return BadRequest(ModelState);
            }

            var createdArchive = await _archiveService.CreateArchive(archiveCreateDto);
            
            return CreatedAtRoute(nameof(GetArchiveById), 
                new { id = createdArchive.Id }, 
                createdArchive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new archive");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArchive(int id, [FromBody] ArchiveCreateDto archiveUpdateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for archive update");
                return BadRequest(ModelState);
            }

            await _archiveService.UpdateArchive(id, archiveUpdateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Archive with id: {Id} not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating archive with id: {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArchive(int id)
    {
        try
        {
            await _archiveService.DeleteArchive(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Archive with id: {Id} not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting archive with id: {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}