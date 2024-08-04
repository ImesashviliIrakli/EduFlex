using Application.Exceptions;
using Application.Interfaces.FileService;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.AssignmentDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class AssignmentService : IAssignmentService
{
    #region Injection
    private readonly IAssignmentRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<AssignmentService> _logger;
    private readonly IFileService<Assignment> _fileService;
    public AssignmentService(
            IAssignmentRepository repository,
            IMapper mapper,
            ILogger<AssignmentService> logger,
            IFileService<Assignment> fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _fileService = fileService;
    }
    #endregion

    #region Read
    public async Task<List<AssignmentDto>> GetAssignmentsAsync(int teacherCourseId, bool isActive)
    {
        var assignments = await _repository.GetAssignmentsAsync(teacherCourseId, isActive);

        var assignmentDtos = _mapper.Map<List<AssignmentDto>>(assignments);

        return assignmentDtos;
    }

    public async Task<AssignmentDto> GetAssignmentByIdAsync(int assignmentId)
    {
        var assignment = await _repository.GetAssignmentByIdAsync(assignmentId);

        if (assignment == null)
            throw new NotFoundException($"Could not find assignment with id: {assignmentId}");

        var assignemtDto = _mapper.Map<AssignmentDto>(assignment);

        return assignemtDto;
    }
    #endregion

    #region Write
    public async Task CreateAssignmentAsync(AddAssignmentDto addAssignmentDto)
    {
        if (addAssignmentDto.File != null)
        {
            string fileName = $"{addAssignmentDto.Title}_{Path.GetFileName(addAssignmentDto.File.FileName)}";
            addAssignmentDto.FileUrl = await _fileService.SaveFileAsync(addAssignmentDto.File, fileName);

            _logger.LogInformation($"Added assignment file: {fileName}");
        }

        var assignent = _mapper.Map<Assignment>(addAssignmentDto);

        await _repository.AddAsync(assignent);

        _logger.LogInformation($"Added new assignment");
    }

    public async Task UpdateAssignmentAsync(UpdateAssignmentDto updateAssignmentDto)
    {
        var assignment = await _repository.GetAssignmentByIdAsync(updateAssignmentDto.Id);

        if (updateAssignmentDto.File != null)
        {
            string fileName = $"{updateAssignmentDto.Title}_{Path.GetFileName(updateAssignmentDto.File.FileName)}";
            assignment.FileUrl = await _fileService.UpdateFileAsync(updateAssignmentDto.File, fileName, assignment.FileUrl);

            _logger.LogInformation($"Added new assignment file: {fileName}");
        }

        assignment.Title = updateAssignmentDto.Title;
        assignment.Description = updateAssignmentDto.Description;
        assignment.MaxGrade = updateAssignmentDto.MaxGrade;
        assignment.MinGrade = updateAssignmentDto.MinGrade;
        assignment.IsActive = updateAssignmentDto.IsActive;

        await _repository.UpdateAsync(assignment);

        _logger.LogInformation($"Assignment was updated: {assignment.Id}");
    }

    public async Task DeleteAssignmentAsync(int assignmentId)
    {
        var assignment = await _repository.GetAssignmentByIdAsync(assignmentId);

        if (assignment == null)
            throw new NotFoundException($"Could not find assignment with id: {assignmentId}");

        await _repository.DeleteAsync(assignment);

        _logger.LogInformation($"Deleted assignment {assignmentId}");

        await _fileService.DeleteFileAsync(assignment.FileUrl);

        _logger.LogInformation("Deleted assignment file");
    }
    #endregion
}
