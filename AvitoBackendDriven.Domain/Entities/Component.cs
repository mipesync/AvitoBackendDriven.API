using System.ComponentModel.DataAnnotations;
using AvitoBackendDriven.Domain.Enums;
using Newtonsoft.Json.Linq;

namespace AvitoBackendDriven.Domain.Entities;

/// <summary>
/// Компонент UI
/// </summary>
public class Component
{
    /// <summary>
    /// Идентификатор компонента
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Уникальное имя компонента
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// Тип компонента
    /// </summary>
    public required ComponentType Kind { get; set; }

    /// <summary>
    /// Платформа отображения компонента
    /// </summary>
    public required Platforms Platform { get; set; }
    
    /// <summary>
    /// Версия для контроля совместимости 
    /// </summary>
    public double? Version { get; set; }

    /// <summary>
    /// Схема для валидации конфига
    /// </summary>
    public required JObject DefaultProperties { get; set; }
    
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
    /// Доступен с
    /// </summary>
    public DateTime? ValidFrom { get; set; }
    
    /// <summary>
    /// Доступен по
    /// </summary>
    public DateTime? ValidTo { get; set; }
    
    #region Navigation Properties
    
    /// <summary>
    /// Список изменений экрана
    /// </summary>
    public List<ChangeHistory>? ChangeHistories { get; set; }
    
    #endregion
}