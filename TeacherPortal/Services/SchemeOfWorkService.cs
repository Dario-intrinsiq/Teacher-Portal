using TeacherPortal.Models;

namespace TeacherPortal.Services;

public class SchemeOfWorkService
{
    private List<SchemeOfWork> _schemes;
    private int _nextId;
    private readonly ClassService _classService;

    public SchemeOfWorkService(ClassService classService)
    {
        _classService = classService;
        _schemes = new List<SchemeOfWork>();
        _nextId = 1;
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        var today = DateTime.Today;
        
        _schemes = new List<SchemeOfWork>
        {
            new SchemeOfWork
            {
                Id = _nextId++,
                SessionId = 1,
                Session = _classService.GetSessionById(1),
                Date = today.AddDays(-2),
                Topic = "Past Simple Tense",
                Description = "Introduction to past simple tense with regular and irregular verbs. Students practiced forming sentences in the past tense.",
                Materials = "Textbook pages 45-48, Verb list handout",
                Homework = "Complete exercises 1-5 on page 49"
            },
            new SchemeOfWork
            {
                Id = _nextId++,
                SessionId = 2,
                Session = _classService.GetSessionById(2),
                Date = today.AddDays(-1),
                Topic = "Past Simple - Questions",
                Description = "Learned how to form questions using 'did' and question words in past simple tense.",
                Materials = "Worksheet: Question formation",
                Homework = "Write 10 questions about last weekend"
            },
            new SchemeOfWork
            {
                Id = _nextId++,
                SessionId = 3,
                Session = _classService.GetSessionById(3),
                Date = today.AddDays(-2),
                Topic = "Conditional Sentences Type 2",
                Description = "Explored hypothetical situations using second conditional. Practice with 'If I were/had...'",
                Materials = "Advanced Grammar book, Online exercises",
                Homework = "Write a short essay using 5 conditional sentences"
            }
        };
    }

    public List<SchemeOfWork> GetAllSchemes()
    {
        return _schemes.OrderByDescending(s => s.Date).ToList();
    }

    public List<SchemeOfWork> GetSchemesBySession(int sessionId)
    {
        return _schemes
            .Where(s => s.SessionId == sessionId)
            .OrderByDescending(s => s.Date)
            .ToList();
    }

    public List<SchemeOfWork> GetSchemesByDate(DateTime date)
    {
        return _schemes
            .Where(s => s.Date.Date == date.Date)
            .OrderBy(s => s.Session?.TimeSlot)
            .ToList();
    }

    public SchemeOfWork? GetSchemeBySessionAndDate(int sessionId, DateTime date)
    {
        return _schemes.FirstOrDefault(s => s.SessionId == sessionId && s.Date.Date == date.Date);
    }

    public SchemeOfWork? GetSchemeById(int id)
    {
        return _schemes.FirstOrDefault(s => s.Id == id);
    }

    public void AddOrUpdateScheme(SchemeOfWork scheme)
    {
        var existingScheme = GetSchemeBySessionAndDate(scheme.SessionId, scheme.Date);
        
        if (existingScheme != null)
        {
            // Update existing scheme
            existingScheme.Topic = scheme.Topic;
            existingScheme.Description = scheme.Description;
            existingScheme.Materials = scheme.Materials;
            existingScheme.Homework = scheme.Homework;
            existingScheme.UpdatedAt = DateTime.Now;
        }
        else
        {
            // Add new scheme
            scheme.Id = _nextId++;
            scheme.Session = _classService.GetSessionById(scheme.SessionId);
            scheme.CreatedAt = DateTime.Now;
            _schemes.Add(scheme);
        }
    }

    public void DeleteScheme(int id)
    {
        var scheme = _schemes.FirstOrDefault(s => s.Id == id);
        if (scheme != null)
        {
            _schemes.Remove(scheme);
        }
    }

    public Dictionary<DateTime, int> GetSchemeCountByDate(int days = 7)
    {
        var counts = new Dictionary<DateTime, int>();
        var today = DateTime.Today;

        for (int i = 0; i < days; i++)
        {
            var date = today.AddDays(-i);
            var count = _schemes.Count(s => s.Date.Date == date.Date);
            if (count > 0)
            {
                counts[date] = count;
            }
        }

        return counts;
    }
}

