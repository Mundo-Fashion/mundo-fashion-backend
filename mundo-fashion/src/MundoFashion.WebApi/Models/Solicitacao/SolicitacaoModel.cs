using System;
using System.Collections.Generic;
using MundoFashion.WebApi.Models.Usuario;

namespace MundoFashion.WebApi.Models.Solicitacao
{
    public record SolicitacaoModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public PrestadorTomadorModel Tomador { get; set; }
        public DetalhesSolicitacaoModel Detalhes { get; set; }
        public ServicoEstampaModel Servico { get; set; }
        public List<MensagemModel> Mensagens { get; set; }
    }
}