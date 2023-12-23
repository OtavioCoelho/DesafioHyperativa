using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DesafioHyperativa.Domain.Utility;
public static class Encryptor<TProperty>
{
    private const string _KEY = "0b821ff9a12d60dff32768e18d43940a9dd1116e6f6cc6d900a4cc1d2fa1963d";

    public static string Encrypt(TProperty data)
    {
        using (TripleDES tripleDes = TripleDES.Create())
        {
            tripleDes.Key = FixSizeKey(_KEY, tripleDes.KeySize / 8);
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = tripleDes.CreateEncryptor(tripleDes.Key, tripleDes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(data);
                    }
                }

                byte[] criptografado = msEncrypt.ToArray();
                return Convert.ToBase64String(criptografado);
            }
        }
    }

    public static TProperty Decrypt(string dataEncrypt)
    {
        byte[] bytesEncript = Convert.FromBase64String(dataEncrypt);

        using (TripleDES tripleDes = TripleDES.Create())
        {
            tripleDes.Key = FixSizeKey(_KEY, tripleDes.KeySize / 8);
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = tripleDes.CreateDecryptor(tripleDes.Key, tripleDes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(bytesEncript))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        var valueDecrypt = srDecrypt.ReadToEnd();
                        if (typeof(TProperty) == typeof(DateTime))
                        {
                            if (DateTime.TryParseExact(valueDecrypt, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultado))
                                return (TProperty)(object)resultado;
                        }
                        return (TProperty)Convert.ChangeType(valueDecrypt, typeof(TProperty));
                    }
                }
            }
        }
    }
    private static byte[] FixSizeKey(string key, int size)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        Array.Resize(ref keyBytes, size);
        return keyBytes;
    }
}
