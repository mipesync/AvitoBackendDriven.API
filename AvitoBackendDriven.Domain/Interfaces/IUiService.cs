using AvitoBackendDriven.Domain.Models.Request;
using AvitoBackendDriven.Domain.Models.Response;

namespace AvitoBackendDriven.Domain.Interfaces;

/// <summary>
/// Описание методов UiService
/// </summary>
public interface IUiService
{
    /// <summary>
    /// Получить конфигурацию экрана
    /// </summary>
    /// <param name="request">Входные данные</param>
    /// <returns><see cref="ScreenResponse"/></returns>
    Task<ScreenResponse> GetScreenAsync(GetScreenRequestModel request);

    /// <summary>
    /// Получить список экранов
    /// </summary>
    /// <returns>Список экранов</returns>
    Task<List<ScreenResponse>> GetScreensAsync();

    /// <summary>
    /// Создать новый экран
    /// </summary>
    /// <param name="request">Входные данные</param>
    Task AddNewScreenAsync(AddNewScreenRequestModel request);

    /// <summary>
    /// Обновить экран
    /// </summary>
    /// <param name="request">Входные данные</param>
    Task UpdateScreenAsync(UpdateScreenRequestModel request);
    
    /// <summary>
    /// Получить список экранов
    /// </summary>
    /// <returns>Список экранов</returns>
    Task<List<ComponentResponse>> GetComponentsAsync();

    /// <summary>
    /// Создать новый компонент
    /// </summary>
    /// <param name="request">Входные данные</param>
    Task AddNewComponentAsync(AddNewComponentRequestModel request);
    
    /// <summary>
    /// Обновить компонент
    /// </summary>
    /// <param name="request">Входные данные</param>
    Task UpdateComponentAsync(UpdateComponentRequestModel request);

    /// <summary>
    /// Добавить новую композицию экрана
    /// </summary>
    /// <param name="request">Входные данные</param>
    Task AddNewCompositionAsync(AddNewCompositionRequestModel request);
}