namespace AvitoBackendDriven.Domain.Models.Request;

/// <summary>
/// Запросовая модель на обновление компонента
/// </summary>
public class UpdateComponentRequestModel : AddNewComponentRequestModel
{
    /// <summary>
    /// Новое имя компонента
    /// </summary>
    public string? NewName { get; set; }
}