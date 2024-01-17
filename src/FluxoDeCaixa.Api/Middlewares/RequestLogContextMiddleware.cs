namespace FluxoDeCaixa.Api.Middlewares
{
    public class RequestLogContextMiddleware(RequestDelegate next)
    {
        public Task InvokeAsync(HttpContext context, ILogger<RequestLogContextMiddleware> logger)
        {
            logger.LogInformation("HTTP {RequestMethod} {RequestPath} started", context.Request.Method, context.Request.Path);
            return next(context);
        }
    }
}