﻿using Application.Models.Dtos;
using Application.Models.Dtos.CourseDtos;
using Application.Models.Dtos.EnrollmentDtos;
using Application.Models.Dtos.FacultyDtos;
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

        // Course Map
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<AddCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

        // Faculty Map
        CreateMap<Faculty, FacultyDto>().ReverseMap();
        CreateMap<AddFacultyDto, Faculty>();
        CreateMap<UpdateFacultyDto, Faculty>();

        // Enrollment Map
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<AddEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
    }
}