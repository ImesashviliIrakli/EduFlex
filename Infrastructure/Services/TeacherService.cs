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
    public async Task<IEnumerable<TeacherDto>> GetAllAsync()
    {
        var teachers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
    }

    public async Task<TeacherDto> GetByIdAsync(int id)
    {
        var teacher = await _repository.GetByIdAsync(t => t.Id == id);
        if (teacher == null)
            throw new NotFoundException($"Teacher with ID {id} not found.");

        return _mapper.Map<TeacherDto>(teacher);
    }

    public async Task<TeacherDto> GetByUserIdAsync(string userId)
    {
        var teacher = await _repository.GetByUserIdAsync(userId);
        if (teacher == null)
            throw new NotFoundException($"Teacher profile with userId:{userId} not found");

        return _mapper.Map<TeacherDto>(teacher);
    }
    #endregion

    #region Write
    public async Task AddAsync(AddTeacherDto addTeacherDto)
    {
        var existingTeacher = await _repository.GetByUserIdAsync(addTeacherDto.UserId);
        if (existingTeacher != null)
            throw new BadRequestException("Teacher profile already exists");

        var teacher = _mapper.Map<Teacher>(addTeacherDto);
        await _repository.AddAsync(teacher);

        _logger.LogInformation($"Teacher was added: {addTeacherDto.Email}");
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var teacher = await _repository.GetByUserIdAsync(userId);
        if (teacher == null)
            throw new NotFoundException($"Teacher profile with userId:{userId} not found");

        if (teacher.Id != id)
            throw new BadRequestException("You don't have permission to delete another teacher's profile");

        await _repository.DeleteAsync(teacher);
        _logger.LogInformation($"Teacher was deleted: {id}");
    }

    public async Task UpdateAsync(UpdateTeacherDto updateTeacherDto)
    {
        var existingTeacher = await _repository.GetByIdAsync(t => t.Id == updateTeacherDto.Id);
        if (existingTeacher == null)
            throw new NotFoundException($"Teacher with ID {updateTeacherDto.Id} not found.");

        if (existingTeacher.UserId != updateTeacherDto.UserId)
            throw new BadRequestException("You don't have permission to update another teacher's profile");

        var teacher = _mapper.Map<Teacher>(updateTeacherDto);
        await _repository.UpdateAsync(teacher);

        _logger.LogInformation($"Teacher was updated: {updateTeacherDto.Id}");
    }
    #endregion
}
