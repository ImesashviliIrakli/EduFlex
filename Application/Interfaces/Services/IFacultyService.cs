using Application.Models.Dtos.FacultyDtos;

namespace Application.Interfaces.Services
{
	public interface IFacultyService
	{
		Task<IEnumerable<FacultyDto>> GetAllAsync();
		Task<FacultyDto> GetByIdAsync(int id);
		Task<FacultyDto> AddAsync(AddFacultyDto entity);
		Task<bool> DeleteAsync(int id);
		Task<FacultyDto> UpdateAsync(int id, UpdateFacultyDto entity);
	}
}
