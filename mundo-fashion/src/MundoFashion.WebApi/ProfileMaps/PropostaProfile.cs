using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class PropostaProfile : Profile
    {
        public PropostaProfile()
        {
            CreateMap<Proposta, PropostaModel>();
            CreateMap<Proposta, PropostaModel>().ReverseMap();
        }
    }
}