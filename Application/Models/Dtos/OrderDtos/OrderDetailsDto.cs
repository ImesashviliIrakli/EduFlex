using Application.Models.Dtos.StudentDtos;

namespace Application.Models.Dtos.OrderDtos;

public class OrderDetailsDto
{
    public int Id { get; set; }
    public required int StudentId { get; set; }
    public int TeacherCourseId { get; set; }
    public decimal Amount { get; set; }
    public int Status { get; set; }

    public StudentDto StudentDto { get; set; }
}

