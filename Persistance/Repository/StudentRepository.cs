using Application.Interfaces.Repositories;
using Domain;
using Persistance.Data;

namespace Persistance.Repository;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly AppDBContext _dbContext;
    public StudentRepository(AppDBContext db) : base(db)
    {
        _dbContext = db;
    }
}
