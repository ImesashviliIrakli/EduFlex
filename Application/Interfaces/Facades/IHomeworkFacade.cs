using Domain;

namespace Application.Interfaces.Facades;

public interface IHomeworkFacade
{
    Task<IEnumerable<Homework>> GetHomeworksAsync(string studentUserId, int teacherCourseId);
    Task<Homework> GetHomeworkByIdAsync(int homeworkId);
    Task<Enrollment> GetEnrollmentAsync(string studentUserId, int enrollmentId);
    Task<Teacher> GetTeacherAsync(string teacherUserId);
    Task<TeacherCourse> GetTeacherCourseAsync(int teacherCourseId);
    Task UploadHomeworkAsync(Homework homework);
    Task UpdateHomeworkAsync(Homework homework);
    Task DeleteHomeworkAsync(Homework homework);
}
