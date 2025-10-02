namespace AvitoBackendDriven.Domain.Models.Request;

/// <summary>
/// Запросовая модель на создание композиции экрана
/// </summary>
public class AddNewCompositionRequestModel
{
    /// <summary>
    /// Название экрана
    /// </summary>
    public required string ScreenName { get; set; }

    /// <summary>
    /// Название компонента
    /// </summary>
    public required string ComponentName { get; set; }

    /// <summary>
    /// Порядок отображения
    /// </summary>
    public required int OrderIndex { get; set; }

    /// <summary>
    /// Специфичный конфиг для компонента композиции
    /// </summary>
    public required string Properties { get; set; }
    
    /// <summary>
    /// Идентификатор родительской композиции
    /// </summary>
    public int? ParentCompositionId { get; set; }
}