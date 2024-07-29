using Domain;

namespace Application.Interfaces.Repositories;

public interface IHomeworkRepository
{
    Task<IEnumerable<Homework>> GetHomeworksAsync(string studentUserId, int teacherCourseId);
    Task UploadHomeworkAsync(Homework homework);
    Task UpdateHomeworkAsync(Homework homework);
    Task DeleteHomeworkAsync(int homeworkId);
}
