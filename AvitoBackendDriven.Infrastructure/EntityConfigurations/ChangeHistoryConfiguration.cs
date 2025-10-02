using AvitoBackendDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvitoBackendDriven.Infrastructure.EntityConfigurations;

public class ChangeHistoryConfiguration : IEntityTypeConfiguration<ChangeHistory>
{
    public void Configure(EntityTypeBuilder<ChangeHistory> builder)
    {
        builder.ToTable("UiChangeHistories");
        
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();

        #region OneToMany

        builder.HasOne(e => e.Screen)
            .WithMany(e => e.ChangeHistories)
            .HasForeignKey(e => e.ScreenId);
        
        builder.HasOne(e => e.Component)
            .WithMany(e => e.ChangeHistories)
            .HasForeignKey(e => e.ComponentId);
        
        #endregion
    }
}