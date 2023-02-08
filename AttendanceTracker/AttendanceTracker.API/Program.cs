using System.Reflection;
using AttendanceTracker.Application.Dto;
using AttendanceTracker.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AttendanceTrackerDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("attendancetrackerDb"));
    
});

var applicationAssembly = typeof(CourseDTO).Assembly;

builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddMediatR(applicationAssembly);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

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

