using System;

namespace AttendanceTracker.Application.Dto;


public class RegistrationDTO
{
    public RegistrationDTO()
    {
    }

    public int Id { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }
}

