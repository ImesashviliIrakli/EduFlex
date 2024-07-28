using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.FacultyDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class FacultyService : IFacultyService
    {
        #region Injection
        private readonly IFacultyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<FacultyService> _logger;

        public FacultyService(
            IFacultyRepository facultyRepository,
            IMapper mapper,
            ILogger<FacultyService> logger
            )
        {
            _repository = facultyRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<FacultyDto>> GetAllAsync()
        {
            var faculties = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<FacultyDto>>(faculties);

            return result;
        }

        public async Task<FacultyDto> GetByIdAsync(int id)
        {
            var faculty = await _repository.GetByIdAsync(filter: (u) => u.Id == id);
            var result = _mapper.Map<FacultyDto>(faculty);

            return result;
        }
        #endregion

        #region Write
        public async Task AddAsync(AddFacultyDto addFacultyDto)
        {
            var faculty = _mapper.Map<Faculty>(addFacultyDto);

            await _repository.AddAsync(faculty);

            _logger.LogInformation($"Faculty was added: {addFacultyDto.Name}");
        }

        public async Task DeleteAsync(int id)
        {
            var faculty = await _repository.GetById(id);

            if (faculty is null)
                throw new NotFoundException($"Could not find faculty with Id:{id}");

            await _repository.DeleteAsync(faculty);

            _logger.LogInformation($"Faculty was deleted: {id}");
        }

        public async Task UpdateAsync(UpdateFacultyDto updateFacultyDto)
        {
            var check = await _repository.GetById(updateFacultyDto.Id);

            if (check is null)
                throw new NotFoundException($"Could not find faculty with id:{updateFacultyDto.Id}");

            var faculty = _mapper.Map<Faculty>(updateFacultyDto);

            await _repository.UpdateAsync(faculty);

            _logger.LogInformation($"Faculty was updated: {updateFacultyDto.Id}");
        }
        #endregion
    }
}
