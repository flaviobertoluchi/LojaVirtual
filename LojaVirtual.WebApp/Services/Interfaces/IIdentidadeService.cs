namespace LojaVirtual.WebApp.Services.Interfaces
{
    public interface IIdentidadeService
    {
        Task<ResponseApi> Entrar(string login, string senha);
    }
}
