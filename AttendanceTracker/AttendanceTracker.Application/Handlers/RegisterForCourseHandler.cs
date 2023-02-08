using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class RegisterForCourseHandler
    : IRequestHandler<RegisterForCourseCommand, RegistrationDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public RegisterForCourseHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<RegistrationDTO?> Handle(RegisterForCourseCommand request, CancellationToken cancellationToken)
    {
        var courseId = request.Registration.CourseId;
        var existingCourse = await _attendanceTrackerDbContext.Courses.FindAsync(new object?[] { courseId }, cancellationToken: cancellationToken);

        var studentId = request.Registration.StudentId;
        var existingStudent = await _attendanceTrackerDbContext.Students.FindAsync(new object?[] { studentId }, cancellationToken: cancellationToken);


        if (existingCourse != null && existingStudent != null)
        {

            var registration = new Registration()
            {
                StudentId = studentId,
                DateCreated = DateTime.UtcNow,
                UserCreated = request.UserAccount.UserName
            };

            existingCourse.Registrations.Add(registration);

            await _attendanceTrackerDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Registration, RegistrationDTO>(registration);
        }

        return null;

    }
}


