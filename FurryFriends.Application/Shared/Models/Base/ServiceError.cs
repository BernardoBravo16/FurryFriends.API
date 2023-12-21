namespace FurryFriends.Application.Shared.Models.Base;

public class ServiceError
{
    public string ErrorMessage { get; set; }
    public string ErrorDetail { get; set; }
    public ServiceErrorTypeEnum ServiceErrorType { get; set; }

    public ServiceError(Exception ex)
    {
        ErrorMessage = ex.Message;
        ErrorDetail = ex.StackTrace;
        ServiceErrorType = ServiceErrorTypeEnum.EXCEPTION_ERROR;
    }

    public ServiceError(string message)
    {
        ErrorMessage = message;
        ErrorDetail = message;
        ServiceErrorType = ServiceErrorTypeEnum.VALIDATION_ERROR;
    }
}