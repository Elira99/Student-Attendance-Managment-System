using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetCourseByIdQuery
    : IRequest<CourseDTO>
{
    private readonly int _courseId;

    public GetCourseByIdQuery(int courseId)
    {
        _courseId = courseId;
    }

    public int CourseId => _courseId;
}

