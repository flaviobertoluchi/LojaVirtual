using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<ResponseApi> ObterTodos();
    }
}
