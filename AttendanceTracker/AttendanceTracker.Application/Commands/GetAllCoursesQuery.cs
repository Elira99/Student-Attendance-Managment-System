using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetAllCoursesQuery
    : IRequest<IEnumerable<CourseDTO>>
{
    public GetAllCoursesQuery()
    {
    }
}

