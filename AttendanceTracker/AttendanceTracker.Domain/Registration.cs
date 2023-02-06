using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Domain
{
    public class Registration
        : EntityBase<int>
    {

        public Registration()
        {
            Attendances = new HashSet<Attendance>();
        }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

    }
}

