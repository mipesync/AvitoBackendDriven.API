using AvitoBackendDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvitoBackendDriven.Infrastructure.EntityConfigurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.ToTable("Components");
        
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();
        builder.HasIndex(e => e.Name).IsUnique();
        
        builder.Property(a => a.Name).HasMaxLength(50);
        
    }
}