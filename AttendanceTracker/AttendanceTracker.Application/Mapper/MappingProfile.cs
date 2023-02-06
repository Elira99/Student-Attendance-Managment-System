using AttendanceTracker.Application.Dto;
using AttendanceTracker.Domain;
using AutoMapper;

namespace AttendanceTracker.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();

            CreateMap<Registration, RegistrationDTO>();

            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();

        }
    }
}

