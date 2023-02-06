using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class UpdateCourseCommand
    : IRequest<CourseDTO>
{
    private readonly CourseDTO _course;

    public UpdateCourseCommand(CourseDTO course)
    {
        _course = course;
    }

    public CourseDTO Course => _course;
}

