using System;
using System.Linq;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Application.Handlers;

public class ConfirmAttendanceHandler
    : IRequestHandler<ConfirmAttendanceCommand, CourseDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public ConfirmAttendanceHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<CourseDTO?> Handle(ConfirmAttendanceCommand request, CancellationToken cancellationToken)
    {
        var courseId = request.CourseId;
        var studentId = request.UserAccount.Id;

        var registration = await _attendanceTrackerDbContext.Registrations.FirstOrDefaultAsync(reg => reg.CourseId == courseId && reg.StudentId == studentId);
        var course = await _attendanceTrackerDbContext.Courses.FindAsync(courseId);

        if (registration != null && course != null)
        {
            var now = DateTime.UtcNow;
            var dayOfWeek = (int)now.DayOfWeek;

            if (course.WeekDaysAsArray.Contains(dayOfWeek.ToString()))
            {
                var attendance = new Attendance()
                {
                    AttendanceDate = now,
                    DateCreated = now,
                    UserCreated = request.UserAccount.UserName
                };

                registration.Attendances.Add(attendance);

                return _mapper.Map<Course, CourseDTO>(course);
            }
        }

        return null;

    }
}


