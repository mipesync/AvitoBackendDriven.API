using System.Text.Json.Nodes;
using AvitoBackendDriven.Domain.Enums;

namespace AvitoBackendDriven.Domain.Entities;

/// <summary>
/// История изменений UI
/// </summary>
public class ChangeHistory
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
    /// Тип изменения
    /// </summary>
    public ChangeType ChangeType { get; set; }
    
    /// <summary>
    /// Разница в изменениях
    /// </summary>
    public required string ChangeData { get; set; }
    
    #region Entities

    /// <summary>
    /// Объект связанного экрана
    /// </summary>
    public Screen Screen { get; set; } = null!;
    
    /// <summary>
    /// Объект связанного компонента
    /// </summary>
    public Component Component { get; set; } = null!;

    #endregion
}