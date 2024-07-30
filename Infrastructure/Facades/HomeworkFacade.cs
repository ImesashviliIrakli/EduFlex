using Application.Interfaces.Facades;
using Application.Interfaces.Repositories;
using Domain;

namespace Infrastructure.Facades;

public class HomeworkFacade : IHomeworkFacade
{
    #region Injection
    private readonly IHomeworkRepository _repository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITeacherCourseRepository _teacherCourseRepository;
    public HomeworkFacade(
        IHomeworkRepository repository,
        IEnrollmentRepository enrollmentRepository,
        ITeacherRepository teacherRepository,
        ITeacherCourseRepository teacherCourseRepository
        )
    {
        _repository = repository;
        _enrollmentRepository = enrollmentRepository;
        _teacherRepository = teacherRepository;
        _teacherCourseRepository = teacherCourseRepository;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<Homework>> GetHomeworksAsync(string studentUserId, int teacherCourseId)
    {
        return await _repository.GetHomeworksAsync(studentUserId, teacherCourseId);
    }

    public async Task<Homework> GetHomeworkByIdAsync(int homeworkId)
    {
        return await _repository.GetHomeworkByIdAsync(homeworkId);
    }

    public async Task<Enrollment> GetEnrollmentAsync(string studentUserId, int enrollmentId)
    {
        return await _enrollmentRepository.GetByStudentUserIdAndEnrollmentIdAsync(studentUserId, enrollmentId);
    }
    public async Task<Teacher> GetTeacherAsync(string teacherUserId)
    {
        return await _teacherRepository.GetByUserIdAsync(teacherUserId);
    }

    public async Task<TeacherCourse> GetTeacherCourseAsync(int teacherCourseId)
    {
        return await _teacherCourseRepository.GetByIdAsync(filter: (u) => u.Id.Equals(teacherCourseId));
    }
    #endregion

    #region Write
    public async Task UploadHomeworkAsync(Homework homework)
    {
        await _repository.UploadHomeworkAsync(homework);
    }

    public async Task UpdateHomeworkAsync(Homework homework)
    {
        await _repository.UpdateHomeworkAsync(homework);
    }

    public async Task DeleteHomeworkAsync(Homework homework)
    {
        await _repository.DeleteHomeworkAsync(homework);
    }
    #endregion
}
