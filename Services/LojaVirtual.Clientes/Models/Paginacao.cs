namespace LojaVirtual.Clientes.Models
{
    public class Paginacao<T>
    {
        public ICollection<T> Data { get; set; } = [];
        public PaginacaoInfo Info { get; set; } = new();
    }

    public class PaginacaoInfo
    {
        public int TotalItens { get; set; }
        public int QtdPorPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int? PaginaAnterior { get; set; }
        public int? PaginaProxima { get; set; }
    }
}
