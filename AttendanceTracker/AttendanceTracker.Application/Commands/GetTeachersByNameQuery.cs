using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetTeachersByNameQuery
    : IRequest<IEnumerable<TeacherDTO>>
{
    private readonly string _partialName;

    public GetTeachersByNameQuery(string partialName)
    {
        _partialName = partialName;
    }

    public string PartialName => _partialName;
}