using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AttendanceTracker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.API.Controllers
{
    public class BaseAPIController
            : ControllerBase
    {

        private readonly IMediator _mediator;

        private UserAccountDTO? _userAccount;

        public BaseAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IMediator Mediator => _mediator;

        protected UserAccountDTO SignedInUser
        {
            get
            {
                if (_userAccount == null)
                {
                    _userAccount = new UserAccountDTO();
                    var identity = HttpContext.User?.Identity as ClaimsIdentity;
                    if (identity != null && identity.IsAuthenticated)
                    {
                        _userAccount.LastName = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                        _userAccount.FirstName = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                        _userAccount.UserName = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                        _userAccount.Role = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                        var idAsString = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                        if (int.TryParse(idAsString, out int id))
                        {
                            _userAccount.Id = id;
                        }
                    }
                }

                return _userAccount;
            }
        }
    }
}

