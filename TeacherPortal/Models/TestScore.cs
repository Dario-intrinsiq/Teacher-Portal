namespace TeacherPortal.Models;

public class TestScore
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int SessionId { get; set; }
    public Session? Session { get; set; }
    public int TestId { get; set; }
    public Test? Test { get; set; }
    public DateTime TestDate { get; set; } = DateTime.Today;
    
    // For single score tests
    public decimal Score { get; set; }
    public decimal? MaxScore { get; set; }
    
    // For multi-component tests (e.g., Reading, Writing, Listening, Speaking)
    public Dictionary<string, decimal> ComponentScores { get; set; } = new Dictionary<string, decimal>();
    
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    
    public decimal TotalScore => ComponentScores.Any() 
        ? ComponentScores.Values.Sum() 
        : Score;
    
    public decimal? Percentage => MaxScore.HasValue && MaxScore.Value > 0 
        ? Math.Round((TotalScore / MaxScore.Value) * 100, 2) 
        : null;
    
    public string Grade => GetGrade();
    
    public bool HasMultipleComponents => Test?.HasMultipleComponents ?? false;
    
    private string GetGrade()
    {
        if (!Percentage.HasValue) return "N/A";
        
        var percentage = Percentage.Value;
        return percentage switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }
}

