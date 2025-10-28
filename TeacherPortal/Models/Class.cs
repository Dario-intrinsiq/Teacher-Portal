namespace TeacherPortal.Models;

public class Class
{
    public int Id { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public List<Session> Sessions { get; set; } = new List<Session>();
}

