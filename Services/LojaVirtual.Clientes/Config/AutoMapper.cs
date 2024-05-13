﻿using AutoMapper;
using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.DTOs;

namespace LojaVirtual.Clientes.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Email, EmailDTO>().ReverseMap();
            CreateMap<Telefone, TelefoneDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Token, TokenDTO>().ReverseMap();
            CreateMap<Paginacao<Cliente>, Paginacao<ClienteDTO>>().ReverseMap();
        }
    }
}
