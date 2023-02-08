using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class AddStudentAccountCommand
    : IRequest<StudentDTO>
{
    private readonly StudentAccountDTO _student;

    public AddStudentAccountCommand(StudentAccountDTO student)
    {
        _student = student;
    }

    public StudentAccountDTO StudentAccount => _student;
}

