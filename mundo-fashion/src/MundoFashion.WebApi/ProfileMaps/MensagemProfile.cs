using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class MensagemProfile : Profile
    {
        public MensagemProfile()
        {
            CreateMap<MensagemModel, Mensagem>();
            CreateMap<MensagemModel, Mensagem>().ReverseMap();
        }
    }
}
