using AvitoBackendDriven.Application.Services;
using AvitoBackendDriven.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AvitoBackendDriven.Application;

/// <summary>
/// Класс добавления сервисов BLL в IoC
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить сервисы слоя бизнес-логики
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов</param>
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUiService, UiService>();
    }
}