using AutoMapper;
using LojaVirtual.Catalogo.Models;
using LojaVirtual.Produtos.Models;
using LojaVirtual.Produtos.Models.DTOs;

namespace LojaVirtual.Catalogo.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Produto, ProdutoDTO>().ForMember(y => y.Imagens, x => x.MapFrom(x => x.Imagens.Select(x => x.Local))).ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Paginacao<Produto>, Paginacao<ProdutoDTO>>().ReverseMap();
            CreateMap<Paginacao<Categoria>, Paginacao<CategoriaDTO>>().ReverseMap();
        }
    }
}
