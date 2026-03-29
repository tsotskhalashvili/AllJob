using AllJob.API.Responses;

namespace AllJob.API.Services.Interfaces;

public interface IExceptionResponseService
{
    ApiErrorResponse GetErrorResponse(Exception ex, bool isDevelopment);
}
