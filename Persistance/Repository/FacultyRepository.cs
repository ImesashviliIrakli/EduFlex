using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;
public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
    private readonly AppDBContext _dbContext;
    public FacultyRepository(AppDBContext db) : base(db)
    {
        _dbContext = db;
    }

    public async Task<Faculty> GetById(int id)
    {
        return await _dbContext.Faculties.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}
