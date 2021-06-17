using System;

namespace MundoFashion.WebApi.Models.Solicitacao
{
    public record NovaSolicitacaoModel
    {
        public NovoDetalhesSolicitacaoModel Detalhes { get; set; }
        public Guid ServicoId { get; set; }
        public Guid TomadorServicoId { get; set; }   
    }
}
