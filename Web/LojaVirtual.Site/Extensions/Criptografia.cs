using System.Security.Cryptography;
using System.Text;

namespace LojaVirtual.Site.Extensions
{
    public static class Criptografia
    {
        public static string Criptografar(string texto, string chave)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(chave));
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new((Stream)cryptoStream))
                {
                    streamWriter.Write(texto);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string Descriptografar(string textoCriptografado, string chave)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(textoCriptografado);

            using Aes aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(chave));
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new((Stream)cryptoStream);
            return streamReader.ReadToEnd();
        }

    }
}
