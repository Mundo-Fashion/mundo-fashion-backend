using AutoMapper;
using MundoFashion.Domain;
using MundoFashion.WebApi.Models.Mensagem;

namespace MundoFashion.WebApi.ProfileMaps
{
    public class MensagemProfile : Profile
    {
        public MensagemProfile()
        {
            CreateMap<Mensagem, MensagemModel>();
        }
    }
}
