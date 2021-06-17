using System;

namespace MundoFashion.WebApi.Models.Mensagem
{
    public record NovaMensagemModel
    {
        public Guid SolicitacaoId { get; set; }
        public Guid EmissorId { get; set; }
        public Guid ReceptorId { get; set; }
        public string Conteudo { get; set; }
    }
}