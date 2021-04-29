using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class DetalhesSolicitacaoProfile : Profile
    {
        public DetalhesSolicitacaoProfile()
        {
            CreateMap<DetalhesSolicitacaoModel, DetalhesSolicitacao>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => y.TipoEstampa))
             .ForMember(s => s.Tecnica, x => x.MapFrom(y => y.TipoTecnica))
             .ForMember(s => s.TecnicaEstamparia, x => x.MapFrom(y => y.TipoTecnicaEstamparia))
             .ForMember(s => s.Nicho, x => x.MapFrom(y => y.TipoNicho))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => y.TipoRapport));

            CreateMap<DetalhesSolicitacaoModel, DetalhesSolicitacao>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => y.TipoEstampa))
             .ForMember(s => s.Tecnica, x => x.MapFrom(y => y.TipoTecnica))
             .ForMember(s => s.TecnicaEstamparia, x => x.MapFrom(y => y.TipoTecnicaEstamparia))
             .ForMember(s => s.Nicho, x => x.MapFrom(y => y.TipoNicho))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => y.TipoRapport))
             .ReverseMap();
        }
    }
}
