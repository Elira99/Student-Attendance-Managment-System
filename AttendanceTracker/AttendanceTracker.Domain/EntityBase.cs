using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracker.Domain
{
    public class EntityBase<T>
    {
        [Key]
        public T Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        [MaxLength(255)]
        public string? UserUpdated { get; set; }
    }
}

