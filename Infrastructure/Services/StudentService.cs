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
    #region Injection
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;

    public StudentService(
        IStudentRepository repository,
        IMapper mapper,
        ILogger<StudentService> logger
        )
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(filter: (u) => u.Id == id);
        if (student == null)
            throw new NotFoundException($"Student with ID {id} not found.");

        return _mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto> GetByUserIdAsync(string userId)
    {
        var student = await _repository.GetByUserIdAsync(userId);

        if (student == null)
            throw new NotFoundException($"Student profile with userId {userId} not found.");

        return _mapper.Map<StudentDto>(student);
    }
    #endregion

    #region Write
    public async Task AddAsync(AddStudentDto addStudentDto)
    {
        var existingStudent = await _repository.GetByUserIdAsync(addStudentDto.UserId);

        if (existingStudent != null)
            throw new BadRequestException("Student profile already exists.");

        var student = _mapper.Map<Student>(addStudentDto);

        await _repository.AddAsync(student);

        _logger.LogInformation($"Student added: {addStudentDto.Email}");
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var student = await _repository.GetByUserIdAsync(userId);

        if (student == null)
            throw new NotFoundException("Couldn't find associated student profile for user.");

        if (student.Id != id)
            throw new BadRequestException("You don't have permission to perform this operation.");

        await _repository.DeleteAsync(student);

        _logger.LogInformation($"Student deleted: {id}");
    }

    public async Task UpdateAsync(UpdateStudentDto updateStudentDto)
    {
        var student = await _repository.GetByIdAsync(filter: (u) => u.Id == updateStudentDto.Id);

        if (student == null)
            throw new NotFoundException($"Student with ID {updateStudentDto.Id} not found.");

        if (student.UserId != updateStudentDto.UserId)
            throw new BadRequestException("You don't have permission to update this profile.");

        _mapper.Map(updateStudentDto, student);

        await _repository.UpdateAsync(student);

        _logger.LogInformation($"Student updated: {updateStudentDto.Id}");
    }
    #endregion
}
