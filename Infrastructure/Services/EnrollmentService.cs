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
    #region Injection
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
    #endregion

    #region Read
    public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
    }

    public async Task<EnrollmentDto> GetByIdAsync(int id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(filter: (u) => u.Id == id);

        if (enrollment == null)
            throw new NotFoundException("Enrollment not found");

        return _mapper.Map<EnrollmentDto>(enrollment);
    }
    #endregion

    #region Write
    public async Task AddAsync(AddEnrollmentDto addEnrollmentDto)
    {
        var validateUser = await ValidateStudentByUserId(addEnrollmentDto.StudentUserId);

        addEnrollmentDto.StudentId = validateUser.Id;

        var enrollment = _mapper.Map<Enrollment>(addEnrollmentDto);
        await _enrollmentRepository.AddAsync(enrollment);

        _logger.LogInformation($"Enrollment was added: {addEnrollmentDto.StudentId}|{addEnrollmentDto.TeacherCourseMapId}");
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var validateUser = await ValidateStudentByUserId(userId);

        var enrollment = await GetAndValidateEnrollmentsAsync(id, validateUser.Id);

        await _enrollmentRepository.DeleteAsync(enrollment);

        _logger.LogInformation($"Enrollment was deleted: {id}");
    }

    public async Task UpdateAsync(UpdateEnrollmentDto updateEnrollmentDto)
    {
        var validateUser = await ValidateStudentByUserId(updateEnrollmentDto.StudentUserId);

        await GetAndValidateEnrollmentsAsync(updateEnrollmentDto.Id, validateUser.Id);

        var updatedEnrollment = _mapper.Map<Enrollment>(updateEnrollmentDto);

        await _enrollmentRepository.UpdateAsync(updatedEnrollment);

        _logger.LogInformation($"Enrollment was updated: {updateEnrollmentDto.Id}");
    }
    #endregion

    #region Other
    private async Task<Student> ValidateStudentByUserId(string userId)
    {
        var student = await _studentRepository.GetByUserIdAsync(userId);

        if (student is null)
            throw new BadRequestException("Student profile already exists");

        return student;
    }

    private async Task<Enrollment> GetAndValidateEnrollmentsAsync(int enrollmentId, int studentId)
    {
        var enrollments = await _enrollmentRepository.GetByStudentId(studentId);

        var enrollment = enrollments.FirstOrDefault(x => x.Id == enrollmentId);

        if (enrollment is null)
            throw new BadRequestException("You can't make changes to this enrollment");

        return enrollment;
    }
    #endregion
}
