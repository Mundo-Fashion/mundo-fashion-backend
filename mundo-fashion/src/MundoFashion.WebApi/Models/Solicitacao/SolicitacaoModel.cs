using System;
using System.Collections.Generic;
using MundoFashion.WebApi.Models.Usuario;
using MundoFashion.WebApi.Models.Mensagem;

namespace MundoFashion.WebApi.Models.Solicitacao
{
    public record SolicitacaoModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public long Codigo { get; set; }
        public PrestadorTomadorModel Tomador { get; set; }
        public DetalhesSolicitacaoModel Detalhes { get; set; }
        public ServicoEstampaModel Servico { get; set; }
        public PropostaModel Proposta { get; set; }
        public List<MensagemModel> Mensagens { get; set; }
    }
}