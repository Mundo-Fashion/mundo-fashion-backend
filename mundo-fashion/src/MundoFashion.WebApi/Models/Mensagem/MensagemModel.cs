using System;

namespace MundoFashion.WebApi.Models.Mensagem
{
    public record MensagemModel
    {        
        public Guid EmissorId { get; set; }
        public Guid ReceptorId { get; set; }
        public string Conteudo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
