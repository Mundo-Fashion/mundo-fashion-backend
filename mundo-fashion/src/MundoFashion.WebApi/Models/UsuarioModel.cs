using System;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models
{
    public record UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public string Cpf { get; set; }
        public List<EmpresaModel> Empresas { get; set; }
        public List<SolicitacaoModel> Solicitacoes { get; set; }
        public ServicoEstampaModel Servico { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
