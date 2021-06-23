using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MundoFashion.Core.Utils;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models;
using MundoFashion.WebApi.Models.Mensagem;
using MundoFashion.WebApi.Models.Solicitacao;
using MundoFashion.WebApi.Models.Usuario;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class SolicitacaoProfile : Profile
    {
        public SolicitacaoProfile()
        {
            CreateMap<Solicitacao, SolicitacaoModel>()
                .ForMember(s => s.Status, s => s.MapFrom(x => EnumUtils.ObterValorEmTexto(x.Status)))    
                .AfterMap((src, dest, context) => dest.Tomador = context.Mapper.Map<Usuario, PrestadorTomadorModel>(src.Tomador))
                .AfterMap((src, dest, context) => dest.Mensagens = context.Mapper.Map<List<Mensagem>, List<MensagemModel>>(src.Mensagens.OrderByDescending(x => x.CreatedAt).ToList()));                
                        
        }
    }
}
