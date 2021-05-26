using System;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models
{
    public record SolicitacaoModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DetalhesSolicitacaoModel Detalhes { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeEmpresa { get; set; }
        public Guid? ServicoId { get; set; }
        public Guid? PrestadorServicoId { get; set; }
        public List<MensagemModel> Mensagens { get; set; } = new List<MensagemModel>();
        public bool IsEmpresa { get; set; }        
    }
}
