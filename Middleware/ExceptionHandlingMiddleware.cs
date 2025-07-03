using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AbcloudzWebAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string title;
            string detail;

            switch (exception)
            {
                case KeyNotFoundException notFoundException:
                {
                    statusCode = (int)HttpStatusCode.NotFound;
                    title = HttpStatusCode.NotFound.ToString();
                    detail = notFoundException.Message ?? "Entity is not found";
                    break;
                }

                case ArgumentException argumentException:
                    {
                        statusCode = (int)HttpStatusCode.BadRequest;
                        title = HttpStatusCode.BadRequest.ToString();
                        detail = exception.Message ?? "Entity validation failed";
                        break;
                    }

                default:
                    {
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        detail = "An error occurred while processing your request.";
                        title = HttpStatusCode.InternalServerError.ToString();
                        break;
                    }
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Detail = detail,
                Title = title,
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/problem+json";

            var json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);
        }
    }

}
