using AutoMapper;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ForMember(y => y.Imagens, x => x.Ignore());
            CreateMap<ProdutoViewModel, Produto>().ForMember(y => y.Imagens, x => x.MapFrom(x => x.Imagens!.Select(x => x.FileName)));
            CreateMap<Paginacao<Categoria>, Paginacao<CategoriaViewModel>>().ReverseMap();
            CreateMap<Paginacao<Produto>, Paginacao<ProdutoViewModel>>().ReverseMap();
            CreateMap<Carrinho, CarrinhoViewModel>().ReverseMap();
            CreateMap<CarrinhoItem, CarrinhoItemViewModel>().ReverseMap();
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Email, EmailViewModel>().ReverseMap();
            CreateMap<Telefone, TelefoneViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }
}
