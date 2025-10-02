using AvitoBackendDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvitoBackendDriven.Infrastructure.EntityConfigurations;

public class CompositionConfiguration : IEntityTypeConfiguration<Composition>
{
    public void Configure(EntityTypeBuilder<Composition> builder)
    {
        builder.ToTable("Compositions");
        
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();
    }
}