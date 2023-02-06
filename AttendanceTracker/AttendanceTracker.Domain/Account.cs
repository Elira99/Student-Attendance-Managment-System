using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Domain
{
    [Index(nameof(UserName), Name = "Index_UserName_Unique", IsUnique = true)]
    public class Account
        : EntityBase<int>
    {
        [MaxLength(25)]
        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(25)]
        public string Salt { get; set; }

        [Required]
        [MaxLength(25)]
        public string Role { get; set; }

        [Required]
        public bool Active { get; set; }

    }
}

