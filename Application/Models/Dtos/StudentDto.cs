namespace Application.Models.Dtos;

public class StudentDto
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PrivateNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}
