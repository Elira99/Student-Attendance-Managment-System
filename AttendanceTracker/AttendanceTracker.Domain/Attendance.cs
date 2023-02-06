using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Domain
{
    public class Attendance
        : EntityBase<int>
    {

        [Required]
        public int RegistrationId { get; set; }

        [ForeignKey("RegistrationId")]
        public Registration Registration { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        public int? ApprovedByTeacherId { get; set; }

        [ForeignKey("ApprovedByTeacherId")]
        public Teacher ApprovedByTeacher { get; set; }

        public DateTime ApprovedOn { get; set; }
    }
}

