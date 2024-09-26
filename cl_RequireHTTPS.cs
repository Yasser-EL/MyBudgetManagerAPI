using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyBudgetManagerAPI;

public class cl_RequireHttps
{
    private readonly RequestDelegate _next;

    public cl_RequireHttps(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.IsHttps)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden; // Forbidden status code
            await context.Response.WriteAsync("HTTPS is required."); // Custom error message
            return; // Terminate the request
        }

        await _next(context); // Call the next middleware in the pipeline
    }
}
