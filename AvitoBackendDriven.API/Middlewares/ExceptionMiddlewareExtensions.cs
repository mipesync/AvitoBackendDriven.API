namespace AvitoBackendDriven.API.Middlewares;

/// <summary>
/// Расширение Middleware для обработки исключений
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Добавить Middleware для обработки исключений
    /// </summary>
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}