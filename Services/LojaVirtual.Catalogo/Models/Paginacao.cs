namespace LojaVirtual.Catalogo.Models
{
    public class Paginacao<T>
    {
        public ICollection<T> Data { get; set; } = [];
        public PaginacaoInfo Info { get; set; } = new();
    }

    public class PaginacaoInfo
    {
        public long TotalItens { get; set; }
        public int QtdPorPagina { get; set; }
        public long TotalPaginas { get; set; }
        public long PaginaAtual { get; set; }
        public long? PaginaAnterior { get; set; }
        public long? PaginaProxima { get; set; }
    }
}
