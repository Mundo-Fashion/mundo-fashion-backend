using System;

namespace MundoFashion.WebApi.Models
{
    public record UsuarioModel
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public ServicoEstampaModel Servico { get; set; }
        public DateTime CreatedAt { get; private set; }

        public UsuarioModel(string nome, string email, string password, DateTime createdAt, string role = "")
        {
            Nome = nome;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
            Role = role;
        }
    }
}
