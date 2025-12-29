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

            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = context.User?.FindFirst(ClaimTypes.Name)?.Value;
            var userRolesList = context.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() ?? new List<string>();
            var userRoles = userRolesList.Any() ? string.Join(", ", userRolesList) : "Aucun";

            var isAuthenticated = context.User?.Identity?.IsAuthenticated == true;
            var userInfo = isAuthenticated
                ? $"{userName ?? "Inconnu"} (ID: {userId ?? "Inconnu"}) | Rôles: [{userRoles}]"
                : "Non authentifié";

            _logger.LogInformation(
                "[{Timestamp}] {Method} {Path} | Utilisateur: {UserInfo} | IP: {IP}",
                startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                context.Request.Method,
                context.Request.Path,
                userInfo,
                context.Connection.RemoteIpAddress
            );

            await _next(context);

            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation(
                "[{Timestamp}] {Method} {Path} - Status: {StatusCode} - Durée: {Duration}ms | Utilisateur: {UserInfo}",
                DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                duration.TotalMilliseconds,
                userInfo
            );

            if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "DELETE")
            {
                _logger.LogWarning(
                    "MODIFICATION | {Method} {Path} | Par: {UserInfo} | Status: {StatusCode}",
                    context.Request.Method,
                    context.Request.Path,
                    userInfo,
                    context.Response.StatusCode
                );
            }
        }

    }
}