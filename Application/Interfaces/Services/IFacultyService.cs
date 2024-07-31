using Application.Models.Dtos.FacultyDtos;

namespace Application.Interfaces.Services
{
	public interface IFacultyService
	{
		Task<IEnumerable<FacultyDto>> GetFacultiesAsync();
		Task<FacultyDto> GetFacultyByIdAsync(int id);
		Task CreateFacultyAsync(AddFacultyDto addFacultyDto);
		Task UpdateFacultyAsync(UpdateFacultyDto updateFacultyDto);
        Task DeleteFacultyAsync(int id);
    }
}
