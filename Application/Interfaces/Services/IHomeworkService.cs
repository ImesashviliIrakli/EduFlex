using Application.Models.Dtos.HomeworkDtos;

namespace Application.Interfaces.Services;

public interface IHomeworkService
{
    Task<IEnumerable<HomeworkDto>> GetHomeworksAsync(string studentUserId, int teacherCourseId);
    Task UploadHomeworkAsync(UploadHomeworkDto uploadHomeworkDto);
    Task UpdateHomeworkAsync(UpdateHomeworkDto updateHomeworkDto);
    Task UpdateHomeworkGradeAsync(UpdateHomeworkGradeDto updateHomeworkGradeDto);
    Task DeleteHomeworkGradeAsync(int homeworkId, string studentUserId);
}
