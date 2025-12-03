using System.Security.Claims;

namespace P7CreateRestApi.Middlewares
{
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
            var startTime = DateTime.UtcNow;

            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Non authentifié";
            var userName = context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonyme";
            var userRoles = string.Join(", ", context.User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? new List<string>());

            _logger.LogInformation(
                "[{Timestamp}] {Method} {Path} | User: {UserName} (ID: {UserId}) | Roles: [{Roles}] | IP: {IP}",
                startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                context.Request.Method,
                context.Request.Path,
                userName,
                userId,
                userRoles,
                context.Connection.RemoteIpAddress
            );

            await _next(context);

            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation(
                "[{Timestamp}] {Method} {Path} - Status: {StatusCode} - Durée: {Duration}ms | User: {UserName}",
                DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                duration.TotalMilliseconds,
                userName
            );

            if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "DELETE")
            {
                _logger.LogWarning(
                    "MODIFICATION | {Method} {Path} | Par: {UserName} (ID: {UserId}) | Roles: [{Roles}] | Status: {StatusCode}",
                    context.Request.Method,
                    context.Request.Path,
                    userName,
                    userId,
                    userRoles,
                    context.Response.StatusCode
                );
            }
        }
    }
}