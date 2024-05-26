using Application.Models.Dtos;
using Application.Models.Dtos.StudentDtos;
using Application.Models.Dtos.TeacherDtos;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Teacher Map
        CreateMap<Teacher, TeacherDto>().ReverseMap();
        CreateMap<AddTeacherDto, Teacher>();
        CreateMap<UpdateTeacherDto, Teacher>();

        // Student Map
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<AddStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();

        // TeacherCourse Map
        CreateMap<TeacherCourse, TeacherCourseDto>().ReverseMap();
        CreateMap<AddTeacherCourseDto, TeacherCourse>();

        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Faculty, FacultyDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
    }
}
