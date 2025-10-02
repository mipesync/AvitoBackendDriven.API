using AvitoBackendDriven.Domain.Interfaces;
using AvitoBackendDriven.Domain.Models;
using AvitoBackendDriven.Domain.Models.Request;
using AvitoBackendDriven.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AvitoBackendDriven.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
[Produces("application/json")]
[ApiVersion("1.0")]
public class ScreenController : Controller
{
    private readonly IUiService _uiService;

    public ScreenController(IUiService uiService)
    {
        _uiService = uiService;
    }
    
    /// <summary>
    /// Получить конфигурацию экрана
    /// </summary>
    /// <param name="requestDto">Входные данные</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="404">Экран не найден</response>
    /// <response code="500">Внутренняя ошибка</response>
    /// <returns><see cref="ScreenResponse"/></returns>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(ScreenResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpGet("screens/details")]
    public async Task<IActionResult> GetScreenConfigurationAsync([FromQuery] GetScreenRequestModel requestDto)
    {
        var response = await _uiService.GetScreenAsync(requestDto);
        return Ok(response);
    }

    /// <summary>
    /// Получить список доступных экранов
    /// </summary>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="500">Внутренняя ошибка</response>
    /// <returns><see cref="ScreenResponse"/></returns>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<ScreenResponse>))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpGet("screens")]
    public async Task<IActionResult> GetScreensAsync()
    {
        var response = await _uiService.GetScreensAsync();
        return Ok(response);
    }
    
    /// <summary>
    /// Добавить новый экран
    /// </summary>
    /// <param name="requestDto">Входные данные</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="400">Экран уже существует</response>
    /// <response code="500">Внутренняя ошибка</response>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpPost("screens/create")]
    public async Task<IActionResult> AddNewScreenAsync([FromBody] AddNewScreenRequestModel requestDto)
    {
        await _uiService.AddNewScreenAsync(requestDto);
        return Ok();
    }
    
    /// <summary>
    /// Обновить экран
    /// </summary>
    /// <param name="requestDto">Входные данные</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="404">Экран не найден</response>
    /// <response code="500">Внутренняя ошибка</response>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpPut("screens/update")]
    public async Task<IActionResult> UpdateScreenAsync([FromBody] UpdateScreenRequestModel requestDto)
    {
        await _uiService.UpdateScreenAsync(requestDto);
        return Ok();
    }

    /// <summary>
    /// Получить список доступных компонентов
    /// </summary>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="500">Внутренняя ошибка</response>
    /// <returns><see cref="ComponentResponse"/></returns>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<ComponentResponse>))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpGet("components")]
    public async Task<IActionResult> GetComponentsAsync()
    {
        var response = await _uiService.GetComponentsAsync();
        return Ok(response);
    }
    
    /// <summary>
    /// Добавить новый компонент
    /// </summary>
    /// <param name="requestDto">Входные данные</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="400">Компонент уже существует</response>
    /// <response code="500">Внутренняя ошибка</response>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpPost("components/create")]
    public async Task<IActionResult> AddNewComponentAsync([FromBody] AddNewComponentRequestModel requestDto)
    {
        await _uiService.AddNewComponentAsync(requestDto);
        return Ok();
    }
    
    /// <summary>
    /// Обновить компонент
    /// </summary>
    /// <param name="requestDto">Входные данные</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="404">Компонент не найден</response>
    /// <response code="500">Внутренняя ошибка</response>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpPut("components/update")]
    public async Task<IActionResult> UpdateComponentAsync([FromBody] UpdateComponentRequestModel requestDto)
    {
        await _uiService.UpdateComponentAsync(requestDto);
        return Ok();
    }
    
    /// <summary>
    /// Добавить новую композицию экрана
    /// </summary>
    /// <param name="requestDto">Входные данные</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="404">Экран не найден</response>
    /// <response code="404">Компонент не найден</response>
    /// <response code="500">Внутренняя ошибка</response>
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    [HttpPost("composition/create")]
    public async Task<IActionResult> AddNewCompositionAsync([FromBody] AddNewCompositionRequestModel requestDto)
    {
        await _uiService.AddNewCompositionAsync(requestDto);
        return Ok();
    }
}