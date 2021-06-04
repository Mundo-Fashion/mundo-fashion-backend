using MundoFashion.Core;
using System;

namespace MundoFashion.Domain
{
    public class Mensagem : Entity
    {
        public Guid EmissorId { get; private set; }
        public Guid ReceptorId { get; private set; }
        public string Conteudo { get; private set; }

        public Solicitacao Solicitacao { get; set; }
        public Guid SolicitacaoId { get; private set; }
        public Mensagem(Guid emissorId, Guid receptorId, string conteudo)
        {
            EmissorId = emissorId;
            ReceptorId = receptorId;
            Conteudo = conteudo;
        }
    }
}
