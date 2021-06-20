using AutoMapper;
using MundoFashion.Core.Utils;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Models;
using MundoFashion.WebApi.Models.Servico;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class ServicoEstampaProfile : Profile
    {
        public ServicoEstampaProfile()
        {
            CreateMap<ServicoEstampa, ServicoEstampaModel>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.TipoEstampa)))
             .ForMember(s => s.TipoTecnica, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.Tecnica)))
             .ForMember(s => s.TipoTecnicaEstamparia, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.TecnicaEstamparia)))
             .ForMember(s => s.TipoNicho, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.Nicho)))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.TipoRapport)));

            CreateMap<ServicoEstampaAtualizadoModel, ServicoEstampa>();
        }
    }
}
