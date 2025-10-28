namespace TeacherPortal.Models;

public class AttendanceRecord
{
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int SessionId { get; set; }
    public Session? Session { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.NotMarked;
    public DateTime Date { get; set; } = DateTime.Today;
    public string? Notes { get; set; }
}

