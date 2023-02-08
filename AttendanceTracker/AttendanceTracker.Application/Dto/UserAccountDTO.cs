using System;
namespace AttendanceTracker.Application.Dto
{
    public class UserAccountDTO
    {
        public string UserName { get; set; }

        public string Role { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Fullname => $"{LastName}, {FirstName}";

    }
}

