using TeacherPortal.Models;

namespace TeacherPortal.Services;

public class TestScoreService
{
    private List<TestScore> _testScores;
    private int _nextId;
    private readonly ClassService _classService;
    private readonly AttendanceService _attendanceService;
    private readonly TestService _testService;

    public TestScoreService(ClassService classService, AttendanceService attendanceService, TestService testService)
    {
        _classService = classService;
        _attendanceService = attendanceService;
        _testService = testService;
        _testScores = new List<TestScore>();
        _nextId = 1;
    }

    public List<TestScore> GetAllTestScores()
    {
        return _testScores.OrderByDescending(t => t.TestDate).ToList();
    }

    public List<TestScore> GetTestScoresBySession(int sessionId)
    {
        return _testScores
            .Where(t => t.SessionId == sessionId)
            .OrderByDescending(t => t.TestDate)
            .ToList();
    }

    public List<TestScore> GetTestScoresByStudent(int studentId)
    {
        return _testScores
            .Where(t => t.StudentId == studentId)
            .OrderByDescending(t => t.TestDate)
            .ToList();
    }

    public TestScore? GetTestScoreById(int id)
    {
        return _testScores.FirstOrDefault(t => t.Id == id);
    }

    public void AddOrUpdateTestScore(TestScore testScore)
    {
        var existing = _testScores.FirstOrDefault(t => 
            t.Id == testScore.Id && testScore.Id > 0);
        
        if (existing != null)
        {
            // Update existing
            existing.Score = testScore.Score;
            existing.MaxScore = testScore.MaxScore;
            existing.ComponentScores = testScore.ComponentScores;
            existing.TestId = testScore.TestId;
            existing.TestDate = testScore.TestDate;
            existing.Notes = testScore.Notes;
            existing.UpdatedAt = DateTime.Now;
        }
        else
        {
            // Add new
            testScore.Id = _nextId++;
            testScore.Student = _attendanceService.GetAllStudents().FirstOrDefault(s => s.Id == testScore.StudentId);
            testScore.Session = _classService.GetSessionById(testScore.SessionId);
            testScore.Test = _testService.GetTestById(testScore.TestId);
            testScore.CreatedAt = DateTime.Now;
            _testScores.Add(testScore);
        }
    }

    public void DeleteTestScore(int id)
    {
        var testScore = _testScores.FirstOrDefault(t => t.Id == id);
        if (testScore != null)
        {
            _testScores.Remove(testScore);
        }
    }

    public Dictionary<int, decimal> GetAverageScoresBySession(int sessionId)
    {
        var scores = _testScores.Where(t => t.SessionId == sessionId);
        return scores
            .GroupBy(t => t.StudentId)
            .ToDictionary(g => g.Key, g => (decimal)g.Average(t => t.Percentage ?? 0));
    }

    public TestScore? GetLastTestResult(int studentId)
    {
        return _testScores
            .Where(t => t.StudentId == studentId)
            .OrderByDescending(t => t.TestDate)
            .ThenByDescending(t => t.CreatedAt)
            .FirstOrDefault();
    }

    public decimal? GetAverageTestResult(int studentId)
    {
        var scores = _testScores
            .Where(t => t.StudentId == studentId && t.Percentage.HasValue)
            .Select(t => t.Percentage!.Value)
            .ToList();

        if (scores.Count == 0)
            return null;

        return Math.Round(scores.Average(), 2);
    }
}

