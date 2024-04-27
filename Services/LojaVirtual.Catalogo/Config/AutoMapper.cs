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
            CreateMap<ProdutoDTO, Produto>().ForMember(y => y.Imagens, x => x.Ignore());
            CreateMap<Produto, ProdutoDTO>().ForMember(y => y.Imagens, x => x.MapFrom(x => x.Imagens.Select(x => x.Local)));
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Paginacao<Produto>, Paginacao<ProdutoDTO>>().ReverseMap();
            CreateMap<Paginacao<Categoria>, Paginacao<CategoriaDTO>>().ReverseMap();
        }
    }
}
