using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class GetCoursesListHandler
    : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDTO>>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetCoursesListHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public Task<IEnumerable<CourseDTO>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_attendanceTrackerDbContext.Courses.Select(c => _mapper.Map<Course, CourseDTO>(c)).AsEnumerable());
    }
}

