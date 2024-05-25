using Application.Interfaces.Repositories;
using Persistance.Data;

namespace Persistance.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDBContext _dbContext;
		public IFacultyRepository Faculty { get; private set; }
		public ICourseRepository Course { get; private set; }

		public UnitOfWork(AppDBContext db)
		{
			_dbContext = db;
			Faculty = new FacultyRepository(db);
			Course = new CourseRepository(db);
		}
	}
}
