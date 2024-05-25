using Application.Models.Dtos;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Teacher, TeacherDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<TeacherCourse, TeacherCourseDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Faculty, FacultyDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
    }
}
