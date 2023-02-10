using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.API.Controllers
{
    [ApiController]
    [Route("/api/teachers")]
    public class TeachersController
            : BaseAPIController
    {
        public TeachersController(IMediator mediator)
            : base(mediator) 
        {

        }

      //  [Authorize]
        [HttpGet("find/{partialName}")]
        public async Task<ActionResult<IEnumerable<TeacherDTO>>> GetTeacherssByName(string partialName)
        {
            var students = await Mediator.Send(new GetStudentsByNameQuery(partialName));
            return Ok(students);
        }

        [Authorize]
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<TeacherDTO>> GetTeacherById([FromRoute] int teacherId)
        {
            var student = await Mediator.Send(new GetStudentByIdQuery(teacherId));
            return student != null ? Ok(student) : NotFound($"Teacher Id {teacherId} not found.");
        }
    }
}

