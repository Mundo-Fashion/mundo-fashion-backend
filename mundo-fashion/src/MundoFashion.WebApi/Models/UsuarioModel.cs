using System;

namespace MundoFashion.WebApi.Models
{
    public record UsuarioModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public UsuarioModel(string username, string password, DateTime createdAt)
        {
            Username = username;
            Password = password;
            CreatedAt = createdAt;
        }
    }
}
