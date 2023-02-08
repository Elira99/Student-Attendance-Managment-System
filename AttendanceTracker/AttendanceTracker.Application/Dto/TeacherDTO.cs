using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracker.Application.Dto
{
    public class TeacherDTO
    {
        public TeacherDTO()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}

