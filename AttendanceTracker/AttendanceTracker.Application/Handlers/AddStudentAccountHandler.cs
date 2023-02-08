using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AttendanceTracker.Utils;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class AddStudentAccountHandler
    : IRequestHandler<AddStudentAccountCommand, StudentDTO>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public AddStudentAccountHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<StudentDTO> Handle(AddStudentAccountCommand request, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;
        //TODO: get the username from principal
        var userCreated = "postman.postman@mail.server.com";

        var studentToAdd = _mapper.Map<StudentAccountDTO, Student>(request.StudentAccount);

        studentToAdd.DateCreated = utcNow;
        studentToAdd.UserCreated = userCreated;

        _attendanceTrackerDbContext.Students.Add(studentToAdd);

        var hashedPassword = PasswordHasher.HashPassword(request.StudentAccount.Password);

        var account = new Account()
        {
            Active = true,
            DateCreated = utcNow,
            UserCreated = userCreated,
            Role = Roles.Student.ToString(),
            Password = hashedPassword.Password,
            Salt = hashedPassword.Salt,
            UserName = studentToAdd.EmailAddress,
        };

        _attendanceTrackerDbContext.Accounts.Add(account);

        await _attendanceTrackerDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Student, StudentDTO>(studentToAdd);
    }
}


