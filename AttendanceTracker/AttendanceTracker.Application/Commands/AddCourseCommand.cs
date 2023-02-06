using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class AddCourseCommand
    : IRequest<CourseDTO>
{
    private readonly CourseDTO _course;

    public AddCourseCommand(CourseDTO course)
    {
        _course = course;
    }

    public CourseDTO Course => _course;
}

