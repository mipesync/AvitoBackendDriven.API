using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using AvitoBackendDriven.API.Middlewares;
using AvitoBackendDriven.API.Services;
using AvitoBackendDriven.Application;
using AvitoBackendDriven.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
});

builder.Host.UseSerilog((_, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(configuration);
    
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.WriteTo.Debug();
});

var connectionString = configuration.GetConnectionString("PostgreSQL");

var infrastructureLogger = LoggerFactory.Create(loggingBuilder => 
    loggingBuilder.AddConsole()).CreateLogger("AvitoBackendDriven.Infrastructure.DependencyInjection");

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.WriteIndented = true;
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JsonSerializerOptions>>().Value);

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Properties";
});

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("SwaggerCors", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddInfrastructure(connectionString, infrastructureLogger);
builder.Services.AddApplication();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvitoBackendDriven API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "AvitoBackendDriven API Документация";
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("SwaggerCors");

app.UseExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapControllers();

app.Run();