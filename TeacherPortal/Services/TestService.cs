using TeacherPortal.Models;

namespace TeacherPortal.Services;

public class TestService
{
    private List<Test> _tests;

    public TestService()
    {
        _tests = new List<Test>();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        _tests = new List<Test>
        {
            new Test
            {
                Id = 1,
                TestName = "Midterm Exam",
                MaxScore = 100,
                Description = "Comprehensive midterm examination",
                ScoreComponents = new List<string> { "Total" }
            },
            new Test
            {
                Id = 2,
                TestName = "Final Exam",
                MaxScore = 100,
                Description = "Final comprehensive examination",
                ScoreComponents = new List<string> { "Total" }
            },
            new Test
            {
                Id = 3,
                TestName = "IELTS Practice",
                MaxScore = 100,
                Description = "IELTS practice test",
                ScoreComponents = new List<string> { "Reading", "Writing", "Listening", "Speaking" }
            },
            new Test
            {
                Id = 4,
                TestName = "Writing Assessment",
                MaxScore = 100,
                Description = "Focused writing assessment",
                ScoreComponents = new List<string> { "Grammar", "Vocabulary", "Content", "Structure" }
            },
            new Test
            {
                Id = 5,
                TestName = "Reading Comprehension",
                MaxScore = 50,
                Description = "Reading comprehension test",
                ScoreComponents = new List<string> { "Reading" }
            },
            new Test
            {
                Id = 6,
                TestName = "Speaking Test",
                MaxScore = 50,
                Description = "Oral proficiency assessment",
                ScoreComponents = new List<string> { "Pronunciation", "Fluency", "Accuracy", "Interaction" }
            },
            new Test
            {
                Id = 7,
                TestName = "TOEFL Practice",
                MaxScore = 120,
                Description = "TOEFL practice exam",
                ScoreComponents = new List<string> { "Reading", "Listening", "Speaking", "Writing" }
            }
        };
    }

    public List<Test> GetAllTests()
    {
        return _tests;
    }

    public Test? GetTestById(int id)
    {
        return _tests.FirstOrDefault(t => t.Id == id);
    }

    public Test? GetTestByName(string testName)
    {
        return _tests.FirstOrDefault(t => t.TestName.Equals(testName, StringComparison.OrdinalIgnoreCase));
    }
}

