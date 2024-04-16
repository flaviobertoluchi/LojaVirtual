using AutoMapper;
using LojaVirtual.ClienteAPI.Models;

namespace LojaVirtual.ClienteAPI.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}
