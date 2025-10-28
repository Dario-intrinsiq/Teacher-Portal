namespace TeacherPortal.Models;

public class SchemeOfWork
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public Session? Session { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public string Topic { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Materials { get; set; }
    public string? Homework { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}

