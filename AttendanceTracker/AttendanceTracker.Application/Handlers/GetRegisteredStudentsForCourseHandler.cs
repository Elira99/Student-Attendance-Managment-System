using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Application.Handlers;

public class GetRegisteredStudentsForCourseHandler
    : IRequestHandler<GetRegisteredStudentsForCourseQuery, IEnumerable<StudentDTO>?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetRegisteredStudentsForCourseHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDTO>?> Handle(GetRegisteredStudentsForCourseQuery request, CancellationToken cancellationToken)
    {
        var courseId = request.CourseId;
        var course = await _attendanceTrackerDbContext.Courses
                    .FindAsync(new object?[] { courseId }, cancellationToken: cancellationToken);

        if (course != null)
        {
            var students = await _attendanceTrackerDbContext.Registrations
                            .Include(reg => reg.Student)
                            .Where(reg => reg.CourseId == courseId)
                            .Select(reg => reg.Student)
                            .ToListAsync();
            return _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(students);
        }

        return null;

    }
}

