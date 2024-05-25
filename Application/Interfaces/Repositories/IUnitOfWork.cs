namespace Application.Interfaces.Repositories;
public interface IUnitOfWork
{
	ICourseRepository Course { get; }
	IFacultyRepository Faculty { get; }
}
