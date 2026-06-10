namespace WebApiSolution.API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method;
        var path   = context.Request.Path;
        var start  = DateTime.UtcNow;
        var sw     = System.Diagnostics.Stopwatch.StartNew();

        _logger.LogInformation("→ [{Method}] {Path} | Started: {Time}",
            method, path, start.ToString("HH:mm:ss.fff"));

        await _next(context);

        sw.Stop();
        var statusCode = context.Response.StatusCode;

        _logger.LogInformation("← [{Method}] {Path} | Status: {StatusCode} | Duration: {Ms} ms",
            method, path, statusCode, sw.ElapsedMilliseconds);
    }
}