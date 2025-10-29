using TeacherPortal.Models;

namespace TeacherPortal.Services;

public class AttendanceService
{
    private List<Student> _students;
    private Dictionary<string, List<AttendanceRecord>> _attendanceRecords;

    public AttendanceService()
    {
        _students = new List<Student>();
        _attendanceRecords = new Dictionary<string, List<AttendanceRecord>>();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        var today = DateTime.Today;
        
        // Add sample students with ages (some under 18, some over)
        _students = new List<Student>
        {
            new Student { Id = 1, StudentId = "STU001", FirstName = "Emma", LastName = "Johnson", PreferredName = "Em", Email = "emma.johnson@school.edu", DateOfBirth = today.AddYears(-17).AddDays(-100), Gender = Gender.Female, CourseStart = today.AddMonths(-6), CourseEnd = today.AddMonths(6) },
            new Student { Id = 2, StudentId = "STU002", FirstName = "Liam", LastName = "Smith", PreferredName = "Liam", Email = "liam.smith@school.edu", DateOfBirth = today.AddYears(-19).AddDays(-50), Gender = Gender.Male, CourseStart = today.AddMonths(-4), CourseEnd = today.AddMonths(8) },
            new Student { Id = 3, StudentId = "STU003", FirstName = "Olivia", LastName = "Brown", PreferredName = "Liv", Email = "olivia.brown@school.edu", DateOfBirth = today.AddYears(-16).AddDays(-200), Gender = Gender.Female, CourseStart = today.AddMonths(-8), CourseEnd = today.AddMonths(4) },
            new Student { Id = 4, StudentId = "STU004", FirstName = "Noah", LastName = "Davis", PreferredName = "Noah", Email = "noah.davis@school.edu", DateOfBirth = today.AddYears(-20).AddDays(-150), Gender = Gender.Male, CourseStart = today.AddMonths(-12), CourseEnd = today.AddMonths(0) },
            new Student { Id = 5, StudentId = "STU005", FirstName = "Ava", LastName = "Wilson", PreferredName = "Ava", Email = "ava.wilson@school.edu", DateOfBirth = today.AddYears(-18).AddDays(-10), Gender = Gender.Female, CourseStart = today.AddMonths(-3), CourseEnd = today.AddMonths(9) },
            new Student { Id = 6, StudentId = "STU006", FirstName = "Ethan", LastName = "Martinez", PreferredName = "Ethan", Email = "ethan.martinez@school.edu", DateOfBirth = today.AddYears(-15).AddDays(-300), Gender = Gender.Male, CourseStart = today.AddMonths(-10), CourseEnd = today.AddMonths(2) },
            new Student { Id = 7, StudentId = "STU007", FirstName = "Sophia", LastName = "Garcia", PreferredName = "Sophie", Email = "sophia.garcia@school.edu", DateOfBirth = today.AddYears(-21).AddDays(-80), Gender = Gender.Female, CourseStart = today.AddMonths(-2), CourseEnd = today.AddMonths(10) },
            new Student { Id = 8, StudentId = "STU008", FirstName = "Mason", LastName = "Anderson", PreferredName = "Mase", Email = "mason.anderson@school.edu", DateOfBirth = today.AddYears(-17).AddDays(-50), Gender = Gender.Male, CourseStart = today.AddMonths(-5), CourseEnd = today.AddMonths(7) }
        };
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }

    public List<AttendanceRecord> GetAttendanceForDateAndSession(DateTime date, int sessionId)
    {
        string key = $"{date:yyyy-MM-dd}_{sessionId}";
        
        if (!_attendanceRecords.ContainsKey(key))
        {
            // Initialize attendance records for this date and session
            _attendanceRecords[key] = _students.Select(s => new AttendanceRecord
            {
                StudentId = s.Id,
                Student = s,
                SessionId = sessionId,
                Status = AttendanceStatus.NotMarked,
                Date = date
            }).ToList();
        }
        
        return _attendanceRecords[key];
    }

    public void UpdateAttendance(int studentId, DateTime date, int sessionId, AttendanceStatus status, string? notes = null)
    {
        string key = $"{date:yyyy-MM-dd}_{sessionId}";
        var records = GetAttendanceForDateAndSession(date, sessionId);
        
        var record = records.FirstOrDefault(r => r.StudentId == studentId);
        if (record != null)
        {
            record.Status = status;
            record.Notes = notes;
        }
    }

    public Dictionary<DateTime, int> GetAttendanceSummary(int days = 7)
    {
        var summary = new Dictionary<DateTime, int>();
        var today = DateTime.Today;
        
        for (int i = 0; i < days; i++)
        {
            var date = today.AddDays(-i);
            string dateKey = date.ToString("yyyy-MM-dd");
            
            if (_attendanceRecords.ContainsKey(dateKey))
            {
                var presentCount = _attendanceRecords[dateKey]
                    .Count(r => r.Status == AttendanceStatus.Present);
                summary[date] = presentCount;
            }
        }
        
        return summary;
    }

    public decimal GetAttendancePercentage(int studentId, int? sessionId = null)
    {
        // Get all attendance records for this student
        var studentRecords = _attendanceRecords.Values
            .SelectMany(records => records)
            .Where(r => r.StudentId == studentId && r.Status != AttendanceStatus.NotMarked);
        
        // Filter by session if provided
        if (sessionId.HasValue)
        {
            studentRecords = studentRecords.Where(r => r.SessionId == sessionId.Value);
        }

        var recordsList = studentRecords.ToList();
        if (recordsList.Count == 0)
            return 0m;

        var presentCount = recordsList.Count(r => r.Status == AttendanceStatus.Present);
        var totalCount = recordsList.Count;

        return totalCount > 0 ? Math.Round((decimal)presentCount / totalCount * 100, 2) : 0m;
    }

    public decimal GetCurrentAttendancePercentage(int studentId, int days = 30)
    {
        var cutoffDate = DateTime.Today.AddDays(-days);
        var studentRecords = _attendanceRecords.Values
            .SelectMany(records => records)
            .Where(r => r.StudentId == studentId && 
                       r.Date >= cutoffDate && 
                       r.Status != AttendanceStatus.NotMarked)
            .ToList();

        if (studentRecords.Count == 0)
            return 0m;

        var presentCount = studentRecords.Count(r => r.Status == AttendanceStatus.Present);
        return Math.Round((decimal)presentCount / studentRecords.Count * 100, 2);
    }
}

