using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs;

public class ArchiveCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public Guid UserId { get; set; }
}