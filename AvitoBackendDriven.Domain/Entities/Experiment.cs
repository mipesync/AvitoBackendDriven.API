using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using AvitoBackendDriven.Domain.Enums;
using Newtonsoft.Json.Linq;

namespace AvitoBackendDriven.Domain.Entities;

/// <summary>
/// Эксперимент
/// </summary>
public class Experiment
{
    /// <summary>
    /// Идентификатор эксперимента
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название эксперимента
    /// </summary>
    [MaxLength(32)]
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание эксперимента
    /// </summary>
    [MaxLength(250)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Платформа отображения эксперимента
    /// </summary>
    public required List<Platforms> Platforms { get; set; }
    
    /// <summary>
    /// Минимальная поддерживаемая версия
    /// </summary>
    public double? MinVersion { get; set; }
    
    /// <summary>
    /// Максимальная поддерживаемая версия
    /// </summary>
    public double? MaxVersion { get; set; }
    
    /// <summary>
    /// Процент трафика
    /// </summary>
    public double TrafficPercentage { get; set; }
    
    /// <summary>
    /// Варианты эксперимента
    /// </summary>
    public required JObject Buckets { get; set; }
    
    /// <summary>
    /// Флаг активности компонента
    /// </summary>
    /// <remarks>
    /// Показывает, доступен ли компонент для отображения на клиенте
    /// </remarks>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Дата начала эксперимента
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Дата окончания эксперимента
    /// </summary>
    public DateTime? EndDate { get; set; }
}