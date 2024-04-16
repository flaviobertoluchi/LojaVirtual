using AutoMapper;
using LojaVirtual.Identidade.API.Models;
using LojaVirtual.Identidade.API.Models.DTOs;

namespace LojaVirtual.Identidade.API.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
