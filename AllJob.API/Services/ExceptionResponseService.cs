using AllJob.API.Responses;
using AllJob.API.Services.Interfaces;
using AllJob.Application.Exceptions;
using System.Net;

namespace AllJob.API.Services;

public class ExceptionResponseService : IExceptionResponseService
{
    public ApiErrorResponse GetErrorResponse(
        Exception ex, bool isDevelopment)
    {
        return ex switch
        {
            NotFoundException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = isDevelopment
                    ? ex.Message : "Resource not found"
            },
            ValidationException v => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Validation failed",
                  
                Errors = v.Errors
            },
            UnauthorizedException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Message = "Unauthorized"
            },
            ForbiddenException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Forbidden,
                Message = "Access denied"
            },
            ConflictException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Conflict,
                Message = isDevelopment
                    ? ex.Message : "Conflict occurred"
            },
            _ => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = isDevelopment
                    ? ex.Message : "An internal server error occurred"
            }
        };
    }
}