using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracker.Application.Dto
{
    public class StudentAccountDTO
        : StudentDTO
    {
        public StudentAccountDTO()
        {
        }

        public string Password { get; set; }
    }
}

