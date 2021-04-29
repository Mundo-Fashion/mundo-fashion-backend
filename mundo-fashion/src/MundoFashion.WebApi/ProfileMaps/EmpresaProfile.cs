using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Models;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, EmpresaModel>().ReverseMap();

            CreateMap<Empresa, EmpresaModel>()
                .AfterMap((src, dest, context) => dest.Servico = context.Mapper.Map<ServicoEstampa, ServicoEstampaModel>(src.Servico));
        }
    }
}
