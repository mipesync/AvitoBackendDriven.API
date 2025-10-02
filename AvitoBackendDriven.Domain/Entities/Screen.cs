using System.ComponentModel.DataAnnotations;
using AvitoBackendDriven.Domain.Enums;
using Newtonsoft.Json.Linq;

namespace AvitoBackendDriven.Domain.Entities;

/// <summary>
/// Экран UI
/// </summary>
public class Screen
{
    /// <summary>
    /// Идентификатор экрана
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Уникальное название экрана
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }
    
    /// <summary>
    /// Платформа отображения экрана
    /// </summary>
    public required Platforms Platform { get; set; }
    
    /// <summary>
    /// Минимальная поддерживаемая версия
    /// </summary>
    public double? MinVersion { get; set; }
    
    /// <summary>
    /// Флаг активности компонента
    /// </summary>
    /// <remarks>
    /// Показывает, доступен ли компонент для отображения на клиенте
    /// </remarks>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Дата создания компонента
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата обновления компонента
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Свойство
    /// </summary>
    public JObject? Properties { get; set; }
    
    /// <summary>
    /// Состояния
    /// </summary>
    public JObject? States { get; set; }
    
    #region Navigation Properties
    
    /// <summary>
    /// Список изменений экрана
    /// </summary>
    public List<ChangeHistory>? ChangeHistories { get; set; }
    
    /// <summary>
    /// Композиция экранов и компонентов
    /// </summary>
    public List<Composition>? ScreenComponents { get; set; }
    
    /// <summary>
    /// Список компонентов
    /// </summary>
    public List<Component>? Components { get; set; }
    
    #endregion
}