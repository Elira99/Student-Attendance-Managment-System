using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracker.Application.Dto
{
    public class StudentDTO
    {
        public StudentDTO()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}

