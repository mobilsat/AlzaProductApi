using System.Diagnostics;
using AlzaProductApi.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AlzaProductApi.Web.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			if (context.Response.HasStarted)
				throw;

			var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

			var (status, title, detail, type) = ex switch
			{
				ProductNotFoundException pnfe => (
					StatusCodes.Status404NotFound,
					"Product not found",
					pnfe.Message,
					"https://httpstatuses.com/404"
				),

				ArgumentException ae => (
					StatusCodes.Status400BadRequest,
					"Invalid request",
					ae.Message,
					"https://httpstatuses.com/400"
				),

				_ => (
					StatusCodes.Status500InternalServerError,
					"Internal server error",
					"An unexpected error occurred.",
					"https://httpstatuses.com/500"
				)
			};

			// 500 logujeme jako Error, 4xx klidně jako Information/Warning (volitelné)
			if (status >= 500)
				logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", traceId);
			else
				logger.LogWarning(ex, "Request failed. TraceId: {TraceId}", traceId);

			var problem = new ProblemDetails
			{
				Status = status,
				Title = title,
				Detail = detail,
				Type = type,
				Instance = context.Request.Path
			};

			problem.Extensions["traceId"] = traceId;

			context.Response.StatusCode = status;
			context.Response.ContentType = "application/problem+json";

			await context.Response.WriteAsJsonAsync(problem);
		}
	}
}
