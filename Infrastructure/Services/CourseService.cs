using Application.Interfaces.FileService;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.CourseDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class CourseService : ICourseService
{
    #region Injection
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CourseService> _logger;
    private readonly IFileService<Course> _imageService;

    public CourseService(
        ICourseRepository courseRepository,
        IMapper mapper,
        ILogger<CourseService> logger,
        IFileService<Course> imageService
        )
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _imageService = imageService;
        _logger = logger;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync(includeProperties: "Faculty");
        var result = _mapper.Map<IEnumerable<CourseDto>>(courses);

        return result;
    }

    public async Task<CourseDto> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(filter: (u) => u.Id == id, includeProperties: "Faculty");
        var result = _mapper.Map<CourseDto>(course);

        return result;
    }
    #endregion

    #region Write
    public async Task CreateCourseAsync(AddCourseDto addCourseDto)
    {
        string fileName = $"{addCourseDto.Title}_{Path.GetFileName(addCourseDto.File.FileName)}";

        addCourseDto.ImageUrl = await _imageService.SaveFileAsync(addCourseDto.File, fileName);

        var course = _mapper.Map<Course>(addCourseDto);

        await _courseRepository.AddAsync(course);

        _logger.LogInformation($"Course was added: {addCourseDto.Title}");
    }

    public async Task UpdateCourseAsync(UpdateCourseDto updateCourseDto)
    {
        var course = await _courseRepository.GetByIdAsync(filter: (u) => u.Id == updateCourseDto.Id);

        if (updateCourseDto.File != null)
        {
            string fileName = $"{updateCourseDto.Title}_{Path.GetFileName(updateCourseDto.File.FileName)}";
            course.ImageUrl = await _imageService.UpdateFileAsync(updateCourseDto.File, fileName, course.ImageUrl);
        }

        course.Title = updateCourseDto.Title;
        course.Description = updateCourseDto.Description;
        course.Price = updateCourseDto.Price;
        course.FacultyId = updateCourseDto.FacultyId;

        await _courseRepository.UpdateAsync(course);

        _logger.LogInformation($"Course was updated: {updateCourseDto.Id}");
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(filter: (u) => u.Id == id);

        await _courseRepository.DeleteAsync(course);

        await _imageService.DeleteFileAsync(course.ImageUrl);

        _logger.LogInformation($"Course was deleted: {id}");
    }
    #endregion
}
