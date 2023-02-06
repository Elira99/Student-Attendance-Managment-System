namespace AttendanceTracker.Application.Dto;

public class CourseDTO
{
    public CourseDTO()
    {
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TeacherId { get; set; }

    public string WeekDays { get; set; }

}

