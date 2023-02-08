using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class ConfirmAttendanceCommand
    : IRequest<CourseDTO>
{
    private readonly int _courseId;
    private readonly UserAccountDTO _userAccount;

    public ConfirmAttendanceCommand(int courseId, UserAccountDTO userAccount)
    {
        _courseId = courseId;
        _userAccount = userAccount;
    }

    public int CourseId => _courseId;

    public UserAccountDTO UserAccount => _userAccount;

}

