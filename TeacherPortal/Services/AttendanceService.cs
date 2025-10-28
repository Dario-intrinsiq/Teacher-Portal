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
        // Add sample students
        _students = new List<Student>
        {
            new Student { Id = 1, FirstName = "Emma", LastName = "Johnson", Email = "emma.johnson@school.edu" },
            new Student { Id = 2, FirstName = "Liam", LastName = "Smith", Email = "liam.smith@school.edu" },
            new Student { Id = 3, FirstName = "Olivia", LastName = "Brown", Email = "olivia.brown@school.edu" },
            new Student { Id = 4, FirstName = "Noah", LastName = "Davis", Email = "noah.davis@school.edu" },
            new Student { Id = 5, FirstName = "Ava", LastName = "Wilson", Email = "ava.wilson@school.edu" },
            new Student { Id = 6, FirstName = "Ethan", LastName = "Martinez", Email = "ethan.martinez@school.edu" },
            new Student { Id = 7, FirstName = "Sophia", LastName = "Garcia", Email = "sophia.garcia@school.edu" },
            new Student { Id = 8, FirstName = "Mason", LastName = "Anderson", Email = "mason.anderson@school.edu" }
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
}

