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

    public async Task<TeacherCourseDto> AddAsync(AddTeacherCourseDto entity)
    {
        var teacher = await ValidateTeacherAsync(entity.UserId, entity.TeacherId);

        var teacherCourse = _mapper.Map<TeacherCourse>(entity);
        var addedCourse = await _teacherCourseRepository.AddAsync(teacherCourse);

        _logger.LogInformation($"Course added to teacher: {teacher.Id}, Course: {addedCourse.Id}");

        return _mapper.Map<TeacherCourseDto>(addedCourse);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var teacher = await ValidateTeacherAsync(userId, null);
        var teacherCourse = await _teacherCourseRepository.GetByIdAsync(filter: (u) => u.Id == id);

        if (teacherCourse == null || teacherCourse.TeacherId != teacher.Id)
            throw new BadRequestException($"Course not found or does not belong to the specified teacher.");

        var deleted = await _teacherCourseRepository.DeleteAsync(id);

        if (deleted)
            _logger.LogInformation($"Course deleted: {id}");
        else
            _logger.LogError($"Course deletion failed: {id}");

        return deleted;
    }

    public async Task<IEnumerable<TeacherCourseDto>> GetAllAsync()
    {
        var teacherCourses = await _teacherCourseRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TeacherCourseDto>>(teacherCourses);
    }

    public async Task<TeacherCourseDto> GetByIdAsync(int id)
    {
        var teacherCourse = await _teacherCourseRepository.GetByIdAsync(filter: (u) => u.Id == id);
        if (teacherCourse == null)
            throw new NotFoundException($"Course with ID {id} not found.");

        return _mapper.Map<TeacherCourseDto>(teacherCourse);
    }

    private async Task<Teacher> ValidateTeacherAsync(string userId, int? teacherId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(userId);
        if (teacher == null)
            throw new BadRequestException("Teacher profile could not be found.");

        if (teacherId.HasValue && teacher.Id != teacherId.Value)
            throw new BadRequestException("You can't perform this action for a different teacher.");

        return teacher;
    }
}