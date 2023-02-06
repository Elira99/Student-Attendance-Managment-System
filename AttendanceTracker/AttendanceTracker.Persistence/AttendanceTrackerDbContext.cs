using AttendanceTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Persistence;


public class AttendanceTrackerDbContext
	: DbContext
{


	public AttendanceTrackerDbContext(DbContextOptions<AttendanceTrackerDbContext> options)
		: base(options)
	{
		
	}

	public DbSet<Teacher> Teachers { get; set; }

	public DbSet<Student> Students { get; set; }

	public DbSet<Account> Accounts { get; set; }

	public DbSet<Course> Courses { get; set; }

	public DbSet<Registration> Registrations { get; set; }

	public DbSet<Attendance> Attendances { get; set; }

}

