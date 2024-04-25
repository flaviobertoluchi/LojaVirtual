using System.Security.Cryptography;
using System.Text;

namespace LojaVirtual.Colaboradores.Extensions
{
    public static class CriptografarSHA256
    {
        public static string Criptografar(string senha)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha + "Ahd@hd@lk@Kd@DdaD"));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
