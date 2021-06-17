using AutoMapper;
using MundoFashion.Core.Utils;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models.Solicitacao;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class DetalhesSolicitacaoProfile : Profile
    {
        public DetalhesSolicitacaoProfile()
        {
            AllowNullCollections = true;        

            CreateMap<DetalhesSolicitacao, DetalhesSolicitacaoModel>()
             .ForMember(s => s.TipoEstampa, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.TipoEstampa)))
             .ForMember(s => s.TipoTecnica, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.Tecnica)))
             .ForMember(s => s.TipoTecnicaEstamparia, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.TecnicaEstamparia)))
             .ForMember(s => s.TipoNicho, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.Nicho)))
             .ForMember(s => s.TipoRapport, x => x.MapFrom(y => EnumUtils.ObterValoresEmTextoFlagEnum(y.TipoRapport)))
             .ForMember(s => s.Imagens, x => x.MapFrom(y => y.Imagens));
        }
    }
}
