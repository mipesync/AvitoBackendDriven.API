namespace AvitoBackendDriven.Domain.Models;

/// <summary>
/// Модель HTTP ошибки
/// </summary>
public class ErrorModel
{
    /// <summary>
    /// HTTP статус код
    /// </summary>
    public int StatusCode { get; set; } = 0;
    
    /// <summary>
    /// Массив ошибок
    /// </summary>
    public List<string> Errors { get; set; } = [];
    
    /// <summary>
    /// Количество ошибок
    /// </summary>
    public int ErrorsCount { get; set; } = 0;
}