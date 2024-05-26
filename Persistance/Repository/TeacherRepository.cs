using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{
    private readonly AppDBContext _dbContext;
    public TeacherRepository(AppDBContext db) : base(db)
    {
        _dbContext = db;
    }

    public async Task<Teacher> GetByUserIdAsync(string userId)
    {
        return await _dbContext.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
