using System;

namespace MundoFashion.WebApi.Models
{
    public class PropostaModel
    {
        public Guid SolicitacaoId { get; set; }
        public double Valor { get; set; }
        public bool Aceita { get; set; }
    }
}
