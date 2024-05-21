using EduFlex.API.Enums;

namespace EduFlex.API.Models;

public class ResponseModel
{
    public ResponseModel(Status status, string message, object? result = null)
    {
        this.Status = status;
        this.Message = message;
        this.Result = result;
    }
    public Status Status { get; set; } = Status.Success;
    public string Message { get; set; } = "Successful request";
    public object? Result { get; set; }
}
