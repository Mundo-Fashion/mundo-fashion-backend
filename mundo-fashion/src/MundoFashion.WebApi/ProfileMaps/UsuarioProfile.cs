﻿using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Models;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {          
            CreateMap<Usuario, UsuarioModel>().ReverseMap();

            CreateMap<Usuario, UsuarioModel>()
                .AfterMap((src, dest, context) => dest.Servico = context.Mapper.Map<ServicoEstampa, ServicoEstampaModel>(src.Servico));
        }
    }
}
