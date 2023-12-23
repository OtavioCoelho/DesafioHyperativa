using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Repository.Extensions;
using DesafioHyperativa.Repository.Mapping.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioHyperativa.Repository.Mapping;
public class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Email)
            .HasEncryptedConversion()
            .IsRequired();

        builder.Property(x => x.Password)
            .HasEncryptedConversion()
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
