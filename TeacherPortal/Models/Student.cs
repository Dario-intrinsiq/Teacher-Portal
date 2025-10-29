namespace TeacherPortal.Models;

public enum Gender
{
    Male,
    Female,
    Other,
    PreferNotToSay
}

public class Student
{
    public int Id { get; set; }
    public string StudentId { get; set; } = string.Empty; // Student ID (e.g., "STU001")
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PreferredName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; } = Gender.PreferNotToSay;
    public DateTime CourseStart { get; set; }
    public DateTime? CourseEnd { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
    
    public int Age => DateTime.Now.Year - DateOfBirth.Year - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
    
    public bool IsUnder18 => Age < 18;
}

