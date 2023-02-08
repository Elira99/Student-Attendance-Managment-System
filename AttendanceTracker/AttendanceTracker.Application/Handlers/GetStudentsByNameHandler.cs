using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class GetStudentsByNameHandler
    : IRequestHandler<GetStudentsByNameQuery, IEnumerable<StudentDTO>>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetStudentsByNameHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public Task<IEnumerable<StudentDTO>> Handle(GetStudentsByNameQuery request, CancellationToken cancellationToken)
    {

        var partialName = (request.PartialName ?? string.Empty).Trim().ToLower();
        if (!string.IsNullOrWhiteSpace(partialName))
        {
            var students = _attendanceTrackerDbContext.Students.Where(student => student.FirstName.ToLower().StartsWith(partialName)
                        || student.LastName.ToLower().StartsWith(partialName)).ToList();

            return Task.FromResult(_mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(students));
        }

        return Task.FromResult(Enumerable.Empty<StudentDTO>());
    }
}

