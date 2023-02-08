using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class RegisterForCourseCommand
    : IRequest<RegistrationDTO>
{
    private readonly RegistrationDTO _registration;
    private readonly UserAccountDTO _userAccount;

    public RegisterForCourseCommand(RegistrationDTO registration, UserAccountDTO userAccount)
    {
        _registration = registration;
        _userAccount = userAccount;
    }

    public RegistrationDTO Registration => _registration;

    public UserAccountDTO UserAccount => _userAccount;

}

