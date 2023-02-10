using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracker.Application.Dto
{
    public class TeacherAccountDTO
        : TeacherDTO
    {
        public TeacherAccountDTO()
        {
        }
        public string Password { get; set; }
    }
}

