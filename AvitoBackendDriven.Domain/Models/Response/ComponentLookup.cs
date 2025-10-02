using System.Text.Json.Serialization;

namespace AvitoBackendDriven.Domain.Models.Response;

/// <summary>
/// Ответная модель компонента
/// </summary>
public class ComponentLookup
{
    /// <summary>
    /// Тип компонента
    /// </summary>
    public string Kind { get; set; }
    
    // /// <summary>
    // /// Базовые свойства компонента
    // /// </summary>
    // public required string DefaultProperties { get; set; }
    
    /// <summary>
    /// Специфичные свойства компонента
    /// </summary>
    public string Properties { get; set; }
    
    /// <summary>
    /// Тело, содержащее дочерние компоненты
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ComponentLookup>? Body { get; set; }
}