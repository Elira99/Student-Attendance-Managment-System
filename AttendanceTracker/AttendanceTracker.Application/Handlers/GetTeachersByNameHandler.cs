using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class GetTeachersByNameHandler
    : IRequestHandler<GetTeachersByNameQuery, IEnumerable<TeacherDTO>>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetTeachersByNameHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public Task<IEnumerable<TeacherDTO>> Handle(GetTeachersByNameQuery request, CancellationToken cancellationToken)
    {

        var partialName = (request.PartialName ?? string.Empty).Trim().ToLower();
        if (!string.IsNullOrWhiteSpace(partialName))
        {
            var teachers = _attendanceTrackerDbContext.Teachers.Where(student => student.FirstName.ToLower().StartsWith(partialName)
                        || student.LastName.ToLower().StartsWith(partialName)).ToList();

            return Task.FromResult(_mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers));
        }

        return Task.FromResult(Enumerable.Empty<TeacherDTO>());
    }
}

