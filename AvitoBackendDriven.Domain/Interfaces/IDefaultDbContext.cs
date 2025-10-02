using AvitoBackendDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvitoBackendDriven.Domain.Interfaces;

/// <summary>
/// Описание контекста базы данных
/// </summary>
public interface IDefaultDbContext
{
    /// <summary>
    /// Таблица UI компонентов
    /// </summary>
    DbSet<Component> Components { get; set; }
    
    /// <summary>
    /// Таблица UI экранов
    /// </summary>
    DbSet<Screen> Screens { get; set; }
    
    /// <summary>
    /// Таблица композиций экранов
    /// </summary>
    DbSet<Composition> Compositions { get; set; }
    
    /// <summary>
    /// Таблица экспериментов
    /// </summary>
    DbSet<Experiment> Experiments { get; set; }
    
    /// <summary>
    /// Таблица истории изменений
    /// </summary>
    DbSet<ChangeHistory> UiChangeHistory { get; set; }
    
    /// <summary>
    /// Сохранить изменения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}