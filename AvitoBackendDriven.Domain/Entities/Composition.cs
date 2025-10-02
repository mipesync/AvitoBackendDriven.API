using Newtonsoft.Json.Linq;

namespace AvitoBackendDriven.Domain.Entities;

/// <summary>
/// Композиция экрана и компонентов
/// </summary>
public class Composition
{
    /// <summary>
    /// Идентификатор композиции
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор экрана
    /// </summary>
    public int ScreenId { get; set; }
    
    /// <summary>
    /// Идентификатор компонента
    /// </summary>
    public int ComponentId { get; set; }
    
    /// <summary>
    /// Порядок отображения
    /// </summary>
    public int OrderIndex { get; set; }
    
    /// <summary>
    /// Свойства компонента на экране
    /// </summary>
    public required JObject Properties { get; set; }
    
    /// <summary>
    /// Идентификатор родительской композиции
    /// </summary>
    public int? ParentCompositionId { get; set; }
}