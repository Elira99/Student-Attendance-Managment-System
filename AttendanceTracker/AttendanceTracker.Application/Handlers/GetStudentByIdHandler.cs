using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AttendanceTracker.Persistence;
using AutoMapper;
using MediatR;

namespace AttendanceTracker.Application.Handlers;

public class GetStudentByIdHandler
    : IRequestHandler<GetStudentByIdQuery, StudentDTO?>
{

    private readonly AttendanceTrackerDbContext _attendanceTrackerDbContext;
    private readonly IMapper _mapper;

    public GetStudentByIdHandler(AttendanceTrackerDbContext attendanceTrackerDbContext, IMapper mapper)
    {
        _attendanceTrackerDbContext = attendanceTrackerDbContext;
        _mapper = mapper;
    }

    public async Task<StudentDTO?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _attendanceTrackerDbContext.Students.FindAsync(new object?[] { request.StudentId }, cancellationToken: cancellationToken);
        return student != null ? _mapper.Map<Student, StudentDTO>(student) : null;
    }
}

