using Application.Interfaces.FileService;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.CourseDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class CourseService : ICourseService
    {
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

        public async Task<CourseDto> AddAsync(AddCourseDto entity)
        {
            string fileName = $"{entity.Title}_{Path.GetFileName(entity.File.FileName)}";

            entity.ImageUrl = await _imageService.SaveFileAsync(entity.File, fileName);

            var course = _mapper.Map<Course>(entity);

            var add = await _courseRepository.AddAsync(course);

            _logger.LogInformation($"Course was added: {add.Id}");

            var result = _mapper.Map<CourseDto>(add);

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(filter: (u) => u.Id == id);

            var delete = await _courseRepository.DeleteAsync(id);

            await _imageService.DeleteFileAsync(course.ImageUrl);

            if (delete)
            {
                _logger.LogInformation($"Course was deleted: {id}");
            }
            else
            {
                _logger.LogError($"Course wasn't deleted: {id}");
            }

            return delete;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync(includeProperties: "Faculty");
            var result = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return result;
        }

        public async Task<CourseDto> GetByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(filter: (u) => u.Id == id, includeProperties: "Faculty");
            var result = _mapper.Map<CourseDto>(course);

            return result;
        }

        public async Task<CourseDto> UpdateAsync(int id, UpdateCourseDto entity)
        {
            var course = _mapper.Map<Course>(entity);

            if (entity.File != null)
            {
                string fileName = $"{entity.Title}_{Path.GetFileName(entity.File.FileName)}";
                course.ImageUrl = await _imageService.UpdateFileAsync(entity.File, fileName, entity.ImageUrl);
            }

            var update = await _courseRepository.UpdateAsync(id, course);

            _logger.LogInformation($"Course was updated: {id}");

            var result = _mapper.Map<CourseDto>(update);

            return result;
        }
    }
}
