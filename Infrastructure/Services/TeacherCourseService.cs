using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class TeacherCourseService : ITeacherCourseService
{
    #region Injection
    private readonly ITeacherCourseRepository _teacherCourseRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherCourseService> _logger;

    public TeacherCourseService(
        ITeacherCourseRepository teacherCourseRepository,
        IMapper mapper,
        ILogger<TeacherCourseService> logger,
        ITeacherRepository teacherRepository)
    {
        _teacherCourseRepository = teacherCourseRepository;
        _mapper = mapper;
        _logger = logger;
        _teacherRepository = teacherRepository;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<TeacherCourseDto>> GetAllAsync()
    {
        var teacherCourses = await _teacherCourseRepository.GetAllAsync("Teacher,Course,Course.Faculty");
        return _mapper.Map<IEnumerable<TeacherCourseDto>>(teacherCourses);
    }

    public async Task<IEnumerable<TeacherCourseDto>> GetByUserIdAsync(string teacherUserId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(teacherUserId);

        if (teacher == null)
            throw new NotFoundException($"Teacher not found.");

        var teacherCourses = await _teacherCourseRepository.GetByTeacherId(teacher.Id);
        return _mapper.Map<IEnumerable<TeacherCourseDto>>(teacherCourses);
    }
    public async Task<TeacherCourseDto> GetByIdAsync(int id)
    {
        var teacherCourse = await _teacherCourseRepository.GetByIdAsync(tc => tc.Id == id);
        if (teacherCourse == null)
            throw new NotFoundException($"TeacherCourse with ID {id} not found.");

        return _mapper.Map<TeacherCourseDto>(teacherCourse);
    }
    #endregion

    #region Write
    public async Task AddAsync(AddTeacherCourseDto addTeacherCourseDto)
    {
        var teacher = await ValidateTeacherAsync(addTeacherCourseDto.UserId, addTeacherCourseDto.TeacherId);

        var teacherCourse = _mapper.Map<TeacherCourse>(addTeacherCourseDto);
        await _teacherCourseRepository.AddAsync(teacherCourse);

        _logger.LogInformation("Course added to teacher with ID {TeacherId}", addTeacherCourseDto.TeacherId);
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var teacher = await ValidateTeacherAsync(userId, null);
        var teacherCourse = await _teacherCourseRepository.GetByIdAsync(tc => tc.Id == id);

        if (teacherCourse == null || teacherCourse.TeacherId != teacher.Id)
            throw new BadRequestException("Course not found or does not belong to the specified teacher.");

        await _teacherCourseRepository.DeleteAsync(teacherCourse);

        _logger.LogInformation("Course with ID {CourseId} deleted for teacher with ID {TeacherId}", id, teacher.Id);
    }
    #endregion

    #region Other
    private async Task<Teacher> ValidateTeacherAsync(string userId, int? teacherId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(userId);

        if (teacher == null)
            throw new BadRequestException("Teacher profile could not be found.");

        if (teacherId.HasValue && teacher.Id != teacherId.Value)
            throw new BadRequestException("You cannot perform this action for a different teacher.");

        return teacher;
    }
    #endregion
}
