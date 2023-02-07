using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.API.Controllers
{
    public class BaseAPIController
            : ControllerBase
    {

        private readonly IMediator _mediator;

        public BaseAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IMediator Mediator => _mediator;
    }
}

