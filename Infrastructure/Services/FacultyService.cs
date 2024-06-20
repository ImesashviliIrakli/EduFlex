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
		private readonly IFacultyRepository _facultyRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<FacultyService> _logger;

		public FacultyService(IFacultyRepository facultyRepository, IMapper mapper, ILogger<FacultyService> logger)
		{
			_facultyRepository = facultyRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<FacultyDto> AddAsync(AddFacultyDto entity)
		{
			var faculty = _mapper.Map<Faculty>(entity);

			var add = await _facultyRepository.AddAsync(faculty);

			_logger.LogInformation($"Faculty was added: {add.Id}");

			var result = _mapper.Map<FacultyDto>(add);

			return result;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var delete = await _facultyRepository.DeleteAsync(id);

			if (delete)
			{
				_logger.LogInformation($"Faculty was deleted: {id}");
			}
			else
			{
				_logger.LogError($"Faculty wasn't deleted: {id}");
			}

			return delete;
		}

		public async Task<IEnumerable<FacultyDto>> GetAllAsync()
		{
			var faculties = await _facultyRepository.GetAllAsync();
			var result = _mapper.Map<IEnumerable<FacultyDto>>(faculties);

			return result;
		}

		public async Task<FacultyDto> GetByIdAsync(int id)
		{
			var faculty = await _facultyRepository.GetByIdAsync(filter: (u) => u.Id == id);
			var result = _mapper.Map<FacultyDto>(faculty);

			return result;
		}

		public async Task<FacultyDto> UpdateAsync(int id, UpdateFacultyDto entity)
		{
			var faculty = _mapper.Map<Faculty>(entity);

			var update = await _facultyRepository.UpdateAsync(id, faculty);

			_logger.LogInformation($"Faculty was updated: {id}");

			var result = _mapper.Map<FacultyDto>(update);

			return result;
		}
	}
}
