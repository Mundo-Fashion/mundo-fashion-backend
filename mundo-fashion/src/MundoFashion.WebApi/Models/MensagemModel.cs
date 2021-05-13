using System;

namespace MundoFashion.WebApi.Models
{
    public record MensagemModel(Guid solicitacaoId, Guid emissorId, Guid receptorId, string conteudo, DateTime dataHora = new DateTime());
}
