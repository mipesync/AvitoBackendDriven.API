using System.Text.Json.Serialization;

namespace AvitoBackendDriven.Domain.Models.Response;

/// <summary>
/// Ответная модель экрана
/// </summary>
public class ScreenResponse
{
    /// <summary>
    /// Название экрана
    /// </summary>
    public string ScreenName { get; set; } = null!;
    
    /// <summary>
    /// Состояния
    /// </summary>
    public string? States { get; set; }
    
    /// <summary>
    /// Свойства
    /// </summary>
    public string? Properties { get; set; }

    /// <summary>
    /// Тело, содержащее компоненты
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ComponentLookup? Root { get; set; }
}