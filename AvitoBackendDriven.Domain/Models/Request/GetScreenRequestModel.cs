using AvitoBackendDriven.Domain.Enums;

namespace AvitoBackendDriven.Domain.Models.Request;

/// <summary>
/// Запросовая модель экрана
/// </summary>
public class GetScreenRequestModel
{
    /// <summary>
    /// Название экрана
    /// </summary>
    public required string ScreenName { get; set; }
    
    /// <summary>
    /// Платформа
    /// </summary>
    public required Platforms Platform { get; set; }
    
    /// <summary>
    /// Версия приложения
    /// </summary>
    public required double AppVersion { get; set; }
}