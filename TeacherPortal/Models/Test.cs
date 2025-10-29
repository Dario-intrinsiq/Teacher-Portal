namespace TeacherPortal.Models;

public class Test
{
    public int Id { get; set; }
    public string TestName { get; set; } = string.Empty;
    public List<string> ScoreComponents { get; set; } = new List<string>();
    public decimal MaxScore { get; set; } = 100;
    public string Description { get; set; } = string.Empty;
    
    public bool HasMultipleComponents => ScoreComponents.Count > 1;
}

