using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.TeacherDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class TeacherService : ITeacherService
{
    #region Injection
    private readonly ITeacherRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherService> _logger;

    public TeacherService(
        ITeacherRepository teacherRepository,
        IMapper mapper,
        ILogger<TeacherService> logger)
    {
        _repository = teacherRepository;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
    {
        var teachers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
    }

    public async Task<TeacherDto> GetTeacherByUserIdAsync(string teacherUserId)
    {
        var teacher = await GetAndValidateTeacherAsync(teacherUserId);

        return _mapper.Map<TeacherDto>(teacher);
    }
    #endregion

    #region Write
    public async Task CreateTeacherProfileAsync(AddTeacherDto addTeacherDto)
    {
        var teacher = await _repository.GetByUserIdAsync(addTeacherDto.UserId);

        _mapper.Map(addTeacherDto, teacher);

        await _repository.AddAsync(teacher);

        _logger.LogInformation($"Teacher was added: {addTeacherDto.Email}");
    }

    public async Task UpdateTeacherProfileAsync(UpdateTeacherDto updateTeacherDto)
    {
        var teacher = await _repository.GetByUserIdAsync(updateTeacherDto.UserId);

        _mapper.Map(updateTeacherDto, teacher);

        await _repository.UpdateAsync(teacher);

        _logger.LogInformation($"Teacher was updated: {updateTeacherDto.Id}");
    }

    public async Task DeleteTeacherProfileAsync(string teacherUserId)
    {
        var teacher = await GetAndValidateTeacherAsync(teacherUserId);

        await _repository.DeleteAsync(teacher);

        _logger.LogInformation($"Teacher was deleted: {teacherUserId}");
    }
    #endregion

    #region Other
    private async Task<Teacher> GetAndValidateTeacherAsync(string teacherUserId)
    {
        var teacher = await _repository.GetByUserIdAsync(teacherUserId);

        if (teacher == null)
            throw new NotFoundException($"Teacher profile with userId:{teacherUserId} not found");

        return teacher;
    }
    #endregion
}
