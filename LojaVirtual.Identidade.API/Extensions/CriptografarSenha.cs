using System.Security.Cryptography;
using System.Text;

namespace LojaVirtual.Identidade.API.Extensions
{
    public static class CriptografarSenha
    {
        public static string Criptografar(string senha)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha + "a3@d873g@hd01@9dg"));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
