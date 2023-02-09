using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.API.Controllers
{
    [ApiController]
    [Route("/api/students")]
    public class StudentsController
            : BaseAPIController
    {
        public StudentsController(IMediator mediator)
            : base(mediator) 
        {

        }



      //  [Authorize]
        [HttpGet("find/{partialName}")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsByName(string partialName)
        {
            var students = await Mediator.Send(new GetStudentsByNameQuery(partialName));
            return Ok(students);
        }

        [Authorize]
        [HttpGet("{studentId}")]
        public async Task<ActionResult<StudentDTO>> GetStudentById([FromRoute] int studentId)
        {
            var student = await Mediator.Send(new GetStudentByIdQuery(studentId));
            return student != null ? Ok(student) : NotFound($"Student Id {studentId} not found.");
        }


    }
}

