using System;

namespace MundoFashion.Core.Dto
{
    public class UsuarioDto
    {        
        public Guid Id { get; private set; }
        public string AvatarLink { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public string DescricaoPessoal { get; set; }
        public DateTime CreatedAt { get; private set; }

        public UsuarioDto(Guid id, string avatarLink, string nome, string email, string role, DateTime createdAt, string descricaoPessoal)
        {
            Id = id;
            Nome = nome;
            AvatarLink = avatarLink;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}
