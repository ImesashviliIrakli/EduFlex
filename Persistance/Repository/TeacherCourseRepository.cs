using Application.Interfaces.Repositories;
using Domain;
using Persistance.Data;

namespace Persistance.Repository;

public class TeacherCourseRepository : Repository<TeacherCourse>, ITeacherCourseRepository
{
    private readonly AppDBContext _dbContext;
    public TeacherCourseRepository(AppDBContext db) : base(db)
    {
        _dbContext = db;
    }
}
