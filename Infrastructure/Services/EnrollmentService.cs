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
    public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsAsync()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
    }

    public async Task<EnrollmentDto> GetEnrollmentAsync(int enrollmentId)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(filter: (u) => u.Id == enrollmentId);

        if (enrollment == null)
            throw new NotFoundException("Enrollment not found");

        return _mapper.Map<EnrollmentDto>(enrollment);
    }
    #endregion

    #region Write
    public async Task EnrollAsync(AddEnrollmentDto addEnrollmentDto)
    {
        var enrollment = _mapper.Map<Enrollment>(addEnrollmentDto);

        await _enrollmentRepository.AddAsync(enrollment);

        _logger.LogInformation($"Enrollment was added to: {addEnrollmentDto.TeacherCourseMapId}");
    }

    public async Task UnEnrollAsync(int enrollmentId, string studentUserId)
    {
        var enrollment = await GetAndValidateEnrollmentsAsync(enrollmentId, studentUserId);

        await _enrollmentRepository.DeleteAsync(enrollment);

        _logger.LogInformation($"Enrollment was deleted: {enrollmentId}");
    }
    #endregion

    #region Other
    private async Task<Enrollment> GetAndValidateEnrollmentsAsync(int enrollmentId, string studentUserId)
    {
        var enrollments = await _enrollmentRepository.GetByStudentUserIdAsync(studentUserId);

        var enrollment = enrollments.FirstOrDefault(x => x.Id == enrollmentId);

        if (enrollment is null)
            throw new BadRequestException("You can't make changes to this enrollment");

        return enrollment;
    }
    #endregion
}
