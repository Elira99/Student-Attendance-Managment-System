using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class AddTeacherAccountCommand
    : IRequest<TeacherDTO>
{
    private readonly TeacherAccountDTO _teacher;

    public AddTeacherAccountCommand(TeacherAccountDTO teacher)
    {
        _teacher = teacher;
    }

    public TeacherAccountDTO TeacherAccount => _teacher;
}

