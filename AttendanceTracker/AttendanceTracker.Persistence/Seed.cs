using System;
using AttendanceTracker.Domain;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AttendanceTracker.Utils;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Persistence
{
    public static class Seed
    {
        private static DateTime utcNow = DateTime.UtcNow;
        private static string userCreated = "appleseed@appleorchard.org";

        public static async Task SeedData(AttendanceTrackerDbContext dbContext)
        {
            var teacher1Id = await AddTeacher(dbContext, "John", "Hanks");
            var teacher2Id = await AddTeacher(dbContext, "Julie", "Johansson");

            var cSharId = await AddClasses(dbContext, teacher1Id, "C# Fundamentals", DateTime.UtcNow.AddMonths(1), 6);
            var javaId = await AddClasses(dbContext, teacher2Id, "Java Spring Boot Fundamentals", DateTime.UtcNow.AddMonths(2), 3);

            var student1Id = await AddStudent(dbContext, "Emma", "Theron");
            var student2Id = await AddStudent(dbContext, "Scarlett", "Page");
            var student3Id = await AddStudent(dbContext, "Mila", "Lawrence");
            var student4Id = await AddStudent(dbContext, "Arnold", "Willis");
            var student5Id = await AddStudent(dbContext, "Tom", "Schwarzenegger");
            var student6Id = await AddStudent(dbContext, "Clint", "Jackman");

            await AddRegistrations(dbContext, cSharId, student1Id);
            await AddRegistrations(dbContext, cSharId, student2Id);
            await AddRegistrations(dbContext, cSharId, student3Id);
            await AddRegistrations(dbContext, javaId, student4Id);
            await AddRegistrations(dbContext, javaId, student5Id);
            await AddRegistrations(dbContext, javaId, student6Id);
        }

        private static async Task<int> AddTeacher(AttendanceTrackerDbContext dbContext, string firstName, string lastName)
        {
            var teacherEmailAddress = $"{firstName.ToLower()}.{lastName.ToLower()}@ourcollege.net";
            var password = "BestTeacher!";

            var hashedPassword = PasswordHasher.HashPassword(password);

            var teacher = new Teacher()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = teacherEmailAddress,
                DateCreated = utcNow,
                UserCreated = userCreated
            };

            var teachersAccount = new Account()
            {
                Active = true,
                UserName = teacherEmailAddress,
                Password = hashedPassword.Password,
                Salt = hashedPassword.Salt,
                Role = Roles.Teacher.ToString(),
                UserCreated = userCreated,
                DateCreated = utcNow,
            };

            dbContext.Teachers.Add(teacher);
            dbContext.Accounts.Add(teachersAccount);

            await dbContext.SaveChangesAsync();

            return teacher.Id;

            // var isMatch = PasswordHasher.ComparePasswords(password, hashedPassword.Salt, hashedPassword.Password);
        }

        private static async Task<int> AddClasses(AttendanceTrackerDbContext dbContext, int teacherId, string name, DateTime startDate, int durationInMonths)
        {

            var teacher = await dbContext.Teachers.FindAsync(teacherId);

            var course = new Course()
            {
                DateCreated = utcNow,
                EndDate = startDate.AddMonths(durationInMonths),
                Name = name,
                StartDate = startDate,
                UserCreated = userCreated,
                WeekDays = "1,3,4"
            };

            teacher?.Courses.Add(course);

            await dbContext.SaveChangesAsync();

            return course.Id;
        }

        private static async Task<int> AddStudent(AttendanceTrackerDbContext dbContext, string firstName, string lastName)
        {
            var studentEmailAddress = $"{firstName.ToLower()}.{lastName.ToLower()}@ourcollege.net";
            var password = "AceNow!";

            var hashedPassword = PasswordHasher.HashPassword(password);

            var teacher = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = studentEmailAddress,
                DateCreated = utcNow,
                UserCreated = userCreated
            };

            var studentAccount = new Account()
            {
                Active = true,
                UserName = studentEmailAddress,
                Password = hashedPassword.Password,
                Salt = hashedPassword.Salt,
                Role = Roles.Student.ToString(),
                UserCreated = userCreated,
                DateCreated = utcNow,
            };


            dbContext.Students.Add(teacher);
            dbContext.Accounts.Add(studentAccount);

            await dbContext.SaveChangesAsync();

            return teacher.Id;

           
        }

        
        private static async Task AddRegistrations(AttendanceTrackerDbContext dbContext, int classId, int studentId)
        {
            var course = await dbContext.Courses.FindAsync(classId);

            var registration = new Registration()
            {
                StudentId = studentId,
                RegistrationDate = utcNow.AddDays(1),
                UserCreated = userCreated,
                DateCreated = utcNow
            };


            course?.Registrations.Add(registration);

            dbContext.Courses.Update(course);

            await dbContext.SaveChangesAsync();

            
        }

    }
}

