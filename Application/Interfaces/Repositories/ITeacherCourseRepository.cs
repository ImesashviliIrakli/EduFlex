using Domain;

namespace Application.Interfaces.Repositories;

public interface ITeacherCourseRepository : IRepository<TeacherCourse>
{
    Task<IEnumerable<TeacherCourse>> GetByTeacherId(int teacherId);
}
