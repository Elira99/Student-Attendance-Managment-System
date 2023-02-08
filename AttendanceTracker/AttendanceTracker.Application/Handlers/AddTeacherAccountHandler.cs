using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AttendanceTracker.Utils;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class AddTeacherAccountHandler
    : IRequestHandler<AddTeacherAccountCommand, TeacherDTO>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public AddTeacherAccountHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<TeacherDTO> Handle(AddTeacherAccountCommand request, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;
        //TODO: get the username from principal
        var userCreated = "postman.postman@mail.server.com";

        var teacherToAdd = _mapper.Map<TeacherAccountDTO, Teacher>(request.TeacherAccount);

        teacherToAdd.DateCreated = utcNow;
        teacherToAdd.UserCreated = userCreated;

        _attendanceTrackerDbContext.Teachers.Add(teacherToAdd);

        var hashedPassword = PasswordHasher.HashPassword(request.TeacherAccount.Password);

        var account = new Account()
        {
            Active = true,
            DateCreated = utcNow,
            UserCreated = userCreated,
            Role = Roles.Teacher.ToString(),
            Password = hashedPassword.Password,
            Salt = hashedPassword.Salt,
            UserName = teacherToAdd.EmailAddress,
        };

        _attendanceTrackerDbContext.Accounts.Add(account);

        await _attendanceTrackerDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Teacher, TeacherDTO>(teacherToAdd);
    }
}


