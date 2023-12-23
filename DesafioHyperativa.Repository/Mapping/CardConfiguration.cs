using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Repository.Extensions;
using DesafioHyperativa.Repository.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioHyperativa.Repository.Mapping;
public class CardConfiguration : EntityConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.LineIdentifier)
            .HasEncryptedConversion();

        builder.Property(x => x.LotNumber)
            .HasEncryptedConversion();

        builder.Property(x => x.Number)
            .HasEncryptedConversion()
            .IsRequired();

        builder.Property(x => x.LotId)
            .HasConversion(new GuidToStringConverter());

        builder.Property(x => x.UserId)
            .HasConversion(new GuidToStringConverter());

        builder.HasOne(x => x.Lot)
            .WithMany(x => x.Cards)
            .HasForeignKey(x => x.LotId);

        builder.HasIndex(x => x.Number)
            .IsUnique();
    }
}
