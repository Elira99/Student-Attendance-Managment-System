using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetStudentByIdQuery
    : IRequest<StudentDTO>
{
    private readonly int _studentId;

    public GetStudentByIdQuery(int studentId)
    {
        _studentId = studentId;
    }

    public int StudentId => _studentId;
}

