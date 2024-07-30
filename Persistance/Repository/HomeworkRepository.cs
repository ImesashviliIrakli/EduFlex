using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly AppDBContext _context;
    #region Injection
    public HomeworkRepository(AppDBContext context)
    {
        _context = context;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<Homework>> GetHomeworksAsync(string studentUserId, int teacherCourseId)
    {
        var homeworks = await _context.Homeworks
            .Where(
                x =>
                x.StudentUserId.Equals(studentUserId) &&
                x.TeacherCourseId.Equals(teacherCourseId)
            ).ToListAsync();

        return homeworks;
    }

    public async Task<Homework> GetHomeworkByIdAsync(int homeworkId)
    {
        return await _context.Homeworks.FirstOrDefaultAsync(x=> x.Id.Equals(homeworkId));
    }
    #endregion

    #region Write
    public async Task UploadHomeworkAsync(Homework homework)
    {
        await _context.Homeworks.AddAsync(homework);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateHomeworkAsync(Homework homework)
    {
        _context.Homeworks.Update(homework);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHomeworkAsync(Homework homework)
    {
        _context.Homeworks.Remove(homework);
        await _context.SaveChangesAsync();
    }
    #endregion
}
