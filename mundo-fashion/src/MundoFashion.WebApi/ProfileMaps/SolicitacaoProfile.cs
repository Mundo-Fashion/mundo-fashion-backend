using AutoMapper;
using MundoFashion.Core.Utils;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models;
using System;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class SolicitacaoProfile : Profile
    {
        public SolicitacaoProfile()
        {
            CreateMap<Solicitacao, SolicitacaoModel>()
                .ForMember(s => s.Status, s => s.MapFrom(x => EnumUtils.ObterValorEmTexto(x.Status)))
                .ForMember(s => s.NomeUsuario, s => s.MapFrom(x => x.Usuario.Nome))
                .ForMember(s => s.NomeEmpresa, s => s.MapFrom(x => x.Empresa.Nome))
                .ForMember(s => s.IsEmpresa, s => s.MapFrom(x => !x.EmpresaId.Equals(Guid.Empty)));

            CreateMap<Solicitacao, SolicitacaoModel>()
                .ForMember(s => s.Status, s => s.MapFrom(x => EnumUtils.ObterValorEmTexto(x.Status)))
                .ForMember(s => s.NomeUsuario, s => s.MapFrom(x => x.Usuario.Nome))
                .ForMember(s => s.NomeEmpresa, s => s.MapFrom(x => x.Empresa.Nome))
                .ForMember(s => s.IsEmpresa, s => s.MapFrom(x => !x.EmpresaId.Equals(Guid.Empty)))
                .ReverseMap();

            CreateMap<SolicitacaoModel, Solicitacao>()
                .ConstructUsing((src, res) =>
                {
                    return new Solicitacao(res.Mapper.Map<DetalhesSolicitacao>(src.Detalhes));
                });
                
        }
    }
}
