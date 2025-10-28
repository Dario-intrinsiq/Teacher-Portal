# Teacher Portal - Attendance Management System

A modern web application built with .NET 9 and Blazor for managing class attendance efficiently.

## Features

- ✅ **Take Attendance**: Mark student attendance with an intuitive interface
- 📊 **Real-time Statistics**: View attendance summary with present, absent, late, and excused counts
- 📅 **Date Selection**: Review and update attendance for any date
- 💾 **Persistent Data**: Attendance records are stored and can be accessed later
- 📝 **Notes**: Add notes for individual students
- 🎨 **Modern UI**: Clean, responsive design with smooth animations

## Attendance Status Options

- **Present**: Student attended class
- **Absent**: Student did not attend
- **Late**: Student arrived late
- **Excused**: Student has an excused absence

## Quick Actions

- **Mark All Present**: Quickly mark all students as present
- **Clear All**: Reset all attendance status to "Not Marked"

## Getting Started

### Prerequisites

- .NET 9 SDK

### Running the Application

1. Navigate to the project directory:
```bash
cd TeacherPortal
```

2. Run the application:
```bash
dotnet run
```

3. Open your browser and navigate to the URL shown in the terminal (typically `https://localhost:5001` or `http://localhost:5000`)

4. Click on "Attendance" in the navigation menu to start taking attendance

## Project Structure

```
TeacherPortal/
├── Models/
│   ├── Student.cs              # Student model
│   ├── AttendanceStatus.cs     # Attendance status enum
│   └── AttendanceRecord.cs     # Attendance record model
├── Services/
│   └── AttendanceService.cs    # Service for managing attendance data
├── Components/
│   ├── Pages/
│   │   ├── Home.razor          # Home page
│   │   ├── Attendance.razor    # Attendance page
│   │   └── *.razor.css         # Page-specific styles
│   └── Layout/
│       └── NavMenu.razor       # Navigation menu
└── wwwroot/
    └── app.css                 # Global styles
```

## Sample Data

The application comes pre-loaded with 8 sample students for testing purposes:
- Emma Johnson
- Liam Smith
- Olivia Brown
- Noah Davis
- Ava Wilson
- Ethan Martinez
- Sophia Garcia
- Mason Anderson

## Technologies Used

- **Framework**: .NET 9
- **UI Framework**: Blazor Server
- **Styling**: Bootstrap 5 + Custom CSS
- **Interactivity**: Blazor InteractiveServer render mode

## Future Enhancements

- Export attendance reports to CSV/Excel
- Student management (add, edit, delete students)
- Class/course management
- Attendance analytics and trends
- Email notifications for absences
- Database integration for persistent storage
- Authentication and authorization

## License

This project is created for educational purposes.

