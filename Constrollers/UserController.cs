using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserApi.DTOs;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        ILogger<UsersController> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserReadDto>> GetUserById(Guid id)
    {
        try
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                _logger.LogWarning("User with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user with id: {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for user creation");
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUser(userCreateDto);
            
            return CreatedAtRoute(nameof(GetUserById), 
                new { id = createdUser.Id }, 
                createdUser);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument error: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new user");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserCreateDto userUpdateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for user update");
                return BadRequest(ModelState);
            }

            await _userService.UpdateUser(id, userUpdateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "User with id: {Id} not found", id);
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument error: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user with id: {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "User with id: {Id} not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user with id: {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{userId}/archives")]
    public async Task<ActionResult<IEnumerable<ArchiveReadDto>>> GetUserArchives(Guid userId)
    {
        try
        {
            var archives = await _userService.GetUserArchives(userId);
            return Ok(archives);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "User with id: {Id} not found", userId);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving archives for user id: {Id}", userId);
            return StatusCode(500, "Internal server error");
        }
    }
}