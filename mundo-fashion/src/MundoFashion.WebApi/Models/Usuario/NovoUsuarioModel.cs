using System;
using System.Collections.Generic;
using MundoFashion.WebApi.Models.Solicitacao;

namespace MundoFashion.WebApi.Models.Usuario
{
    public record NovoUsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public string Cpf { get; set; }
        public ServicoEstampaModel Servico { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
