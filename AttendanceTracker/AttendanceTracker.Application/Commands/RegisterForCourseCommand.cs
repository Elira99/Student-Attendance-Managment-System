using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class RegisterForCourseCommand
    : IRequest<RegistrationDTO>
{
    private readonly RegistrationDTO _registration;

    public RegisterForCourseCommand(RegistrationDTO registration)
    {
        _registration = registration;
    }

    public RegistrationDTO Registration => _registration;
}

