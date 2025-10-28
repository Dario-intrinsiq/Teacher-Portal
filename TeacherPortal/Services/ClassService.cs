using TeacherPortal.Models;

namespace TeacherPortal.Services;

public class ClassService
{
    private List<Class> _classes;

    public ClassService()
    {
        _classes = new List<Class>();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        _classes = new List<Class>
        {
            new Class 
            { 
                Id = 1, 
                ClassName = "A1 Int", 
                Subject = "Intermediate English",
                RoomNumber = "Room 101",
                Sessions = new List<Session>
                {
                    new Session 
                    { 
                        Id = 1, 
                        ClassId = 1, 
                        SessionName = "Session 1", 
                        TimeSlot = "9:00 - 10:00",
                        DaysOfWeek = new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }
                    },
                    new Session 
                    { 
                        Id = 2, 
                        ClassId = 1, 
                        SessionName = "Session 2", 
                        TimeSlot = "11:00 - 12:00",
                        DaysOfWeek = new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }
                    }
                }
            },
            new Class 
            { 
                Id = 2, 
                ClassName = "B2 Adv", 
                Subject = "Advanced English",
                RoomNumber = "Room 102",
                Sessions = new List<Session>
                {
                    new Session 
                    { 
                        Id = 3, 
                        ClassId = 2, 
                        SessionName = "Session 1", 
                        TimeSlot = "10:15 - 11:15",
                        DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday }
                    }
                }
            },
            new Class 
            { 
                Id = 3, 
                ClassName = "C1 Prof", 
                Subject = "Professional English",
                RoomNumber = "Room 103",
                Sessions = new List<Session>
                {
                    new Session 
                    { 
                        Id = 4, 
                        ClassId = 3, 
                        SessionName = "Session 1", 
                        TimeSlot = "13:30 - 14:30",
                        DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday }
                    },
                    new Session 
                    { 
                        Id = 5, 
                        ClassId = 3, 
                        SessionName = "Session 2", 
                        TimeSlot = "15:00 - 16:00",
                        DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday }
                    }
                }
            },
            new Class 
            { 
                Id = 4, 
                ClassName = "A2 Elem", 
                Subject = "Elementary English",
                RoomNumber = "Room 104",
                Sessions = new List<Session>
                {
                    new Session 
                    { 
                        Id = 6, 
                        ClassId = 4, 
                        SessionName = "Session 1", 
                        TimeSlot = "14:45 - 15:45",
                        DaysOfWeek = new[] { DayOfWeek.Monday, DayOfWeek.Friday }
                    }
                }
            }
        };
    }

    public List<Class> GetAllClasses()
    {
        return _classes;
    }

    public Class? GetClassById(int id)
    {
        return _classes.FirstOrDefault(c => c.Id == id);
    }

    public List<Session> GetAllSessions()
    {
        return _classes.SelectMany(c => c.Sessions).ToList();
    }

    public Session? GetSessionById(int sessionId)
    {
        return _classes.SelectMany(c => c.Sessions).FirstOrDefault(s => s.Id == sessionId);
    }

    public List<Session> GetSessionsByClass(int classId)
    {
        var cls = GetClassById(classId);
        return cls?.Sessions ?? new List<Session>();
    }

    public List<Session> GetSessionsByDay(DayOfWeek day)
    {
        return _classes
            .SelectMany(c => c.Sessions)
            .Where(s => s.DaysOfWeek.Contains(day))
            .OrderBy(s => TimeSpan.Parse(s.TimeSlot.Split('-')[0].Trim()))
            .ToList();
    }
}

