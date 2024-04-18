using AutoMapper;
using LojaVirtual.Produtos.Models;
using LojaVirtual.Produtos.Models.DTOs;

namespace LojaVirtual.Catalogo.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
