using AvitoBackendDriven.Domain.Extensions;
using AvitoBackendDriven.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AvitoBackendDriven.Infrastructure;

/// <summary>
/// Класс добавления сервисов инфраструктуры в IoC
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить сервисы инфраструктуры в IoC
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="connectionString">Строка подключения к базе данных</param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException">Строка подключения не указана</exception>
    public static void AddInfrastructure(this IServiceCollection serviceCollection, string? connectionString, 
        ILogger logger)
    {
        if (connectionString.IsNullOrEmpty())
        {
            var ex = new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty");
            
            logger.LogError(ex, ex.Message, args: DateTime.UtcNow);
            throw ex;
        }
        
        serviceCollection.AddScoped<IDefaultDbContext, DefaultDbContext>();
        serviceCollection.AddDbContext<DefaultDbContext>(options => options.UseNpgsql(connectionString)
            .UseLoggerFactory(LoggerFactory.Create(builder => 
            {
                builder.AddFilter((category, level) => 
                    !category!.Contains("Microsoft.EntityFrameworkCore") || level >= LogLevel.Warning);
            })));
    }
}