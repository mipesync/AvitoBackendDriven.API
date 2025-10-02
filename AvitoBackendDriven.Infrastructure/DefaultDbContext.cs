using System.Reflection;
using AvitoBackendDriven.Domain.Entities;
using AvitoBackendDriven.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AvitoBackendDriven.Infrastructure;

/// <summary>
/// Класс контекста базы данных
/// </summary>
public class DefaultDbContext : DbContext, IDefaultDbContext
{
    /// <summary>
    /// Конструктор, инициализирующий первоначальные настройки контекста
    /// </summary>
    /// <param name="options">Первоначальные настройки</param>
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options): base(options) { }
    
    public DbSet<Component> Components { get; set; }
    public DbSet<Screen> Screens { get; set; }
    public DbSet<Composition> Compositions { get; set; }
    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<ChangeHistory> UiChangeHistory { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<JObject>()
            .HaveConversion<JObjectToStringConverter>()
            .HaveColumnType("jsonb");

        configurationBuilder.Properties<JArray>()
            .HaveConversion<JArrayToStringConverter>()
            .HaveColumnType("jsonb");

        configurationBuilder.Properties<JToken>()
            .HaveConversion<JTokenToStringConverter>()
            .HaveColumnType("jsonb");
    }


    private sealed class JObjectToStringConverter : ValueConverter<JObject, string>
    {
        public JObjectToStringConverter()
            : base(
                v => v.ToString(Formatting.None),
                v => string.IsNullOrWhiteSpace(v) ? new JObject() : JObject.Parse(v))
        {
        }
    }

    private sealed class JArrayToStringConverter : ValueConverter<JArray, string>
    {
        public JArrayToStringConverter()
            : base(
                v => v.ToString(Formatting.None),
                v => string.IsNullOrWhiteSpace(v) ? new JArray() : JArray.Parse(v))
        {
        }
    }

    private sealed class JTokenToStringConverter : ValueConverter<JToken, string>
    {
        public JTokenToStringConverter()
            : base(
                v => v.ToString(Formatting.None),
                v => string.IsNullOrWhiteSpace(v) ? JValue.CreateNull() : JToken.Parse(v))
        {
        }
    }
}