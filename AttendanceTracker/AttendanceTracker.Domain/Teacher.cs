using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Domain
{
    [Index(nameof(EmailAddress), Name = "Index_Teacher_EmailAddress_Unique", IsUnique = true)]
    public class Teacher
        : EntityBase<int>
    {

        public Teacher()
        {
            Courses = new HashSet<Course>();
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


        public ICollection<Course> Courses { get; set; }

    }
}