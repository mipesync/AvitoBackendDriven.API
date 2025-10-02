using AvitoBackendDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvitoBackendDriven.Infrastructure.EntityConfigurations;

public class ExperimentConfiguration : IEntityTypeConfiguration<Experiment>
{
    public void Configure(EntityTypeBuilder<Experiment> builder)
    {
        builder.ToTable("Experiments");
        
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();
        
        builder.Property(a => a.Name).HasMaxLength(32);
        builder.Property(a => a.Description).HasMaxLength(250);
    }
}