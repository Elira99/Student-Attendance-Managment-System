using System.Reflection;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AttendanceTrackerDbContext>(options =>
{
    options.UseSqlite(@"Data source=attendancetracker.db");
    
});

var applicationAssembly = typeof(CourseDTO).Assembly;

builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddMediatR(applicationAssembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var dbContext = builder.Services.BuildServiceProvider().GetService<AttendanceTrackerDbContext>())
{
    if (dbContext is not null)
    {
        await dbContext.Database.EnsureDeletedAsync();
        dbContext.Database.Migrate();
        await Seed.SeedData(dbContext);
    }
}

app.Run();

