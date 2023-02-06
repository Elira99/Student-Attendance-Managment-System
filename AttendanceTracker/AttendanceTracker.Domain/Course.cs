using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Domain
{
    public class Course
        : EntityBase<int>
    {

        public Course()
        {
            Registrations = new HashSet<Registration>();
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [Required]
        public string WeekDays { get; set; } // 1,3,4

        public ICollection<Registration> Registrations { get; set; }

        [NotMapped]
        public IEnumerable<string> WeekDaysAsArray => (WeekDays ?? string.Empty).Split(',');
    }
}

