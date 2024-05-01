namespace LojaVirtual.Site.Extensions
{
    public class Cookie(IHttpContextAccessor accessor)
    {
        private readonly IHttpContextAccessor accessor = accessor;
        private readonly string chaveCriptogafia = "87d1ghd@8ahdHdE@jkKDJ9876Djjda9i";

        public string? Obter(string key)
        {
            var cookie = accessor.HttpContext?.Request.Cookies[key];
            if (cookie is null) return null;

            return Criptografia.Descriptografar(cookie, chaveCriptogafia);
        }

        public void Adicionar(string key, string value, int validadeDias = 7, bool essencial = false)
        {
            var cookie = Obter(key);
            if (cookie is not null) Excluir(key);

            var options = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddDays(validadeDias),
                IsEssential = essencial
            };

            value = Criptografia.Criptografar(value, chaveCriptogafia);
            accessor.HttpContext?.Response.Cookies.Append(key, value, options);
        }

        public void Excluir(string key)
        {
            accessor.HttpContext?.Response.Cookies.Delete(key);
        }
    }
}
