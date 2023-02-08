using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class UpdateCourseCommand
    : IRequest<CourseDTO>
{
    private readonly CourseDTO _course;
    private readonly UserAccountDTO _userAccount;

    public UpdateCourseCommand(CourseDTO course, UserAccountDTO userAccount)
    {
        _course = course;
        _userAccount = userAccount;
    }

    public CourseDTO Course => _course;

    public UserAccountDTO UserAccount => _userAccount;

}

