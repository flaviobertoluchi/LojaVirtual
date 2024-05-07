using AutoMapper;
using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.DTOs;

namespace LojaVirtual.Pedidos.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<PedidoItem, PedidoItemDTO>();
            CreateMap<SituacaoPedido, SituacaoPedidoDTO>();
            CreateMap<Cliente, ClienteDTO>();
        }
    }
}
