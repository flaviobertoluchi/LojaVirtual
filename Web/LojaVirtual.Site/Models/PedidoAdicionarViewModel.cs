using LojaVirtual.Site.Models.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.Models
{
    public class PedidoAdicionarViewModel
    {
        public Pedido Pedido { get; set; } = new();
        public List<SelectListItem> Email { get; set; } = [];
        public List<SelectListItem> Telefone { get; set; } = [];
        public List<SelectListItem> Endereco { get; set; } = [];

        public int EmailId { get; set; }
        public int TelefoneId { get; set; }
        public int EnderecoId { get; set; }

        public ClienteViewModel ClienteViewModel { get; set; } = new();
        public bool PedidoAlterado { get; set; }
    }
}
