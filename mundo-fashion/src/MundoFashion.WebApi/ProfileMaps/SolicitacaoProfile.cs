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
                .ForMember(s => s.IsEmpresa, s => s.MapFrom(x => x.EmpresaId.HasValue && !x.EmpresaId.Equals(Guid.Empty)))
                .ForMember(s => s.PrestadorServicoId, s => s.MapFrom(x => x.Servico.ObterIdPrestador()));

            CreateMap<Solicitacao, SolicitacaoModel>()
                .ForMember(s => s.Status, s => s.MapFrom(x => EnumUtils.ObterValorEmTexto(x.Status)))
                .ForMember(s => s.NomeUsuario, s => s.MapFrom(x => x.Servico.Usuario.Nome))
                .ForMember(s => s.NomeEmpresa, s => s.MapFrom(x => x.Servico.Empresa.Nome))
                .ForMember(s => s.IsEmpresa, s => s.MapFrom(x => x.EmpresaId.HasValue && !x.EmpresaId.Equals(Guid.Empty)))
                .ForMember(s => s.PrestadorServicoId, s => s.MapFrom(x => x.Servico.ObterIdPrestador()))
                .ReverseMap();

            CreateMap<SolicitacaoModel, Solicitacao>()
                .ConstructUsing((src, res) =>
                {
                    return new Solicitacao(res.Mapper.Map<DetalhesSolicitacao>(src.Detalhes));
                });
                
        }
    }
}
