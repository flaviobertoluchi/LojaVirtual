using AutoMapper;
using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.DTOs;

namespace LojaVirtual.Pedidos.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<PedidoItem, PedidoItemDTO>().ReverseMap();
            CreateMap<SituacaoPedido, SituacaoPedidoDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Paginacao<Pedido>, Paginacao<PedidoDTO>>().ReverseMap();
        }
    }
}
