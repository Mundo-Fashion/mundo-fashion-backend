using System;

namespace MundoFashion.WebApi.Models
{
    public record PropostaModel
    {
        public double Valor { get; set; }
        public bool Aceita { get; set; }
    }
}
