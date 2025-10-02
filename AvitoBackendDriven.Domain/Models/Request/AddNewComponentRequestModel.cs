using System.ComponentModel.DataAnnotations;
using AvitoBackendDriven.Domain.Enums;

namespace AvitoBackendDriven.Domain.Models.Request;

/// <summary>
/// Запросовая модель на создание нового компонента
/// </summary>
public class AddNewComponentRequestModel
{
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
    /// Общие свойства компонента
    /// </summary>
    public required string DefaultProperties { get; set; }
    
    /// <summary>
    /// Доступен с
    /// </summary>
    public DateTime? ValidFrom { get; set; }
    
    /// <summary>
    /// Доступен по
    /// </summary>
    public DateTime? ValidTo { get; set; }
    
    /// <summary>
    /// Флаг активности компонента
    /// </summary>
    /// <remarks>
    /// Показывает, доступен ли компонент для отображения на клиенте
    /// </remarks>
    public bool IsActive { get; set; }
}