namespace UserApi.DTOs;

public class ArchiveReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserReadDto User { get; set; } = null!;
}