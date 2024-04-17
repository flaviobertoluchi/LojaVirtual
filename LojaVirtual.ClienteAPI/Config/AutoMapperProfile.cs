using AutoMapper;
using LojaVirtual.ClienteAPI.Models;
using LojaVirtual.ClienteAPI.Models.DTOs;

namespace LojaVirtual.ClienteAPI.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Email, EmailDTO>().ReverseMap();
            CreateMap<Telefone, TelefoneDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Token, TokenDTO>().ReverseMap();
        }
    }
}
