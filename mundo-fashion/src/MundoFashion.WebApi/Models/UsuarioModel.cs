using System;

namespace MundoFashion.WebApi.Models
{
    public record UsuarioModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public ServicoEstampaModel Servico { get; set; }
        public DateTime CreatedAt { get; private set; }

        public UsuarioModel(string username, string password, DateTime createdAt, string role = "")
        {
            Username = username;
            Password = password;
            CreatedAt = createdAt;
            Role = role;
        }
    }
}
