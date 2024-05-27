using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.EnrollmentDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(
        IStudentRepository studentRepository,
        IMapper mapper,
        ILogger<EnrollmentService> logger,
        IEnrollmentRepository enrollmentRepository)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _logger = logger;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<EnrollmentDto> AddAsync(AddEnrollmentDto entity)
    {
        var validateUser = await ValidateStudentByUserId(entity.StudentUserId);

        entity.StudentId = validateUser.Id;

        var enrollment = _mapper.Map<Enrollment>(entity);
        var addedEnrollment = await _enrollmentRepository.AddAsync(enrollment);

        _logger.LogInformation($"Enrollment was added: {addedEnrollment.Id}");

        return _mapper.Map<EnrollmentDto>(addedEnrollment);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var validateUser = await ValidateStudentByUserId(userId);

        var validate = await ValidateEnrollments(id, validateUser.Id);
        if (!validate)
            throw new BadRequestException("You can't delete this enrollment");

        var deleteResult = await _enrollmentRepository.DeleteAsync(id);

        if (deleteResult)
            _logger.LogInformation($"Enrollment was deleted: {id}");
        else
            _logger.LogError($"Enrollment wasn't deleted: {id}");

        return deleteResult;
    }

    public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
    }

    public async Task<EnrollmentDto> GetByIdAsync(int id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);

        if (enrollment == null)
            throw new NotFoundException("Enrollment not found");

        return _mapper.Map<EnrollmentDto>(enrollment);
    }

    public async Task<EnrollmentDto> UpdateAsync(int id, UpdateEnrollmentDto entity)
    {
        var validateUser = await ValidateStudentByUserId(entity.StudentUserId);

        var validate = await ValidateEnrollments(id, validateUser.Id);

        if (!validate)
            throw new BadRequestException("You can't delete this enrollment");

        var updatedEnrollment = _mapper.Map<Enrollment>(entity);

        var result = await _enrollmentRepository.UpdateAsync(id, updatedEnrollment);

        _logger.LogInformation($"Enrollment was updated: {id}");

        return _mapper.Map<EnrollmentDto>(result);
    }

    private async Task<Student> ValidateStudentByUserId(string userId)
    {
        var student = await _studentRepository.GetByUserIdAsync(userId);

        if (student != null)
            throw new BadRequestException("Student profile already exists");

        return student;
    }

    private async Task<bool> ValidateEnrollments(int enrollmentId, int studentId)
    {
        var enrollments = await _enrollmentRepository.GetByStudentId(studentId);

        if (!enrollments.Any(enrollment => enrollment.Id == enrollmentId))
            return false;

        return true;
    }
}
