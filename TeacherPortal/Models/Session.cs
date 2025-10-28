namespace TeacherPortal.Models;

public class Session
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public string TimeSlot { get; set; } = string.Empty;
    public DayOfWeek[] DaysOfWeek { get; set; } = Array.Empty<DayOfWeek>();
    
    public string DaysDisplay => string.Join(", ", DaysOfWeek.Select(d => d.ToString().Substring(0, 3)));
    
    public string FullSessionName => $"{SessionName} ({TimeSlot})";
}

