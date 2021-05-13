using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models;
using System.Linq;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class DetalhesSolicitacaoProfile : Profile
    {
        public DetalhesSolicitacaoProfile()
        {
            AllowNullCollections = true;

            CreateMap<DetalhesSolicitacaoModel, DetalhesSolicitacao>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => y.TipoEstampa))
             .ForMember(s => s.Tecnica, x => x.MapFrom(y => y.TipoTecnica))
             .ForMember(s => s.TecnicaEstamparia, x => x.MapFrom(y => y.TipoTecnicaEstamparia))
             .ForMember(s => s.Nicho, x => x.MapFrom(y => y.TipoNicho))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => y.TipoRapport));           

            CreateMap<DetalhesSolicitacao, DetalhesSolicitacaoModel>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => y.TipoEstampa))
             .ForMember(s => s.TipoTecnica, x => x.MapFrom(y => y.Tecnica))
             .ForMember(s => s.TipoTecnicaEstamparia, x => x.MapFrom(y => y.TecnicaEstamparia))
             .ForMember(s => s.TipoNicho, x => x.MapFrom(y => y.Nicho))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => y.TipoRapport))
             .ForMember(s => s.Imagens, x => x.MapFrom(y => y.Imagens));
        }
    }
}
