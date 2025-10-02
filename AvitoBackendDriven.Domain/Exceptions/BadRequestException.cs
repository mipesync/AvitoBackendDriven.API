namespace AvitoBackendDriven.Domain.Exceptions;

/// <summary>
/// Исключение некорректного запроса
/// </summary>
public class BadRequestException : Exception
{
    public BadRequestException(string? message, Exception? innerException = null): base(message ?? "Некорректный запрос", innerException) {}
}