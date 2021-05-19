using System;

namespace MundoFashion.Core.Dto
{
    public class UsuarioDto
    {        
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public UsuarioDto(Guid id, string nome, string email, string role, DateTime createdAt)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}
