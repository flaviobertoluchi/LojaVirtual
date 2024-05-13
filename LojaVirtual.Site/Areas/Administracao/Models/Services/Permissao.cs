using System.ComponentModel;

namespace LojaVirtual.Site.Areas.Administracao.Models.Services
{
    public class Permissao
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }

        [DisplayName("Visualizar Colaborador")]
        public bool VisualizarColaborador { get; set; }

        [DisplayName("Adicionar Colaborador")]
        public bool AdicionarColaborador { get; set; }

        [DisplayName("Editar Colaborador")]
        public bool EditarColaborador { get; set; }

        [DisplayName("Excluir Colaborador")]
        public bool ExcluirColaborador { get; set; }

        [DisplayName("Visualizar Cliente")]
        public bool VisualizarCliente { get; set; }

        [DisplayName("Visualizar Categoria")]
        public bool VisualizarCategoria { get; set; }

        [DisplayName("Adicionar Categoria")]
        public bool AdicionarCategoria { get; set; }

        [DisplayName("Editar Categoria")]
        public bool EditarCategoria { get; set; }

        [DisplayName("Excluir Categoria")]
        public bool ExcluirCategoria { get; set; }

        [DisplayName("Visualizar Produto")]
        public bool VisualizarProduto { get; set; }

        [DisplayName("Adicionar Produto")]
        public bool AdicionarProduto { get; set; }

        [DisplayName("Editar Produto")]
        public bool EditarProduto { get; set; }

        [DisplayName("Excluir Produto")]
        public bool ExcluirProduto { get; set; }

        [DisplayName("Vizualizar Pedido")]
        public bool VizualizarPedido { get; set; }

        [DisplayName("Adicionar Situação Pedido")]
        public bool AdicionarSituacaoPedido { get; set; }
    }
}
