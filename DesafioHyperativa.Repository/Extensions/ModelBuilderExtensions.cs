using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioHyperativa.Repository.Extensions;
public static class ModelBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasEncryptedConversion<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        var converter = CryptoHelper<TProperty>.GetCryptoConverter();
        return propertyBuilder
            .HasConversion(converter);
    }
}
