using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class TeacherCourseRepository : Repository<TeacherCourse>, ITeacherCourseRepository
{
    private readonly AppDBContext _context;
    public TeacherCourseRepository(AppDBContext db) : base(db)
    {
        _context = db;
    }

    public async Task<IEnumerable<TeacherCourse>> GetByTeacherId(int teacherId)
    {
        return await _context.TeacherCourseMaps.Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
    }
}
