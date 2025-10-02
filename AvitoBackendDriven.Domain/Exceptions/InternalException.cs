namespace AvitoBackendDriven.Domain.Exceptions;

/// <summary>
/// Внутреннее исключение
/// </summary>
public class InternalException : Exception
{
    /// <summary>
    /// Инициализация внутреннего исключения
    /// </summary>
    /// <param name="message">Текст исключения</param>
    /// <param name="innerException">Внутреннее исключение</param>
    public InternalException(string message, Exception? innerException = null): base(message, innerException) {}
}