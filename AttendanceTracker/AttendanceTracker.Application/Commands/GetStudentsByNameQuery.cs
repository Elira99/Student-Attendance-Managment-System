using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetStudentsByNameQuery
    : IRequest<IEnumerable<StudentDTO>>
{
    private readonly string _partialName;

    public GetStudentsByNameQuery(string partialName)
    {
        _partialName = partialName;
    }

    public string PartialName => _partialName;
}

