using Application.Models.Dtos.FacultyDtos;

namespace Application.Interfaces.Services
{
	public interface IFacultyService
	{
		Task<IEnumerable<FacultyDto>> GetAllAsync();
		Task<FacultyDto> GetByIdAsync(int id);
		Task AddAsync(AddFacultyDto addFacultyDto);
		Task DeleteAsync(int id);
		Task UpdateAsync(UpdateFacultyDto updateFacultyDto);
	}
}
