using Application.Exceptions;
using Application.Interfaces.Facades;
using Application.Interfaces.FileService;
using Application.Interfaces.Services;
using Application.Models.Dtos.HomeworkDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class HomeworkService : IHomeworkService
{
    #region Injection
    private readonly IHomeworkFacade _facade;
    private readonly IMapper _mapper;
    private readonly ILogger<HomeworkService> _logger;
    private readonly IFileService<Homework> _fileService;
    public HomeworkService(
        IHomeworkFacade facade,
        IMapper mapper,
        ILogger<HomeworkService> logger,
        IFileService<Homework> fileService
        )
    {
        _facade = facade;
        _mapper = mapper;
        _logger = logger;
        _fileService = fileService;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<HomeworkDto>> GetHomeworksAsync(string studentUserId, int teacherCourseId)
    {
        var homeworks = await _facade.GetHomeworksAsync(studentUserId, teacherCourseId);
        return _mapper.Map<IEnumerable<HomeworkDto>>(homeworks);
    }
    #endregion

    #region Write
    public async Task UploadHomeworkAsync(UploadHomeworkDto uploadHomeworkDto)
    {
        var enrollment = await GetAndValidateEnrollmentAsync(uploadHomeworkDto.StudentUserId, uploadHomeworkDto.EnrollmentId);

        string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(uploadHomeworkDto.File.FileName)}";
        uploadHomeworkDto.FileUrl = await _fileService.SaveFileAsync(uploadHomeworkDto.File, fileName);

        var homework = _mapper.Map<Homework>(uploadHomeworkDto);

        await _facade.UploadHomeworkAsync(homework);

        _logger.LogInformation($"Uploaded homework successfully: {uploadHomeworkDto.FileUrl}");
    }

    public async Task UpdateHomeworkAsync(UpdateHomeworkDto updateHomeworkDto)
    {
        var enrollment = await GetAndValidateEnrollmentAsync(updateHomeworkDto.StudentUserId, updateHomeworkDto.EnrollmentId);

        var homework = await GetAndValidateHomeworkAsync(updateHomeworkDto.Id, updateHomeworkDto.StudentUserId);

        string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(updateHomeworkDto.File.FileName)}";

        homework.FileUrl = await _fileService.UpdateFileAsync(updateHomeworkDto.File, fileName, homework.FileUrl);

        await _facade.UpdateHomeworkAsync(homework);

        _logger.LogInformation($"Updated homework successfully: {updateHomeworkDto.Id}");
    }

    public async Task UpdateHomeworkGradeAsync(UpdateHomeworkGradeDto updateHomeworkGradeDto)
    {
        var teacher = await GetAndValidateTeacherAsync(updateHomeworkGradeDto.TeacherUserId);

        var homework = await GetAndValidateHomeworkAsync(updateHomeworkGradeDto.Id);

        var teacherCourse = await GetAndValidateTeacherCourseAsync(homework.TeacherCourseId, teacher.Id);

        homework.Grade = updateHomeworkGradeDto.Grade;

        await _facade.UpdateHomeworkAsync(homework);

        _logger.LogInformation($"Updated homework successfully: {updateHomeworkGradeDto.Id}");
    }

    public async Task DeleteHomeworkAsync(int homeworkId, string studentUserId)
    {
        var homework = await GetAndValidateHomeworkAsync(homeworkId, studentUserId);

        await _facade.DeleteHomeworkAsync(homework);

        await _fileService.DeleteFileAsync(homework.FileUrl);

        _logger.LogInformation($"Deleted homework successfully: {homeworkId}");

    }
    #endregion

    #region Other
    private async Task<Enrollment> GetAndValidateEnrollmentAsync(string studentUserId, int enrollmentId)
    {
        var enrollment = await _facade.GetEnrollmentAsync(studentUserId, enrollmentId);

        if (enrollment is null)
            throw new NotFoundException($"Couldn't find enrollment with id:{enrollmentId} for student: {studentUserId}");

        return enrollment;
    }

    private async Task<Homework> GetAndValidateHomeworkAsync(int homeworkId, string? studentUserId = null)
    {
        var homework = await _facade.GetHomeworkByIdAsync(homeworkId);

        if (homework is null)
            throw new NotFoundException($"Couldn't find Homework with id: {homeworkId}");

        if (homework.Grade != 0)
            throw new BadRequestException($"Homework with id:{homeworkId} has already been graded");

        if (string.IsNullOrEmpty(studentUserId) && homework.StudentUserId != studentUserId)
            throw new BadRequestException($"Student with id:{studentUserId} doesn't have permission");

        return homework;
    }

    private async Task<Teacher> GetAndValidateTeacherAsync(string teacherUserId)
    {
        var teacher = await _facade.GetTeacherAsync(teacherUserId);

        if (teacher is null)
            throw new NotFoundException($"Couldn't find teacher with userId: {teacherUserId}");

        return teacher;
    }

    private async Task<TeacherCourse> GetAndValidateTeacherCourseAsync(int teacherCourseId, int teacherId)
    {
        var teacherCourse = await _facade.GetTeacherCourseAsync(teacherCourseId);

        if (teacherCourse is null)
            throw new NotFoundException($"Couldn't find teacherCourseMap with id: {teacherCourseId}");

        if (teacherCourse.TeacherId != teacherId)
            throw new BadRequestException($"Teacher with id:{teacherId} doesn't have permission to grade this homework");

        return teacherCourse;
    }
    #endregion
}
