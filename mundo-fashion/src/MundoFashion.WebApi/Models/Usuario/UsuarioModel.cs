using System;

namespace MundoFashion.WebApi.Models.Usuario
{
    public record UsuarioModel
    {
        public Guid Id { get; set; }
        public string AvatarLink { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public string Cpf { get; set; }
        public ServicoEstampaModel Servico { get; set; }
        public string DescricaoPessoal { get; set; }
        public bool UtilizaSuporteAlexa { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}