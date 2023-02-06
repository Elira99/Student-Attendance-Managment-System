using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetRegisteredStudentsForCourseQuery
    : IRequest<IEnumerable<StudentDTO>>
{
    private readonly int _courseId;

    public GetRegisteredStudentsForCourseQuery(int courseId)
    {
        _courseId = courseId;
    }

    public int CourseId => _courseId;
}

