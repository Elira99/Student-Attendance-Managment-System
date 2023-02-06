using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AttendanceTracker.Domain
{
    [Index(nameof(EmailAddress), Name = "Index_Student_EmailAddress_Unique", IsUnique = true)]
    public class Student
		: EntityBase<int>
	{

        public Student()
        {
            Registrations = new HashSet<Registration>();
        }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
		public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string EmailAddress { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}

