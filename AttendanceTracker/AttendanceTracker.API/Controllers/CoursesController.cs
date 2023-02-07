using System;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.API.Controllers
{
    [ApiController]
    [Route("/api/courses")]
    public class CoursesController
            : BaseAPIController
    {
        public CoursesController(IMediator mediator)
            : base(mediator) 
        {

        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAllCourses()
        {
            var courses = await Mediator.Send(new GetAllCoursesQuery());
            return courses != null ? Ok(courses) : NotFound();
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDTO>> GetCourseById([FromRoute] int courseId)
        {
            var course = await Mediator.Send(new GetCourseByIdQuery(courseId));
            return course != null ? Ok(course) : NotFound($"Course Id {courseId} not found.");
        }

        [HttpPost("add")]
        public async Task<ActionResult<CourseDTO>> Add([FromBody] CourseDTO course)
        {
            var newCourse = await Mediator.Send(new AddCourseCommand(course));
            return newCourse;
        }

        [HttpPut("update")]
        public async Task<ActionResult<CourseDTO>> Update([FromBody] CourseDTO course)
        {
            var updatedCourse = await Mediator.Send(new UpdateCourseCommand(course));
            return updatedCourse != null ? Ok(updatedCourse) : NotFound("Course not found.");
        }

        [HttpPost("registerstudent")]
        public async Task<ActionResult<RegistrationDTO>> RegisterStudent([FromBody] RegistrationDTO registration)
        {
            var reg = await Mediator.Send(new RegisterForCourseCommand(registration));
            return reg != null ? Ok(reg) : NotFound($"Course or student no found.");
        }

        [HttpGet("registeredstudents/{courseId}")]
        public async Task<ActionResult<CourseDTO>> RegisteredStudents([FromRoute] int courseId)
        {
            var course = await Mediator.Send(new GetRegisteredStudentsForCourseQuery(courseId));
            return course != null ? Ok(course) : NotFound($"Course Id {courseId} not found.");
        }
    }
}

