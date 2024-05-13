using AutoMapper;
using LojaVirtual.Colaboradores.Models;
using LojaVirtual.Colaboradores.Models.DTOs;

namespace LojaVirtual.Colaboradores.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Colaborador, ColaboradorDTO>().ReverseMap();
            CreateMap<Token, TokenDTO>().ReverseMap();
            CreateMap<Permissao, PermissaoDTO>().ReverseMap();
            CreateMap<Paginacao<Colaborador>, Paginacao<ColaboradorDTO>>().ReverseMap();
        }
    }
}
