using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly AppDBContext _dbContext;
    public StudentRepository(AppDBContext db) : base(db)
    {
        _dbContext = db;
    }
    public async Task<Student> GetByUserIdAsync(string userId)
    {
        return await _dbContext.Students.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
