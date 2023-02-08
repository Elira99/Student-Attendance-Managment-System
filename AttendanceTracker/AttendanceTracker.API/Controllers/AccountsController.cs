using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AttendanceTracker.Application.Commands;
using AttendanceTracker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceTracker.API.Controllers
{

    [ApiController]
    [Route("/api/accounts")]
    public class AccountsController
            : BaseAPIController
    {

        private readonly IConfiguration _config;

        public AccountsController(IMediator mediator, IConfiguration config)
            : base(mediator) 
        {
            _config = config;
        }

        

        [AllowAnonymous]
        [HttpPost("createstudentaccount")]
        public async Task<ActionResult<StudentDTO>> Add([FromBody] StudentAccountDTO studentAccount)
        {
            var student = await Mediator.Send(new AddStudentAccountCommand(studentAccount));
            return student;
        }

        [AllowAnonymous]
        [HttpPost("createteacheraccount")]
        public async Task<ActionResult<TeacherDTO>> Add([FromBody] TeacherAccountDTO teacherAccount)
        {
            var teacher = await Mediator.Send(new AddTeacherAccountCommand(teacherAccount));
            return teacher;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO login)
        {
            var userAccount = await Mediator.Send(new GetUserAccountQuery(login));

            if (userAccount != null)
            {
                return GenerateJSONWebToken(userAccount);
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateJSONWebToken(UserAccountDTO userAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Email, userAccount.UserName),
                new Claim(ClaimTypes.GivenName, userAccount.FirstName),
                new Claim(ClaimTypes.Surname, userAccount.LastName),
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                new Claim(ClaimTypes.Role, userAccount.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

