using System.Security.Cryptography;
using System.Text;
using AvitoBackendDriven.Domain.Entities;
using AvitoBackendDriven.Domain.Enums;
using AvitoBackendDriven.Domain.Exceptions;
using AvitoBackendDriven.Domain.Interfaces;
using AvitoBackendDriven.Domain.Models.Request;
using AvitoBackendDriven.Domain.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace AvitoBackendDriven.Application.Services;

public class UiService : IUiService
{
    private readonly ILogger<UiService> _logger;
    private readonly IDefaultDbContext _dbContext;

    public UiService(ILogger<UiService> logger, IDefaultDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ScreenResponse> GetScreenAsync(GetScreenRequestModel request)
    {
        var screen = await _dbContext.Screens
            .Include(e => e.Components)
            .FirstOrDefaultAsync(e => e.Name == request.ScreenName 
                && e.Platform == request.Platform && e.IsActive && e.MinVersion >= request.AppVersion);
        if (screen == null)
            throw new NotFoundException("Экран не найден");
        
        List<Component> validComponents = []; 
        
        var compositions = await _dbContext.Compositions.Where(e => e.ScreenId == screen.Id)
            .OrderBy(e => e.OrderIndex).ToListAsync();
        if (compositions.Count != 0)
        {
            var compositionComponents = await _dbContext.Components.Where(e => compositions
                .Select(composition => composition.ComponentId).Contains(e.Id)).ToListAsync();
            
            var now = DateTime.UtcNow;
            validComponents = compositionComponents
                .Where(e => e.IsActive)
                .Where(e => e.ValidFrom == null || e.ValidFrom <= now)
                .Where(e => e.ValidTo == null || e.ValidTo >= now)
                .Select(e => e)
                .ToList();
        }

        var response = new ScreenResponse
        {
            ScreenName = request.ScreenName,
            States = screen.States?.ToString(),
            Properties = screen.Properties?.ToString()
        };
        response.Root = StructureComponentHierarchy(validComponents, compositions);

        return response;
    }

    public async Task<List<ScreenResponse>> GetScreensAsync()
    {
        var screens = await _dbContext.Screens.ToListAsync();
        
        List<ScreenResponse> screenResponses = [];
        screenResponses.AddRange(screens.Select(screen => new ScreenResponse
        {
            ScreenName = screen.Name, 
            States = screen.States?.ToString(), 
            Properties = screen.Properties?.ToString()
        }));

        return screenResponses;
    }


    public async Task AddNewScreenAsync(AddNewScreenRequestModel request)
    {
        var existScreen = await _dbContext.Screens.FirstOrDefaultAsync(e => e.Name == request.Name);
        if (existScreen is not null)
            throw new BadRequestException($"Экран с именем '{request.Name}' уже существует");

        var newScreen = new Screen
        {
            Name = request.Name,
            Platform = request.Platform,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            MinVersion = request.MinVersion
        };
        
        if (request.Properties is not null)
            newScreen.Properties = JObject.Parse(request.Properties);
        if (request.States is not null)
            newScreen.States = JObject.Parse(request.States);
        
        await _dbContext.Screens.AddAsync(newScreen);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateScreenAsync(UpdateScreenRequestModel request)
    {
        var existScreen = await _dbContext.Screens.FirstOrDefaultAsync(e => e.Name == request.Name);
        if (existScreen is null)
            throw new NotFoundException($"Экран с именем '{request.Name}' не найден");
        
        existScreen.Name = request.NewName ?? request.Name;
        existScreen.Platform = request.Platform;
        existScreen.IsActive = request.IsActive;
        existScreen.MinVersion = request.MinVersion;
        if (request.States is not null)
            existScreen.States = JObject.Parse(request.States);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ComponentResponse>> GetComponentsAsync()
    {
        var components = await _dbContext.Components.ToListAsync();
        
        List<ComponentResponse> componentsResponses = [];
        componentsResponses.AddRange(components.Select(component => new ComponentResponse
        {
            ComponentName = component.Name,
            DefaultProperties = component.DefaultProperties.ToString(),
            Kind = component.Kind.ToString().ToLower(),
        }));

        return componentsResponses;
    }

    public async Task AddNewComponentAsync(AddNewComponentRequestModel request)
    {
        var existComponent = await _dbContext.Components.FirstOrDefaultAsync(e => e.Name == request.Name);
        if (existComponent is not null)
            throw new BadRequestException($"Экран с именем '{request.Name}' уже существует");
        
        var newComponent = new Component
        {
            Name = request.Name,
            Platform = request.Platform,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            DefaultProperties = JObject.Parse(request.DefaultProperties),
            Kind = request.Kind,
            Version = request.Version,
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo
        };
        
        await _dbContext.Components.AddAsync(newComponent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateComponentAsync(UpdateComponentRequestModel request)
    {
        var existComponent = await _dbContext.Components.FirstOrDefaultAsync(e => e.Name == request.Name);
        if (existComponent is null)
            throw new NotFoundException($"Компонент с именем '{request.Name}' не найден");
        
        existComponent.Name = request.NewName ?? request.Name;
        existComponent.Platform = request.Platform;
        existComponent.IsActive = request.IsActive;
        existComponent.Version = request.Version;
        existComponent.DefaultProperties = JObject.Parse(request.DefaultProperties);
        existComponent.Kind = request.Kind;
        existComponent.ValidFrom = request.ValidFrom;
        existComponent.ValidTo = request.ValidTo;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddNewCompositionAsync(AddNewCompositionRequestModel request)
    {
        var existScreen = await _dbContext.Screens.FirstOrDefaultAsync(e => e.Name == request.ScreenName);
        if (existScreen is null)
            throw new NotFoundException($"Экран с именем '{request.ScreenName}' не найден");
        
        var existComponent = await _dbContext.Components.FirstOrDefaultAsync(e => e.Name == request.ComponentName);
        if (existComponent is null)
            throw new NotFoundException($"Компонент с именем '{request.ComponentName}' не найден");

        var newComposition = new Composition
        {
            ScreenId = existScreen.Id,
            ComponentId = existComponent.Id,
            OrderIndex = request.OrderIndex,
            Properties = JObject.Parse(request.Properties),
            ParentCompositionId = request.ParentCompositionId
        };
        
        await _dbContext.Compositions.AddAsync(newComposition);
        await _dbContext.SaveChangesAsync();
    }

    private ComponentLookup StructureComponentHierarchy(List<Component> components, 
        List<Composition> compositions, Component? component = null)
    {
        component ??= components[0];
        
        var currentComposition = compositions.First(e => e.ComponentId == component.Id);
        var componentResponse = new ComponentLookup
        {
            Kind = component.Kind.ToString().ToLower(),
            Properties = currentComposition.Properties.ToString()
        };
        var childCompositions = compositions
            .Where(c => c.ParentCompositionId == currentComposition.Id)
            .OrderBy(c => c.OrderIndex)
            .ToList();

        if (childCompositions.Count != 0)
        {
            var childComponents = childCompositions
                .Select(childComp => 
                {
                    var childComponent = components.First(c => c.Id == childComp.ComponentId);
                    return StructureComponentHierarchy(components, compositions, childComponent);
                })
                .ToList();
            componentResponse.Body ??= [];
            componentResponse.Body.AddRange(childComponents);
        }
        
        return componentResponse;
    }
}