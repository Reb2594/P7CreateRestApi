using System.Net;
using System.Text.Json;

namespace P7CreateRestApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Méthode appelée pour chaque requête HTTP
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Essaie d'exécuter le reste de l'application
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite");

                // Erreur en réponse JSON
                await EnvoyerMessageErreur(context, ex);
            }
        }

        // Prépare et envoie le message d'erreur au client
        private async Task EnvoyerMessageErreur(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ChoisirCodeErreur(exception);

            var messageErreur = new
            {
                statusCode = context.Response.StatusCode,
                message = exception.Message
            };

            var json = JsonSerializer.Serialize(messageErreur);
            await context.Response.WriteAsync(json);
        }

        // Quel code HTTP renvoyer ?
        private int ChoisirCodeErreur(Exception exception)
        {
            // 404
            if (exception is KeyNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }                

            // 401
            if (exception is UnauthorizedAccessException)
            {
                return (int)HttpStatusCode.Unauthorized;
            }

            // 400
            if (exception is ArgumentException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            //408
            if (exception is TimeoutException)
            {
                return (int)HttpStatusCode.RequestTimeout;
            }

            // 409
            if (exception is InvalidOperationException)
            {
                return (int)HttpStatusCode.Conflict;
            }

            // 500
            return (int)HttpStatusCode.InternalServerError;
        }
    }
}