using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class GetCourseByIdHandler
    : IRequestHandler<GetCourseByIdQuery, CourseDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetCourseByIdHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<CourseDTO?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _attendanceTrackerDbContext.Courses.FindAsync(new object?[] { request.CourseId }, cancellationToken: cancellationToken);
        return course != null ? _mapper.Map<Course, CourseDTO>(course) : null;
    }
}

