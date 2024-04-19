﻿using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface IProdutoRepository
    {
        Task<long> TotalItens();
        Task<ICollection<Produto>> ObterPaginado(int pagina, int qtdPorPagina, bool incluirImagens = false);
        Task<Produto?> Obter(long id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Produto produto);
    }
}
