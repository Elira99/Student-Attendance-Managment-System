using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class UpdateCourseHandler
    : IRequestHandler<UpdateCourseCommand, CourseDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public UpdateCourseHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<CourseDTO?> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var courseId = request.Course.Id;
        var existingCourse = await _attendanceTrackerDbContext.Courses.FindAsync(new object?[] { courseId }, cancellationToken: cancellationToken);

        if (existingCourse != null)
        {
            existingCourse = _mapper.Map<CourseDTO, Course>(request.Course, existingCourse);

            existingCourse.DateUpdated = DateTime.UtcNow;
            //TODO: get the username from principal
            existingCourse.UserUpdated = "postman.postman@mail.server.com";

            await _attendanceTrackerDbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<Course, CourseDTO>(existingCourse);
        }

        return null;

    }
}


