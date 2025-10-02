namespace AvitoBackendDriven.Domain.Models.Request;

/// <summary>
/// Запросовая модель на обновление экрана
/// </summary>
public class UpdateScreenRequestModel : AddNewScreenRequestModel
{
    /// <summary>
    /// Новое имя экрана
    /// </summary>
    /// <remarks>
    /// Если не указывать, то остаётся прежнее название
    /// </remarks>
    public string? NewName { get; set; } 
}