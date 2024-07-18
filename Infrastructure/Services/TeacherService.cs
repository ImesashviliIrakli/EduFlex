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
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherService> _logger;
    public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeacherService> logger)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<TeacherDto> AddAsync(AddTeacherDto entity)
    {
        var checkUser = await _teacherRepository.GetByUserIdAsync(entity.UserId);

        if (checkUser != null)
            throw new BadRequestException($"Teacher profile already exists");

        var teacher = _mapper.Map<Teacher>(entity);

        var add = await _teacherRepository.AddAsync(teacher);

        _logger.LogInformation($"Teacher was added: {add.Id}");

        var result = _mapper.Map<TeacherDto>(add);

        return result;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var checkUser = await _teacherRepository.GetByUserIdAsync(userId);

        if (checkUser.Id != id)
            throw new BadRequestException("You don't have permission to delete other teachers profile");

        var delete = await _teacherRepository.DeleteAsync(id);

        if (delete)
            _logger.LogInformation($"Teacher was deleted: {id}");
        else
            _logger.LogError($"Teacher wasn't deleted: {id}");

        return delete;
    }

    public async Task<IEnumerable<TeacherDto>> GetAllAsync()
    {
        var teachers = await _teacherRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<TeacherDto>>(teachers);

        return result;
    }

    public async Task<TeacherDto> GetByIdAsync(int id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(filter: (u) => u.Id == id);
        var result = _mapper.Map<TeacherDto>(teacher);

        return result;
    }

    public async Task<TeacherDto> GetByUserIdAsync(string userId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(userId);

        if (teacher == null)
            throw new NotFoundException($"Teacher profile with userId:{userId} not found");

        var result = _mapper.Map<TeacherDto>(teacher);

        return result;
    }

    public async Task<TeacherDto> UpdateAsync(int id, UpdateTeacherDto entity)
    {
        var checkUser = await _teacherRepository.GetByIdAsync(filter: (u) => u.Id == id);

        if (checkUser.UserId != entity.UserId)
            throw new BadRequestException("You don't have permission to update different profile");

        var teacher = _mapper.Map<Teacher>(entity);

        var update = await _teacherRepository.UpdateAsync(id, teacher);

        _logger.LogInformation($"Teacher was updated: {id}");

        var result = _mapper.Map<TeacherDto>(update);

        return result;
    }
}
