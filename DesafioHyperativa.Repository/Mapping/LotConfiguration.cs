using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Repository.Extensions;
using DesafioHyperativa.Repository.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioHyperativa.Repository.Mapping;
public class LotConfiguration : EntityConfiguration<Lot>
{
    public override void Configure(EntityTypeBuilder<Lot> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasEncryptedConversion()
            .IsRequired();

        builder.Property(x => x.DateOperation)
            .HasEncryptedConversion()
            .IsRequired();

        builder.Property(x => x.LotNumber)
            .HasEncryptedConversion()
            .IsRequired();
        
        builder.Property(x => x.NumberRecords)
            .HasEncryptedConversion()
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasConversion(new GuidToStringConverter());

        builder.HasMany(x => x.Cards)
            .WithOne(x => x.Lot)
            .HasForeignKey(x => x.LotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.Name, x.DateOperation, x.LotNumber
        })
        .IsUnique();
    }
}
