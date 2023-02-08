using System;
using AttendanceTracker.Application.Dto;
using MediatR;

namespace AttendanceTracker.Application.Commands;


public class GetUserAccountQuery
    : IRequest<UserAccountDTO>
{
    private readonly LoginDTO _loginDTO;

    public GetUserAccountQuery(LoginDTO loginDTO)
    {
        _loginDTO = loginDTO;
    }

    public LoginDTO Login => _loginDTO;
}

