namespace LojaVirtual.Colaboradores.Models.DTOs
{
    public class PermissaoDTO
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public bool VisualizarColaborador { get; set; }
        public bool AdicionarColaborador { get; set; }
        public bool EditarColaborador { get; set; }
        public bool ExcluirColaborador { get; set; }
        public bool VisualizarCliente { get; set; }
        public bool VisualizarCategoria { get; set; }
        public bool AdicionarCategoria { get; set; }
        public bool EditarCategoria { get; set; }
        public bool ExcluirCategoria { get; set; }
        public bool VisualizarProduto { get; set; }
        public bool AdicionarProduto { get; set; }
        public bool EditarProduto { get; set; }
        public bool ExcluirProduto { get; set; }
        public bool VizualizarPedido { get; set; }
        public bool AdicionarSituacaoPedido { get; set; }
    }
}
