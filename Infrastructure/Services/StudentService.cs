using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.StudentDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IStudentRepository repository, IMapper mapper, ILogger<StudentService> logger)
    {
        _studentRepository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<StudentDto> AddAsync(AddStudentDto entity)
    {
        var checkUser = await _studentRepository.GetByUserIdAsync(entity.UserId);

        if (checkUser != null)
            throw new BadRequestException($"Student profile already exists");

        var Student = _mapper.Map<Student>(entity);

        var add = await _studentRepository.AddAsync(Student);

        _logger.LogInformation($"Student was added: {add.Id}");

        var result = _mapper.Map<StudentDto>(add);

        return result;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var checkUser = await _studentRepository.GetByUserIdAsync(userId);

        if (checkUser.Id != id)
            throw new BadRequestException("You don't have permission to delete other Students profile");

        var delete = await _studentRepository.DeleteAsync(id);

        if (delete)
            _logger.LogInformation($"Student was deleted: {id}");
        else
            _logger.LogError($"Student wasn't deleted: {id}");

        return delete;
    }

    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var Students = await _studentRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<StudentDto>>(Students);

        return result;
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var Student = await _studentRepository.GetByIdAsync(filter: (u) => u.Id == id);
        var result = _mapper.Map<StudentDto>(Student);

        return result;
    }

    public async Task<StudentDto> GetByUserIdAsync(string userId)
    {
        var student = await _studentRepository.GetByUserIdAsync(userId);

        if (student == null)
            throw new NotFoundException($"Student profile with userId:{userId} not found");

        var result = _mapper.Map<StudentDto>(student);

        return result;
    }

    public async Task<StudentDto> UpdateAsync(int id, UpdateStudentDto entity)
    {
        var checkUser = await _studentRepository.GetByIdAsync(filter: (u) => u.Id == id);

        if (checkUser.UserId != entity.UserId)
            throw new BadRequestException("You don't have permission to update different profile");

        var Student = _mapper.Map<Student>(entity);

        var update = await _studentRepository.UpdateAsync(id, Student);

        _logger.LogInformation($"Student was updated: {id}");

        var result = _mapper.Map<StudentDto>(update);

        return result;
    }
}
