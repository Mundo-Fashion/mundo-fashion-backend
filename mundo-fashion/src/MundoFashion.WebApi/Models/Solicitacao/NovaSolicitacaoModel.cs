using System;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models.Solicitacao
{
    public record NovaSolicitacaoModel
    {
        public DetalhesSolicitacaoModel Detalhes { get; set; }
        public Guid ServicoId { get; set; }
        public Guid TomadorServicoId { get; set; }   
    }
}
