using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class AddCourseHandler
    : IRequestHandler<AddCourseCommand, CourseDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public AddCourseHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<CourseDTO?> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {

        var teacher = _attendanceTrackerDbContext.Teachers.Find(request.Course.TeacherId);

        if (teacher != null)
        {
            var courseToAdd = _mapper.Map<CourseDTO, Course>(request.Course);

            courseToAdd.DateCreated = DateTime.UtcNow;
            courseToAdd.UserCreated = request.UserAccount.UserName;

            teacher.Courses.Add(courseToAdd);

            await _attendanceTrackerDbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<Course, CourseDTO>(courseToAdd!);
        }

        return null;

    }
}


