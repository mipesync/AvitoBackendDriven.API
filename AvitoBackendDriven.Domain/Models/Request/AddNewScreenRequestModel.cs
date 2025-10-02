using System.ComponentModel.DataAnnotations;
using AvitoBackendDriven.Domain.Enums;

namespace AvitoBackendDriven.Domain.Models.Request;

/// <summary>
/// Запросовая модель на создание нового экрана
/// </summary>
public class AddNewScreenRequestModel
{
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
    /// Свойства
    /// </summary>
    public string? Properties { get; set; }
    
    /// <summary>
    /// Состояния
    /// </summary>
    public string? States { get; set; }
}