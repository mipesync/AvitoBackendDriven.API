namespace AvitoBackendDriven.Domain.Models.Response;

/// <summary>
/// Ответная модель компонента
/// </summary>
public class ComponentResponse
{
    /// <summary>
    /// Имя компонента
    /// </summary>
    public required string ComponentName { get; set; }
    
    /// <summary>
    /// Свойства по умолчанию
    /// </summary>
    public required string DefaultProperties { get; set; }
    
    /// <summary>
    /// Тип компонента
    /// </summary>
    public required string Kind { get; set; }
}