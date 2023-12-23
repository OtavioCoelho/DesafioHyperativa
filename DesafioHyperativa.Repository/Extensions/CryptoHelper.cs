using DesafioHyperativa.Domain.Utility;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioHyperativa.Repository.Extensions;
public static class CryptoHelper<TProperty>
{
    public static ValueConverter<TProperty, string> GetCryptoConverter()
    {
        return new ValueConverter<TProperty, string>(
            v => Encryptor<TProperty>.Encrypt(v),
            v => Encryptor<TProperty>.Decrypt(v));
    }
}
