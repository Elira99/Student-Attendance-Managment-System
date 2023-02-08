using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AttendanceTracker.Utils;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Application.Handlers;

public class GetUserAccountHandler
    : IRequestHandler<GetUserAccountQuery, UserAccountDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetUserAccountHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<UserAccountDTO?> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Login?.UserName) && !string.IsNullOrWhiteSpace((request?.Login.Password)))
        {
            var username = request.Login.UserName.ToLower();
            var account = await _attendanceTrackerDbContext.Accounts.FirstOrDefaultAsync(acc => acc.UserName.ToLower().Equals(username));
            if (account != null)
            {
                if (PasswordHasher.ComparePasswords(request.Login.Password, account.Salt, account.Password)) {
                    var id = 0;
                    var firstName = "";
                    var lastName = "";

                    if (account.Role.Equals(Roles.Student.ToString()))
                    {
                        var student = await _attendanceTrackerDbContext.Students.FirstOrDefaultAsync(stu => stu.EmailAddress.ToLower().Equals(username));
                        if (student != null)
                        {
                            id = student.Id;
                            firstName = student.FirstName;
                            lastName = student.LastName;
                        }
                    }
                    else if (account.Role.Equals(Roles.Teacher.ToString()))
                    {
                        var teacher = await _attendanceTrackerDbContext.Teachers.FirstOrDefaultAsync(tea => tea.EmailAddress.ToLower().Equals(username));
                        if (teacher != null)
                        {
                            id = teacher.Id;
                            firstName = teacher.FirstName;
                            lastName = teacher.LastName;
                        }
                    }

                    return new UserAccountDTO
                    {
                        Role = account.Role,
                        UserName = account.UserName,
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName
                    };
                }
            }
        }

        return null;
    }
}

