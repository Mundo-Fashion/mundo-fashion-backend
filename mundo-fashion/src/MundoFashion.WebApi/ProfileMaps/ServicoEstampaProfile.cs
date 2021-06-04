using AutoMapper;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Models;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class ServicoEstampaProfile : Profile
    {
        public ServicoEstampaProfile()
        {
            CreateMap<ServicoEstampaModel, ServicoEstampa>()
                .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => y.TipoEstampa))
                .ForMember(s => s.Tecnica, x => x.MapFrom(y => y.TipoTecnica))
                .ForMember(s => s.TecnicaEstamparia, x => x.MapFrom(y => y.TipoTecnicaEstamparia))
                .ForMember(s => s.Nicho, x => x.MapFrom(y => y.TipoNicho))
                .ForMember(s => s.TipoRapport, x => x.MapFrom(y => y.TipoRapport));

            CreateMap<ServicoEstampaModel, ServicoEstampa>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => y.TipoEstampa))
             .ForMember(s => s.Tecnica, x => x.MapFrom(y => y.TipoTecnica))
             .ForMember(s => s.TecnicaEstamparia, x => x.MapFrom(y => y.TipoTecnicaEstamparia))
             .ForMember(s => s.Nicho, x => x.MapFrom(y => y.TipoNicho))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => y.TipoRapport))
             .ReverseMap();
        }
    }
}
