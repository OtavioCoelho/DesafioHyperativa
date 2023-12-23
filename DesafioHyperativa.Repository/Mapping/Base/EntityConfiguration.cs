using DesafioHyperativa.Domain.Entities.Base;
using DesafioHyperativa.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioHyperativa.Repository.Mapping.Base;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id)
            .HasAnnotation("Sqlite:Autoincrement", true);

        builder.Property(x => x.Id)
            .HasConversion(new GuidToStringConverter())
            .HasColumnOrder(1);

        builder.Property(x => x.DtRegister)
            .HasColumnOrder(2)
            .HasEncryptedConversion()
            .IsRequired();
        builder.Property(x => x.DtUpdate)
            .HasColumnOrder(2)
            .HasEncryptedConversion()
            .IsRequired();
    }
}
