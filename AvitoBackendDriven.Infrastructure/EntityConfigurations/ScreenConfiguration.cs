using AvitoBackendDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvitoBackendDriven.Infrastructure.EntityConfigurations;

public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
{
    public void Configure(EntityTypeBuilder<Screen> builder)
    {
        builder.ToTable("Screens");
        
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.Id).IsUnique();
        builder.HasIndex(a => a.Name).IsUnique();
        
        builder.Property(a => a.Name).HasMaxLength(50);
    }
}